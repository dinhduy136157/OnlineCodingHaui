using Newtonsoft.Json;
using OnlineCodingHaui.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCodingHaui.Application.Services.Implementations
{
    public class WrapCodeService : IWrapCodeService
    {
        public string Wrap(string studentCode, string functionName, string returnType, string parametersJson, string exampleInput, string language)
        {
            try
            {
                var parameters = JsonConvert.DeserializeObject<List<Parameter>>(parametersJson);
                return language.ToLower() switch
                {
                    "python" => WrapPython(studentCode, functionName, returnType, parameters),
                    "java" => WrapJava(studentCode, functionName, returnType, parameters),
                    "php" => WrapPHP(studentCode, functionName, returnType, parameters),
                    _ => throw new NotSupportedException($"Language {language} is not supported")
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error wrapping code: {ex.Message}");
            }
        }

        private string WrapPython(string code, string functionName, string returnType, List<Parameter> parameters)
        {
            code = code.Trim();
            if (code.Contains("if __name__ == '__main__':"))
                code = code.Split(new[] { "if __name__ == '__main__':" }, StringSplitOptions.None)[0].Trim();

            return $@"{code}

if __name__ == '__main__':
    import sys
    import ast
    input = sys.stdin.read().strip()
    try:
{Indent(GeneratePythonInputProcessing(parameters), 2)}
        result = {functionName}({string.Join(", ", parameters.Select(p => p.Name))})
{Indent(GeneratePythonOutput(returnType), 2)}
    except Exception as e:
        print(""Error:"", str(e))";
        }


        private string WrapJava(string code, string functionName, string returnType, List<Parameter> parameters)
        {
            code = code.Trim();

            // Nếu code đã chứa public class Main thì không cần wrap lại
            if (code.Contains("public class Main"))
            {
                return code;
            }

            // Nếu code đã chứa static main hoặc method static, chỉ cần thêm class Main
            return $@"import java.util.*;
            import java.util.stream.*;

            public class Main {{
                {code}

                public static void main(String[] args) {{
                    Scanner scanner = new Scanner(System.in);
                    try {{
                        String input = scanner.nextLine();
                        {GenerateJavaInputProcessing(parameters)}
                        {GetJavaType(returnType)} result = {functionName}({string.Join(", ", parameters.Select(p => p.Name))});
                        {GenerateJavaOutput(returnType)}
                    }} catch (Exception e) {{
                        System.out.println(""Error: "" + e.getMessage());
                    }} finally {{
                        scanner.close();
                    }}
                }}
            }}";
        }

        private string WrapPHP(string code, string functionName, string returnType, List<Parameter> parameters)
        {
            code = code.Trim();

            return $@"<?php
            {code}

            // Input processing
            $input = trim(fgets(STDIN));
            {GeneratePHPInputProcessing(parameters)}

            // Execute function
            $result = {functionName}({string.Join(", ", parameters.Select(p => "$" + p.Name))});

            // Output processing
            {GeneratePHPOutput(returnType)}
            ?>";
        }

        #region Input Processing
        private string GeneratePythonInputProcessing(List<Parameter> parameters)
        {
            var sb = new StringBuilder();

            // Nếu chỉ có 1 tham số và kiểu mảng (ví dụ: int[])
            if (parameters.Count == 1 && parameters[0].Type.EndsWith("[]"))
            {
                var param = parameters[0];
                sb.AppendLine("import ast");
                sb.AppendLine($"{param.Name} = list(map({GetPythonTypeConverter(param.Type.Replace("[]", ""))}, ast.literal_eval(input)))");
            }
            else
            {
                sb.AppendLine("import ast");
                for (int i = 0; i < parameters.Count; i++)
                {
                    var param = parameters[i];
                    if (param.Type.EndsWith("[]"))
                    {
                        sb.AppendLine($"{param.Name} = list(map({GetPythonTypeConverter(param.Type.Replace("[]", ""))}, ast.literal_eval(input.split()[{i}])))");
                    }
                    else
                    {
                        sb.AppendLine($"{param.Name} = {GetPythonTypeConverter(param.Type)}(input.split()[{i}])");
                    }
                }
            }

            return sb.ToString();
        }



        private string GenerateJavaInputProcessing(List<Parameter> parameters)
        {
            var sb = new StringBuilder();

            // Nếu chỉ có 1 tham số và kiểu mảng
            if (parameters.Count == 1 && parameters[0].Type.EndsWith("[]"))
            {
                var param = parameters[0];
                sb.AppendLine("String[] inputArray = input.replaceAll(\"[\\\\[\\\\]]\", \"\").split(\",\");");
                sb.AppendLine($"{param.Type} {param.Name} = new {param.Type}[inputArray.length];");
                sb.AppendLine("for (int i = 0; i < inputArray.length; i++) {");
                sb.AppendLine($"    String trimmed = inputArray[i].trim();");
                sb.AppendLine($"    if (!trimmed.isEmpty()) {{");
                sb.AppendLine($"        {param.Name}[i] = {GetJavaParseFunction(param.Type.Replace("[]", ""))}(trimmed);");
                sb.AppendLine($"    }}");
                sb.AppendLine("}");
            }
            else
            {
                // Xử lý trường hợp nhiều tham số
                sb.AppendLine("String[] parts = input.split(\"\\\\s+\");");
                for (int i = 0; i < parameters.Count; i++)
                {
                    var param = parameters[i];
                    if (param.Type.EndsWith("[]"))
                    {
                        sb.AppendLine($"String[] arrayParts = parts[{i}].replaceAll(\"[\\\\[\\\\]]\", \"\").split(\",\");");
                        sb.AppendLine($"{param.Type} {param.Name} = new {param.Type}[arrayParts.length];");
                        sb.AppendLine("for (int j = 0; j < arrayParts.length; j++) {");
                        sb.AppendLine($"    String trimmed = arrayParts[j].trim();");
                        sb.AppendLine($"    if (!trimmed.isEmpty()) {{");
                        sb.AppendLine($"        {param.Name}[j] = {GetJavaParseFunction(param.Type.Replace("[]", ""))}(trimmed);");
                        sb.AppendLine($"    }}");
                        sb.AppendLine("}");
                    }
                    else
                    {
                        sb.AppendLine($"String trimmed = parts[{i}].trim();");
                        sb.AppendLine($"if (!trimmed.isEmpty()) {{");
                        sb.AppendLine($"    {GetJavaType(param.Type)} {param.Name} = {GetJavaParseFunction(param.Type)}(trimmed);");
                        sb.AppendLine($"}} else {{");
                        sb.AppendLine($"    {GetJavaType(param.Type)} {param.Name} = {GetJavaDefaultValue(param.Type)};");
                        sb.AppendLine($"}}");
                    }
                }
            }

            return sb.ToString();
        }

        private string GeneratePHPInputProcessing(List<Parameter> parameters)
        {
            var sb = new StringBuilder();

            // Nếu chỉ có 1 tham số và kiểu mảng
            if (parameters.Count == 1 && parameters[0].Type.EndsWith("[]"))
            {
                var param = parameters[0];
                sb.AppendLine($"$inputArray = array_map('trim', explode(',', trim($input, '[]')));");
                sb.AppendLine($"${param.Name} = array_map('{GetPHPTypeConverter(param.Type.Replace("[]", ""))}', $inputArray);");
            }
            else
            {
                // Xử lý trường hợp nhiều tham số
                sb.AppendLine("$parts = explode(' ', $input);");
                for (int i = 0; i < parameters.Count; i++)
                {
                    var param = parameters[i];
                    if (param.Type.EndsWith("[]"))
                    {
                        sb.AppendLine($"$arrayParts = array_map('trim', explode(',', trim($parts[{i}], '[]')));");
                        sb.AppendLine($"${param.Name} = array_map('{GetPHPTypeConverter(param.Type.Replace("[]", ""))}', $arrayParts);");
                    }
                    else
                    {
                        sb.AppendLine($"${param.Name} = {GetPHPTypeConverter(param.Type)}(trim($parts[{i}]));");
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Output Processing
        private string GeneratePythonOutput(string returnType)
        {
            return returnType.EndsWith("[]")
                ? "print(result)"  // In ra mảng theo dạng thực tế trong Python, không dùng join
                : "print(result)";
        }


        private string GenerateJavaOutput(string returnType)
        {
            if (returnType.EndsWith("[]"))
            {
                return @"
            if (result == null) {
                System.out.println(""[]"");
            } else {
                StringBuilder sb = new StringBuilder();
                sb.append(""["");
                for (int i = 0; i < result.length; i++) {
                    if (result[i] != null) {
                        sb.append(result[i]);
                    }
                    if (i < result.length - 1) {
                        sb.append("", "");
                    }
                }
                sb.append(""]"");
                System.out.println(sb.toString());
            }";
            }
            else
            {
                return @"
            if (result == null) {
                System.out.println(""null"");
            } else {
                System.out.println(result);
            }";
            }
        }

        private string GeneratePHPOutput(string returnType)
        {
            var lowerType = returnType.ToLower();
            if (lowerType == "bool[]" || lowerType == "boolean[]")
            {
                return @"
if ($result === null) {
    echo '[]';
} else {
    echo '[' . implode(', ', array_map(function($v) { return $v ? 'True' : 'False'; }, $result)) . ']';
}";
            }
            else if (lowerType == "bool" || lowerType == "boolean")
            {
                return @"
if ($result === null) {
    echo 'null';
} else {
    echo $result ? 'True' : 'False';
}";
            }
            else if (returnType.EndsWith("[]"))
            {
                return @"
if ($result === null) {
    echo '[]';
} else {
    echo '[' . implode(', ', $result) . ']';
}";
            }
            else
            {
                return @"
if ($result === null) {
    echo 'null';
} else {
    echo $result;
}";
            }
        }

        #endregion

        #region Type Helpers
        private string GetJavaTypeConverter(string type) => type switch
        {
            "int" => "Integer::parseInt",
            "float" => "Float::parseFloat",
            "double" => "Double::parseDouble",
            "string" => "",
            "bool" => "Boolean::parseBoolean",
            _ => throw new NotSupportedException($"Type {type} is not supported in Java")
        };
        private string GetPythonTypeConverter(string type) => type switch
        {
            "int" => "int",
            "float" => "float",
            "double" => "float",
            "string" => "str",
            "bool" => "bool",
            _ => "str"
        };

        private string GetJavaType(string type) => type switch
        {
            "int" => "int",
            "float" => "float",
            "double" => "double",
            "string" => "String",
            "bool" => "boolean",
            "int[]" => "int[]",
            "float[]" => "float[]",
            "double[]" => "double[]",
            "string[]" => "String[]",
            "bool[]" => "boolean[]",
            _ => "Object"
        };

        private string GetJavaArrayType(string type) => type switch
        {
            "int" => "Int",
            "float" => "Double",
            "double" => "Double",
            _ => "Object"
        };

        private string GetJavaParseFunction(string type) => type switch
        {
            "int" => "Integer.parseInt",
            "float" => "Float.parseFloat",
            "double" => "Double.parseDouble",
            "bool" => "Boolean.parseBoolean",
            _ => ""
        };

        private string GetJavaDefaultValue(string type) => type switch
        {
            "int" => "0",
            "float" => "0.0f",
            "double" => "0.0",
            "string" => "\"\"",
            "bool" => "false",
            _ => "null"
        };

        private string GetPHPTypeConverter(string type) => type switch
        {
            "int" => "intval",
            "float" => "floatval",
            "double" => "floatval",
            "string" => "strval",
            "bool" => "boolval",
            _ => "strval"
        };
        #endregion
        private string Indent(string code, int level = 1)
        {
            var indent = new string(' ', level * 4);
            return string.Join("\n", code.Split('\n').Select(line => indent + line));
        }
        private class Parameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
    }
}

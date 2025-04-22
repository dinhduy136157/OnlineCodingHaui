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

            // Nếu chỉ có 1 tham số và là mảng
            if (parameters.Count == 1 && parameters[0].Type.EndsWith("[]"))
            {
                var param = parameters[0];
                sb.AppendLine($"{param.Type} {param.Name} = Arrays.stream(input.replaceAll(\"[\\\\[\\\\]]\", \"\").split(\",\"))");
                sb.AppendLine($".map(String::trim).filter(s -> !s.isEmpty()).mapTo{GetJavaArrayType(param.Type.Replace("[]", ""))}({GetJavaTypeConverter(param.Type.Replace("[]", ""))}).toArray();");

            }
            else
            {
                // Xử lý trường hợp nhiều tham số
                sb.AppendLine("String[] parts = input.split(\"\\\\s+\");");
                for (int i = 0; i < parameters.Count; i++)
                {
                    var param = parameters[i];
                    sb.AppendLine(param.Type.EndsWith("[]")
                        ? $"{param.Type} {param.Name} = Arrays.stream(parts[{i}].replaceAll(\"[\\\\[\\\\]]\", \"\").split(\",\"))"
                          + $".map(String::trim).filter(s -> !s.isEmpty()).mapTo{GetJavaArrayType(param.Type.Replace("[]", ""))}({GetJavaTypeConverter(param.Type.Replace("[]", ""))}).toArray();"
                        : $"{GetJavaType(param.Type)} {param.Name} = {GetJavaParseFunction(param.Type)}(parts[{i}]);");
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
                System.out.println(""null"");
            } else {
                System.out.print(""["");
                for (int i = 0; i < result.length; i++) {
                    System.out.print(result[i]);
                    if (i < result.length - 1) {
                        System.out.print("", "");
                    }
                }
                System.out.println(""]"");
            }
        ";
            }
            else
            {
                return @"
            System.out.println(result);
        ";
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
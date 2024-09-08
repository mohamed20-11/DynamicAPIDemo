using EnvDTE;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {
        // نتأكد ان اسم الكلاس valid
        static readonly HashSet<string> ReservedKeywords = new HashSet<string>
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
        "char", "checked", "class", "const", "continue", "decimal", "default",
        "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
        "false", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
        "in", "int", "interface", "internal", "is", "lock", "long", "namespace",
        "new", "null", "object", "operator", "out", "override", "params", "private",
        "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
        "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
        "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
        "ushort", "using", "virtual", "void", "volatile", "while" , "program"
    };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            List<string> properties = new List<string>();
            string moreProps;

            do
            {
                Console.Write("Enter a property name (or type 'done' to finish): ");
                string propertyName = Console.ReadLine();
                if (propertyName.ToLower() == "done") break;

                Console.Write("Enter the property type (e.g., int, string, DateTime): ");
                string propertyType = Console.ReadLine();

                propertyName = EnsureValidName(propertyName, true);
                properties.Add($"{propertyType} {propertyName}");

                Console.Write("Add another property? (yes/no): ");
                moreProps = Console.ReadLine();
            } while (moreProps.ToLower() == "yes");

            Console.Write("Enter the class name: ");
            string className = Console.ReadLine();
            className = EnsureValidName(className, false);

            CreateNewClass(className, properties);
        }
        static string EnsureValidName(string name, bool isProperty)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z_]\w*$") || ReservedKeywords.Contains(name))
            {
                // If not valid, addddd  a random character to the name
                name += new Random().Next(1, 1000).ToString();
            }
            return name;
        }

        static void CreateNewClass(string className,List<string> properties)
        {
            // Get the directory of the currently executing assembly (where Program.cs is located)
            //string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string classFilePath = Path.Combine(projectDirectory, $"{className}.cs");

            try
            {
                // Check if a class with the same name already exists
                if (File.Exists(classFilePath))
                {
                    Console.WriteLine($"A class named '{className}' already exists.");
                    return;
                }

                // Create the class file with basic structure
                string classContent = $@"
using System;

public class {className}
{{
   ";

                foreach (var property in properties)
                {
                    classContent += $"    public {property} {{ get; set; }}\n";
                }
                classContent+=$@"
                // Constructor
    public {className
    }
    () 
    {{
        // TODO: Initialize properties here if necessary
    }}

    // TODO: Add methods as needed
}}";

                // Write the class content to the file
                File.WriteAllText(classFilePath, classContent);

                Console.WriteLine($"Class '{className}' created at: {classFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

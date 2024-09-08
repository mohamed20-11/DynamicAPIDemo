using EnvDTE;

namespace ConsoleApp1
{
    public class Program
    {
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

                properties.Add($"{propertyType} {propertyName}");

                Console.Write("Add another property? (yes/no): ");
                moreProps = Console.ReadLine();
            } while (moreProps.ToLower() == "yes");


            Console.Write("Enter the class name: ");
            string className = Console.ReadLine();
            CreateNewClass(className, properties);
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

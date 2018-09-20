using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Tasks.TestingChamber.TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Use args to testing files");
                return;
            }

            foreach (var item in args)
            {
                if (IsDirectory(item))
                {
                    Directory.GetFiles(item).ToList().ForEach(x =>
                    {
                        try
                        {
                            TestFile(x);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error while running of {item}\r\n{e.Message}");
                        }
                    });
                }
                else
                {
                    try
                    {
                        TestFile(item);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error while running of {item}\r\n{e.Message}");
                    }
                }
            }
            Console.ReadLine();
        }

        static void TestFile(string file)
        {
            var dll = Assembly.LoadFile(file);
            var runnables = dll.GetExportedTypes().Where(x => x.Name.Contains("Runnable") && x.IsClass);
            foreach (var item in runnables)
            {
                dynamic runnable = Activator.CreateInstance(item);
                int result = runnable.Run(1);
                PrintResult(result);
            }
        }

        static void PrintResult(int result)
        {
            string output = result == 1
                ? $"Success"
                : $"Wrong answer.\tExpected: 1\tResult: {result}";
            Console.WriteLine(output);
        }

        static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory);
        }
    }
}

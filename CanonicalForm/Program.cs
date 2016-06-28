using System;
using System.IO;

namespace CanonicalForm
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var sr = new StreamReader("TestFile.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parser = new ParserEquation.ParserEquation(line);
                        var root = parser.Parse();
                        var canonicalForm = root.ToCanonicalForm();
                        foreach (var monomial in canonicalForm)
                        {
                            Console.Write(monomial.Сoefficient);
                            foreach (var varible in monomial.Varibles)
                            {
                                Console.Write(varible.Name + "^" + varible.Degree);
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("/========================/");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}

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
                        parser.Parse();
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

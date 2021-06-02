using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    class FileIOEx
    {
        static string file = "C:/Users/NosyT/source/repos/Preparation/PlayField/FileIOEx/Text.txt";

        public static void SynchronousRead()
        {
            using(FileStream fs = File.OpenRead("C:/Users/NosyT/source/repos/Preparation/PlayField/FileIOEx/Text.txt"))
            {
                byte[] b = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                fs.Read(b, 0, b.Length);
                Console.WriteLine("Synchronous Read: " + encoder.GetString(b));
            }
        }

        public static void WriteFile()
        {
            StreamWriter wtr = new StreamWriter(file);
            wtr.WriteLine("First Line - The Trilogy");
            wtr.Close();
            wtr.Dispose();
        }

        public static void ReadFile(out string output)
        {
            StreamReader rdr = new StreamReader(file);
            output = rdr.ReadLine();
            rdr.Close();
            rdr.Dispose();
        }

        public static void Main(string[] args)
        {
            WriteFile();

            string output;
            ReadFile(out output);

            Console.WriteLine($"Read Output: {output}");

            SynchronousRead();

            Console.ReadLine();
        }
    }
}

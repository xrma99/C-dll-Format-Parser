using System;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;

namespace CsharpDLLparser
{
    class Program
    {
        static void readfile(string filepath)
        {
            //读取二进制文件
            FileStream fs = new FileStream("C:\\Users\\t-xinma\\Documents\\Csharp_tryout.dll",FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Byte content;
            long totallen = br.BaseStream.Length;//total length

            //写入txt文件
            FileStream fr = new FileStream("C:\\Users\\t-xinma\\Documents\\test.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fr);

            while (br.BaseStream.Position<br.BaseStream.Length)
            {
                content = br.ReadByte();
                
                Console.Write(content.ToString("X"));
                sw.Write(content.ToString("X"));
            }

            fs.Close();
            br.Close();

            fr.Close();
            br.Close();

            

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please input file path");
            string filepath=Console.ReadLine();
            readfile(filepath);
        }
    }
}

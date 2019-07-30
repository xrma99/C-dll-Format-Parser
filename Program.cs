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
            FileStream fs = new FileStream("C:\\Users\\t-xinma\\Documents\\CsharpHelloworld.dll", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Byte content;
            char c;
            long totallen = br.BaseStream.Length;//total length

            //写入txt文件
            FileStream fr = new FileStream("C:\\Users\\t-xinma\\Documents\\Tes.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fr);

            int count = 0;
            int line = 16;
            while (br.BaseStream.Position<br.BaseStream.Length)
            {
                content = br.ReadByte();
                c = (char)content;
                if (content < line)
                {
                    Console.Write("0");
                    sw.Write("0");
                }
                Console.Write(c);
                sw.Write(content.ToString("X"));
                count += 1;
            }


            Console.WriteLine(count);


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

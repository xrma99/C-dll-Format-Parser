using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;

namespace CsharpDLLparser
{
    class Program
    {
        private static int Reverseread(byte[] data,int lowindex,int largeindex)
        {
            //length is no bigger than 4 bytes
            int i;
            byte tmp;
            int res=0;
            for ( i = lowindex ; i <= (lowindex+largeindex) / 2; i++)
            {
                tmp = data[i];
                data[i] = data[largeindex - i];
                data[largeindex - i] = tmp;
            }
            for (i = lowindex; i <= largeindex; i++)
                res = res * 256 + data[i];
            printbyte(data,lowindex, largeindex);
            return res;
        }
        static void printbyte(byte[] data,int lowindex,int largeindex)
        {
            int i;
            for (i = lowindex; i <= largeindex; i++)
            {
                if (data[i] < 16)
                {
                    Console.Write("0");
                }
                Console.Write(data[i].ToString("X"));
            }
        }
        static void readfile(string filepath)
        {
            //读取二进制文件
            FileStream fs = new FileStream("C:\\Users\\t-xinma\\Documents\\Csharp_tryout.dll", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Byte content;
            char c;
            long totallen = br.BaseStream.Length;//total length

            //写入txt文件
            FileStream fr = new FileStream("C:\\Users\\t-xinma\\Documents\\Test.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fr);

            
            int count = 0,border=0;
            int flag=0;
            int paragraph_sz = 16;//unit:byte
            int section_c=7;//一共有多少个section，最多有7个
            int[] section_sz = new int[7];

            byte[] rawdata = new byte[16];
            byte[] data = new byte[8];

            while (br.BaseStream.Position<br.BaseStream.Length)
            {
                switch (flag)
                {
                    case 0://MZ header
                        Console.WriteLine("MZ Header");

                        Console.Write("Magic id: ");
                        printbyte(br.ReadBytes(2), 0, 1);
                        Console.WriteLine();

                        printbyte(br.ReadBytes(6),0,5);
                        Console.WriteLine();

                        Console.Write("Number of paragraph in the header: ");
                        int len = Reverseread(br.ReadBytes(2), 0, 1);//get the length of the MZ header
                        Console.WriteLine();

                        printbyte(br.ReadBytes(len*paragraph_sz-10), 0, len * paragraph_sz - 10-1);
                        Console.WriteLine();
                        
                        Console.WriteLine();
                        flag++;
                        break;
                    case 1:
                        /************default part**************/
                        br.ReadBytes(14);
                        for (int i = 0; i < 39; i++)
                        {
                            content = br.ReadByte();
                            c = (char)content;
                            Console.Write(c);
                        }//This program cannot be run in DOS mode.
                        Console.WriteLine();
                        
                        br.ReadBytes(11);

                        Console.WriteLine();
                        flag++;
                        break;
                    case 2://PE Header
                        
                        Console.WriteLine("PE Header");
                        Console.Write("Magic id: ");
                        printbyte(br.ReadBytes(4), 0, 3);
                        Console.WriteLine();

                        Console.Write("Machine Type: ");
                        Reverseread(br.ReadBytes(2),0,1);
                        Console.WriteLine();

                        Console.Write("Number of sections: ");
                        section_c = Reverseread(br.ReadBytes(2), 0, 1);//get the length of the MZ header
                        Console.WriteLine();

                        br.ReadBytes(240);

                        Console.WriteLine();
                        flag++;
                        break;
                    case 3://section header information
                        Console.Write("Section ");
                        for(int i = 0; i < 8; i++)
                        {
                            content = br.ReadByte();
                            c = (char)content;
                            Console.Write(c);
                        }
                        Console.WriteLine();

                        Console.Write("Section size: ");
                        section_sz[border] = Reverseread(br.ReadBytes(4),0,3);
                        Console.WriteLine();

                        br.ReadBytes(28);

                        border++;
                        if (border >= section_c)//section header 结束
                        {
                            flag++;
                            br.ReadBytes(16);//Reserved
                        }
                            
                        Console.WriteLine();
                        break;
                    case 4://Section Content: how to define the reserved bytes???
                        for(int i = 0; i < section_c; i++)
                        {
                            Console.WriteLine("Section Content:");
                            printbyte(br.ReadBytes(section_sz[i]), 0, section_sz[i] - 1);
                            Console.WriteLine();
                        }
                        flag++;
                        break;
                    default:
                        Console.Write(br.ReadByte().ToString("X"));
                        break;
                }
                
                //sw.Write(content.ToString("X"));
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

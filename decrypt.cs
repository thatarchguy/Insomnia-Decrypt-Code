//Programmer:   Kevin Law
//Uses:         To decrypt the key used to save the config in the insomnia malware.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) // init program
            {
                Console.WriteLine("----Insomnia Decrypter----");
                Console.WriteLine("----     Kevin Law    ----");
                Console.WriteLine("Best if ran from cmd.exe to be able to paste into.");
                Console.WriteLine("Enter enc value from Config.cs: ");
                string enc = Console.ReadLine(); // Get string from user
               
                
                if (enc == "")
                {
                    Console.WriteLine("No Input");
                }
                
                
                if (enc == "exit") 
                {
                   break;
                }
                
                
                 string toParse = Encoding.UTF8.GetString(Convert.FromBase64String(enc));
                 string customerID = toParse.Substring(0, 8);
                 string[] nfo = Chk(Decrypt(toParse.Remove(0, 8)), Convert.ToInt32(customerID)).Split(new char[1]
    {
      '/'
    });

                //Dump Config
                //Console.WriteLine("Dumping All");
                 //foreach (var item in nfo)
                 //{
                  //   Console.WriteLine(item.ToString());
                // }
               
                Console.WriteLine("Server: " + nfo[0]);
                Console.WriteLine("Port: " + nfo[1]);
                Console.WriteLine("Password: " + nfo[2]);
                Console.WriteLine("Channel: " + nfo[3]);
                Console.WriteLine("Key: " + nfo[4]);
                Console.WriteLine("bkChan: " + nfo[5]);
                Console.WriteLine("rkChan: " + nfo[6]);
                Console.WriteLine("AuthHost: " + nfo[7]);
                Console.WriteLine("RegistryKey: " + nfo[8]);
                Console.WriteLine("InstallEnv: " + nfo[9]);
                Console.WriteLine("Colors: " + nfo[10]);
                Console.WriteLine("AntiSandboxie: " + nfo[11]);

            }
        }

        public static string Chk(string textToEncrypt, int key)
        {
            StringBuilder stringBuilder1 = new StringBuilder(textToEncrypt);
            StringBuilder stringBuilder2 = new StringBuilder(textToEncrypt.Length);
            for (int index = 0; index < textToEncrypt.Length; ++index)
            {
                char ch = (char)((uint)stringBuilder1[index] ^ (uint)key);
                stringBuilder2.Append(ch);
            }
            return ((object)stringBuilder2).ToString();
        }

        public static string Decrypt(string text)
        {
            try
            {
                string str = string.Empty;
                char[] chArray1 = text.ToCharArray();
                int num1 = (int)chArray1[0];
                int index1 = text.Length - 1;
                int length = text.Length;
                int index3;
                text = "";
                switch (num1 % 4)
                {
                    case 0:
                        for (int index2 = 1; index2 < index1; --index1)
                        {
                            char ch1 = chArray1[index2];
                            chArray1[index2] = chArray1[index1];
                            chArray1[index1] = ch1;
                            index3 = index2 + 2;
                            if (index3 < index1)
                            {
                                char ch2 = chArray1[index3];
                                chArray1[index3] = chArray1[index3 - 1];
                                chArray1[index3 - 1] = ch2;
                            }
                            index2 = index3 + 1;
                        }
                        for (int index2 = 1; index2 < length; ++index2)
                            text = text + (object)chArray1[index2];
                        break;
                    case 1:
                        for (int index2 = 1; index2 < index1; index1 = index3 - 1)
                        {
                            char ch1 = chArray1[index2];
                            chArray1[index2] = chArray1[index1];
                            chArray1[index1] = ch1;
                            index3 = index1 - 2;
                            if (index2 < index3)
                            {
                                char ch2 = chArray1[index3];
                                chArray1[index3] = chArray1[index3 + 1];
                                chArray1[index3 + 1] = ch2;
                            }
                            ++index2;
                        }
                        for (int index2 = 1; index2 < length; ++index2)
                            text = text + (object)chArray1[index2];
                        break;
                    case 2:
                        char ch3 = chArray1[index1];
                        chArray1[index1] = chArray1[index1 / 2];
                        chArray1[index1 / 2] = ch3;
                        char ch4 = chArray1[1];
                        chArray1[1] = chArray1[index1 / 2 + 1];
                        chArray1[index1 / 2 + 1] = ch4;
                        for (int index2 = 1; index2 < length; ++index2)
                            text = text + (object)chArray1[index2];
                        break;
                    case 3:
                        for (int index2 = 1; index2 < length; ++index2)
                            text = text + (object)chArray1[index2];
                        break;
                }
                char[] chArray2 = text.ToCharArray();
                int num2 = (int)chArray2[0];
                int index4 = 1;
                while (index4 < text.Length)
                {
                    str = (int)chArray2[index4] < 80 ? str + (object)(char)(num2 - (int)chArray2[index4 + 1] + 48) : str + (object)(char)((int)chArray2[index4 + 1] + num2 - 48);
                    index4 += 2;
                }
                return str;
            }
            catch
            {
                Environment.Exit(0);
                return (string)null;
            }
        }





    }
}

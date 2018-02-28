using System;
using IFiles_CFindFile_CWriteFile;

namespace UseCFindFileCWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoty = @"D:\Test_file_sys";
            IFiles myfiles = new CFindFiles(directoty, "reversed2");
            IFiles WriteNow = new CWrite();
            if(myfiles.GetStrings()!=null)
            { 
            WriteNow.WriteStrings(myfiles.GetStrings());
            Console.WriteLine("Check File");
            }

            myfiles.SetStrings(directoty,"all");
            if (myfiles.GetStrings() != null)
            {
                WriteNow.WriteStrings(myfiles.GetStrings());
                Console.WriteLine("Check File");
            }

            myfiles.SetStrings(directoty, "reversed1");
            if (myfiles.GetStrings() != null)
            {
                WriteNow.WriteStrings(myfiles.GetStrings());
                Console.WriteLine("Check File");
            }

            myfiles.SetStrings(directoty, "cpp");
            if (myfiles.GetStrings() != null)
            {
                WriteNow.WriteStrings(myfiles.GetStrings());
                Console.WriteLine("Check File");
            }

            Console.ReadKey();
        }
    }
}

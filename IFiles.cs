using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiles_CFindFile_CWriteFile
{
    using System;
    using System.IO;


    public interface IFiles
    {
        void SetStrings(string directory, string mod);
        String[] GetStrings();
        void WriteStrings(string[] strings);
        void WriteStrings(string[] strings,string directories);

    }


    public class CFindFiles : IFiles
    {
        private static string[] _stringsFromFile;
        public CFindFiles()
        { }

        public CFindFiles(string directory, string mod)
        { SetStrings(directory, mod); }

        public static string[] GetStrings()
        { return _stringsFromFile; }

        public void SetStrings(string directory, string mod)
        {
            String[] fromFile;

            if (Directory.Exists(directory))      //правильно ли передали дерикторию
            {
                switch (mod)
                {
                    case "all":
                    {
                        fromFile = GetFilesFromDir(directory, "*");
                            if (fromFile!=null) { 
                                _stringsFromFile = ReadyNames(fromFile, directory);}
                            else { 
                            
                            }
                        break;
                        }
                    case "cpp":
                        {
                            fromFile = GetFilesFromDir(directory, "cpp");
                            if (fromFile != null)
                            {
                                _stringsFromFile = ReadyNames(fromFile, directory);
                            }                            
                            break;
                        }
                    case "reversed1":
                        {
                            fromFile = GetFilesFromDir(directory, "*");
                            if (fromFile != null)
                            {
                                _stringsFromFile = Reversed(ReadyNames(GetFilesFromDir(directory, "*"), directory), 1);
                            }                           
                            break;
                        }
                    case "reversed2":
                        {
                            fromFile = GetFilesFromDir(directory, "*");
                            if (fromFile != null)
                            {
                                _stringsFromFile = Reversed(ReadyNames(GetFilesFromDir(directory, "*"), directory), 2);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Error: Wrong mod");
                            _stringsFromFile = null;
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("Error: Wrong dir {0} not exists ", directory); // если неверно дана дериктория
                _stringsFromFile = null;
            }

        }

        string[] IFiles.GetStrings()
        {
            return GetStrings();
        }

        public virtual void WriteStrings(string[] strings)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteStrings(string[] strings, string directories)
        {
            throw new NotImplementedException();
        }

        public string[] GetFilesFromDir(string directory, string mod) // поиск файлов и подпапок в заданой дерикт
        {
            try
            {
                string[] str;
                str = Directory.GetFiles(directory, mod);// нахождение всех файлов из текущей папки с расширением cpp
                string[] dirs = Directory.GetDirectories(directory, "*"); //поиск подпапок                   
                foreach (string dir in dirs)
                {
                    int siz = str.Length;
                    string[] addStr = GetFilesFromDir(dir, mod);    //поиск файлов в подпапках
                    Array.Resize(ref str, siz + addStr.Length); //расширение масива
                    Array.Copy(addStr, 0, str, siz, addStr.Length);//добавление новых файлов в общий список
                }

                return str;
            }
            catch (Exception e)
            {
                Console.WriteLine("In dir {1}. The process failed: {0}", e, directory);
                Console.WriteLine();
                return null;
            }

        }


        public string[] ReadyNames(string[] names, string dir)//уберает лишни путь для относительности с стартовой папки
        {

                string[] ready = new string[names.Length];
                int i = 0;
                foreach (string name in names)
                {
                    ready[i++] = name.Substring(dir.Length);
                }
                return ready;
        }


        public string[] Reversed(string[] files, int mod)
        {
            int y = 0;
            string[] resolt = new string[files.Length];
            if (mod == 1)
            {
                foreach (string file in files)
                {
                    string[] dirs = file.Split(new char[] { '\\' }); //делим на масив значений между слешем
                    int i = dirs.Length - 1;
                    resolt[y] = dirs[i--];//берем последний элемент
                    while (i >= 0)  //добавляем поочереди в обратном порядке
                    {
                        resolt[y] += @"\";
                        resolt[y] += dirs[i--];//берем последний элемент
                    }
                    y++;
                }
            }
            else
            {
                foreach (string file in files)
                {
                    char[] begStr = file.ToCharArray(); // перевод в символы
                    char[] endStr = new char[begStr.Length];
                    for (int i = 0; i < begStr.Length; i++)
                    { endStr[i] = begStr[begStr.Length - (i + 1)]; } //переворот строки
                    resolt[y++] = new string(endStr); //сохранение результата
                }
            }
            return resolt;
        }

    }

    public class CWrite : IFiles
    {
        public virtual string[] GetStrings()
        {
            throw new NotImplementedException();
        }

        public virtual void SetStrings(string directory, string mod)
        {
            throw new NotImplementedException();
        }

        public async void WriteStrings(string[] strings)
        {
           
            try
            {
                using (StreamWriter fWriter = new StreamWriter(@"D:\results.txt", true, System.Text.Encoding.Default))
                {
                    if (strings != null)
                    {
                        foreach (string str in strings)
                        {
                            await fWriter.WriteLineAsync(str);
                        }       
                    }
                    else
                    {
                        await fWriter.WriteLineAsync("Strings are empty");
                    }
                    fWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e);
                Console.WriteLine();
            }
           

        }

        public async void WriteStrings(string[] strings, string directories)
        {
            try
            {
                using (StreamWriter fWriter = new StreamWriter(directories, true, System.Text.Encoding.Default))
                {
                    if (strings != null)
                    {
                        foreach (string str in strings)
                        {
                            await fWriter.WriteLineAsync(str);
                        }
                    }
                    else
                    {
                        await fWriter.WriteLineAsync("Strings are empty");
                    }
                    fWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e);
                Console.WriteLine();
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csharpexam
{
    class Dictionary
    {
        private List<string> words;
        private List<List<string>> translations;
        private List<string> trans;
        public Dictionary()
        {
            words = new List<string>();
            translations = new List<List<string>>();
        }
        public void CreateDict(string path)
        {
            int i = 0;
            using (StreamWriter str = new StreamWriter(path))
            {
                do
                {
                    Console.WriteLine("Введите слово (для окончания ввода нажмите Enter): ");
                    string word = Console.ReadLine();
                    if (word != "")
                    {
                        List<string> trans = new List<string>();
                        words.Add(word);
                        Console.WriteLine("Введите перевод(ы) этого слова (для окончания ввода нажмите Enter): ");
                        string word2;
                        do
                        {
                            word2 = Console.ReadLine();
                            if (word2 != "") trans.Add(word2);
                        } while (word2 != "");
                        translations.Add(trans);
                        string phrase = string.Join(", ", translations.ToArray()[i]);
                        str.WriteLine(words[i] + " - " + phrase);
                        i++;
                    }
                    else break;
                } while (!words.Equals(""));
            }
            Console.WriteLine();
        }
        public void PrintDict(string path)
        {
            using (StreamWriter str = new StreamWriter(path, false))
            {
                for (int i = 0; i < words.Count; i++)
                {
                    string phrase = string.Join(", ", translations.ToArray()[i]);
                    str.WriteLine(words[i] + " - " + phrase);
                }
            }
            Console.WriteLine();
        }
        public void AddWord(string path)
        {
            using (StreamWriter str = new StreamWriter(path, true))
            {
                Console.WriteLine("Введите слово на русском (для окончания ввода нажмите Enter): ");
                string word = Console.ReadLine();
                if (word != "")
                {
                    List<string> trans = new List<string>();
                    words.Add(word);
                    Console.WriteLine("Введите перевод(ы) этого слова (для окончания ввода нажмите Enter): ");
                    string word2;
                    do
                    {
                        word2 = Console.ReadLine();
                        if (word2 != "") trans.Add(word2);
                    } while (word2 != "");
                    translations.Add(trans);
                    string phrase = string.Join(", ", translations.ToArray()[words.Count - 1]);
                    str.WriteLine(words[words.Count - 1] + " - " + phrase);
                }
            }
        }
        public void ChangeWord(string path)
        {
            Console.Write("Введите индекс слова, которое вы хотите поменять в словаре: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Введите слово: ");
            words[num] = Console.ReadLine();
            PrintDict(path);
        }
        public void ChangeTranslation(string path)
        {
            Console.Write("Введите индекс слова, перевод которого нужно изменить: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Введите индекс перевода слова, которое нужно изменить: ");
            int num2 = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Введите слово: ");
            translations.ElementAt(num).RemoveAt(num2);
            translations.ElementAt(num).Insert(num2, Console.ReadLine());
            PrintDict(path);
        }
        public void DeleteWord(string path)
        {
            Console.Write("Введите индекс слова, которое вы хотите удалить в словаре: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            words.RemoveAt(num);
            translations.RemoveAt(num);
            PrintDict(path);
        }
        public void DeleteTranslation(string path)
        {
            Console.Write("Введите индекс слова, перевод которого вы хотите удалить в словаре: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Введите индекс перевода слова, которое вы хотите удалить в словаре: ");
            int num2 = Convert.ToInt32(Console.ReadLine()) - 1;
            if (translations.ToArray()[num].Count() > 1)
            {
                translations.ToArray()[num].RemoveAt(num2);
                PrintDict(path);
            }
            else Console.WriteLine("Переводы этого слова больше нельзя удалять!");
            Console.WriteLine();
        }
        public void PrintWord()
        {
            Console.WriteLine("Введите путь: ");
            string path3 = Console.ReadLine();
            Console.Write("Введите индекс слова, которое вы хотите распечатать: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            using (StreamWriter str3 = new StreamWriter(path3))
            {
                string phrase = string.Join(", ", translations.ToArray()[num]);
                str3.WriteLine(words[num] + " - " + phrase);
            }
            Console.WriteLine();
        }
        public void SearchWord(string word)
        {
            int n = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] == word)
                {
                    string phrase = string.Join(", ", translations.ToArray()[i]);
                    Console.WriteLine("Перевод слова \"" + word + "\": " + phrase);
                    n++;
                }
            }
            if (n == 0) Console.WriteLine("Данного слова нет в словаре!");
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            string path = "";
            string path1 = "..\\Русско-английский словарь.txt";
            string path2 = "..\\Англо-русский словарь.txt";
            Console.Write("Выберите, какой словарь создать: русско-английский или англо-русский (0, 1): ");
            bool choice;
            choice = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            if (!choice) path = path1;
            else if (choice) path = path2;
            Dictionary dic = new Dictionary();
            dic.CreateDict(path);
            do
            {
                Console.WriteLine("Выберите опцию: ");
                Console.WriteLine("1 - изменить слово в словаре");
                Console.WriteLine("2 - изменить перевод слова в словаре");
                Console.WriteLine("3 - удалить слово в словаре");
                Console.WriteLine("4 - удалить перевод слова в словаре");
                Console.WriteLine("5 - распечатать слово и перевод: ");
                Console.WriteLine("6 - добавить слово и перевод: ");
                Console.WriteLine("7 - найти перевод слова: ");
                Console.WriteLine("0 - выход");
                num = Convert.ToInt32(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        dic.ChangeWord(path);
                        break;
                    case 2:
                        dic.ChangeTranslation(path);
                        break;
                    case 3:
                        dic.DeleteWord(path);
                        break;
                    case 4:
                        dic.DeleteTranslation(path);
                        break;
                    case 5:
                        dic.PrintWord();
                        break;
                    case 6:
                        dic.AddWord(path);
                        break;
                    case 7:
                        Console.Write("Введите искомое слово: ");
                        dic.SearchWord(Console.ReadLine());
                        break;
                }
            } while (num != 0);
        }
    }
}
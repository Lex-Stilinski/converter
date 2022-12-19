using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace convert
{
    internal class ReadSave
    {
        public static List<Model> fandom = new List<Model>();

        private static void DesTxT(string path)
        {
            string[] MyModel_txt = File.ReadAllLines(path);

            for (int i = 0; i < MyModel_txt.Length; i += 3) 
            {
                Model fandoms = new Model();
                fandoms.Fandom = MyModel_txt[i];
                fandoms.Character_1 = MyModel_txt[i + 1];
                fandoms.Character_2 = MyModel_txt[i + 2];

                fandom.Add(fandoms);
            }
            foreach (string i in MyModel_txt)
            {
                Console.WriteLine(" " + i);
            }
        }

        private static void SerTxT(string path)
        {
            foreach (Model fandoms in fandom) 
            {
                File.AppendAllText(path, fandoms.Fandom + "\n");
                File.AppendAllText(path, fandoms.Character_1 + "\n");
                File.AppendAllText(path, fandoms.Character_2 + "\n");
            }
        }

        private static void DesJson(string path)
        {
            string MyModel_json = File.ReadAllText(path);

            fandom = JsonConvert.DeserializeObject<List<Model>>(MyModel_json);

            foreach (Model i in fandom)
            {
                Console.WriteLine(" " + i.Fandom);
                Console.WriteLine(" " + i.Character_1);
                Console.WriteLine(" " + i.Character_2);
            }
        }

        private static void SerJson(string path)
        {
            string MyModel_json = JsonConvert.SerializeObject(fandom);
            File.WriteAllText(path, MyModel_json);
        }

        private static void DesXML(string path) 
        {
            XmlSerializer MyModel_xml = new XmlSerializer(typeof(List<Model>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fandom = (List<Model>)MyModel_xml.Deserialize(fs);
            }

            foreach (Model i in fandom)
            {
                Console.WriteLine(" " + i.Fandom);
                Console.WriteLine(" " + i.Character_1);
                Console.WriteLine(" " + i.Character_2);
            }
        }

        private static void SerXml(string path) 
        {
            XmlSerializer MyModel_xml = new XmlSerializer(typeof(List<Model>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                MyModel_xml.Serialize(fs, fandom);
            }
        }

        public static void ConDes()
        {
            Console.WriteLine("F1 - Сохранить файл (.txt, .json, .xml). \nEsc - выход из программы");
            Console.WriteLine("-------------------------------------------------------------------");

            Console.WriteLine("Введите путь до файла который вы хотите открыть: ");
            string filepath = Console.ReadLine();

            if (filepath.Contains(".txt"))
            {
                Console.WriteLine("---------------------------------");
                DesTxT(filepath);
            }
            if (filepath.Contains(".json"))
            {
                Console.WriteLine("---------------------------------");
                DesJson(filepath);
            }
            if (filepath.Contains(".xml"))
            {
                Console.WriteLine("---------------------------------");
                DesXML(filepath);
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.F1)
            {
                ConSer();
            }
            /*else if (key.Key == ConsoleKey.Escape) 
            {
                Process.
            }*/
        }

        public static void ConSer()
        {
            Console.Clear();
            Console.WriteLine("Введите формат, в который вы хотите конвертировать файл (.txt, .json, .xml): ");
            string filepath = Console.ReadLine();

            if (filepath.Contains(".txt"))
            {
                SerTxT(filepath);
            }
            if (filepath.Contains(".json"))
            {
                SerJson(filepath);
            }
            if (filepath.Contains(".xml"))
            {
                SerXml(filepath);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using textConverter;

namespace Converter
{
    class textConverter
    {
        public static Chair Stefan = new Chair("ИКЕА СТЕФАН","ДЕРЕВО","ЧЕРНЫЙ");
        public static Chair Addler = new Chair("ИКЕА АДДЛЕР","ПЛАСТИК","ЖЕЛТЫЙ");

        public static void visualize()
        {
            int coords = 1;

            List<Chair> newList;
            List<Chair> chairs = new List<Chair>();

            chairs.Add(Stefan);
            chairs.Add(Addler);

            Console.WriteLine("ОТ ВАС ТРЕБУЕТСЯ ВВОД ПУТИ ДО ФАЙЛА. ЕСЛИ, ВДРУГ, ВЫ ХОТИТЕ ПЕРЕЙТИ В РЕЖИМ РЕДАКТИРОВАНИЯ НАЖМИТЕ F3");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                if (Path.GetExtension(path) == ".xml")
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Chair>));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        newList = (List<Chair>)xml.Deserialize(fs);
                    }
                    Console.WriteLine($"{newList[0].name}\n{newList[0].material}\n{newList[0].color}\n{newList[1].name}\n{newList[1].material}\n{newList[1].color}");
                }
                else if (Path.GetExtension(path) == ".json")
                {
                    string info = File.ReadAllText(path);
                    List<Chair> json = JsonConvert.DeserializeObject<List<Chair>>(info);
                    Console.WriteLine($"{json[0].name}\n{json[0].material}\n{json[0].color}");
                }
                else if (Path.GetExtension(path) == ".txt")
                {
                    string text = File.ReadAllText(path);
                    Console.WriteLine(text);
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.F3)
                {
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("ВЫБЕРИТЕ СТУЛЬЧИК И НАЖМИТЕ КНОПКУ ENTER");
                        Console.WriteLine("     СТУЛЬЧИК STEFAN");
                        Console.WriteLine("     СТУЛЬЧИК ADDLER");
                        Console.SetCursorPosition(1, coords);
                        Console.WriteLine("-->");
                        ConsoleKeyInfo Key = Console.ReadKey();
                        if (Key.Key == ConsoleKey.UpArrow)
                        {
                            if (coords - 1 >= 0)
                            {
                                coords--;
                            }
                        }
                        else if (Key.Key == ConsoleKey.DownArrow)
                        {
                            if (coords + 1 < 3)
                            {
                                coords++;
                            }
                        }
                        else if (Key.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.WriteLine("ДЛЯ СОХРАНЕНИЯ НАЖМИТЕ f1, А ДЛЯ ВЫХОДА escape");

                            if (coords == 1)
                            {
                                Console.WriteLine("ВВЕДИТЕ НОВОЕ ИМЯ: ");
                                Stefan.name = Console.ReadLine();
                                Console.WriteLine("ВВЕДИТЕ МАТЕРИАЛ СТУЛЬЧИКА");
                                Stefan.material = Console.ReadLine();
                                Console.WriteLine("ВВЕДИТЕ НОВЫЙ ЦВЕТ ДЛЯ СТУЛЬЧИКА");
                                Stefan.color = Console.ReadLine();
                            }
                            else if (coords == 2)
                            {
                                Console.WriteLine("ВВЕДИТЕ НОВОЕ ИМЯ: ");
                                Addler.name = Console.ReadLine();
                                Console.WriteLine("ВВЕДИТЕ МАТЕРИАЛ СТУЛЬЧИКА");
                                Addler.material = Console.ReadLine();
                                Console.WriteLine("ВВЕДИТЕ НОВЫЙ ЦВЕТ ДЛЯ СТУЛЬЧИКА");
                                Addler.color = Console.ReadLine();
                            }
                        }

                        Console.WriteLine("НАЖМИТЕ КЛАВИШУ");
                        Key = Console.ReadKey();
                        if (Key.Key == ConsoleKey.F1)
                        {
                            if (Path.GetExtension(path) == ".xml")
                            {
                                XmlSerializer xml = new XmlSerializer(typeof(List<Chair>));
                                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                                {
                                    xml.Serialize(fs, chairs);
                                }
                            }
                            else if (Path.GetExtension(path) == ".json")
                            {
                                string json = JsonConvert.SerializeObject(chairs);
                                File.WriteAllText(path, json);
                            }
                            else if (Path.GetExtension(path) == ".txt")
                            {
                                string text = $"{chairs[0].name}\n{chairs[0].material}\n{chairs[0].color}\n{chairs[1].name}\n{chairs[1].material}\n{chairs[1].color}";
                                File.WriteAllText(path, text);
                            }
                            break;
                        }
                        else if (Key.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("ВЫХОД ИЗ ПРОГРАММЫ");
                            break;
                        }

                    }
                    Console.Clear();
                }

            }
            else
            {
                if (Path.GetExtension(path) == ".xml")
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Chair>));
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        xml.Serialize(fs, chairs);
                    }
                }
                else if (Path.GetExtension(path) == ".txt")
                {
                    string text = $"{chairs[0].name}\n{chairs[0].material}\n{chairs[0].color}";
                    File.WriteAllText(path, text);
                }
                else if (Path.GetExtension(path) == ".json")
                {
                    string json = JsonConvert.SerializeObject(chairs);
                    File.WriteAllText(path, json);
                }
            }
        }
        public static void Main()
        {
            visualize();
        }
    }
}
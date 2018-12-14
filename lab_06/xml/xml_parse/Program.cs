using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker work = new Worker();
        }
 
    }
    class Worker
    {
        private XmlDocument _myDocument;

        private XmlDocument myDocument
        {
            get
            {
                if (_myDocument == null)
                {
                    Console.WriteLine("File dosen't load, loading ...");
                    LoadFile();
                }
                return _myDocument;
            }
            set { _myDocument = value; }
        }
        /// <summary>
        /// Открытие документа, находящегося в файле.
        /// </summary>
        private void LoadFile()
        {
            myDocument = new XmlDocument();
            FileStream myFile = null;
            try
            {
                myDocument.Load("/home/wh75er/projects/database-course/lab_06/xml/common.xml");

                Console.WriteLine("File was loaded.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                if (myFile != null)
                {
                    myFile.Close();
                }
            }

            Pause();
            MainMenu();
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе.
        /// </summary>
        private void Search()
        {
            Console.Clear();
            Console.WriteLine("\nSearch information in document:");
            Console.WriteLine("1. with GetElementsByTagName");
            Console.WriteLine("2. with GetElementsById");
            Console.WriteLine("3. with SelectNodes");
            Console.WriteLine("4. with SelectSingleNode");
            Console.WriteLine("5. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 5)
            {
                switch (option)
                {
                    case 1: GetByTag(); break;
                    case 2: GetByID(); break;
                    case 3: GetByNode(); break;
                    case 4: GetBySingleNode(); break;
                    case 5: MainMenu(); break;
                }
            }
            else
            {
                Console.WriteLine("Something wrong. Try again");
                Pause();
                Search();
            }
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе с помощью метода GetElementsByTagName
        /// </summary>
        private void GetByTag()
        {
            Console.Write("\nEnter tag:");
            string tag = Console.ReadLine();
            if (tag != null)
            {
                XmlNodeList taglist = myDocument.GetElementsByTagName(tag);
                if (taglist.Count == 0)
                {
                    Console.WriteLine("Nothing found");
                }
                foreach (XmlNode element in taglist)
                {
                    foreach (dynamic child in element) { 
                        Console.WriteLine(child.InnerText);
                    }
                    Console.WriteLine("----");
                    //Console.WriteLine(element.ChildNodes[0].InnerText);
                }
            }
            else
            {
                Console.WriteLine("Wrong tag");
            }
            Pause();
            Search();
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе с помощью метода GetElementsById
        /// </summary>
        private void GetByID()
        {

            Console.Write("\nEnter ID:");
            var tag = Console.ReadLine();
            if (tag != null)
            {
                XmlElement element = myDocument.GetElementById(tag);
                if (element is null)
                {
                    Console.WriteLine("Nothing found");
                }
                else
                {
                    foreach (dynamic i in element.ChildNodes) {
                        Console.WriteLine(i.InnerText);
                    }
                    //Console.WriteLine(element.ChildNodes[0].InnerText);
                }
            }
            else
            {
                Console.WriteLine("Wrong ID");
            }
            Pause();
            Search();
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе с помощью метода SelectNodes
        /// </summary>
        private void GetByNode()
        {

            Console.WriteLine("\nNames of vampires");
            //linConsole.ReadLine()
            XmlNodeList Node = myDocument.SelectNodes("//Monster[type='Vampire']");
            for (int i = 0; i < Node.Count; i++)
                Console.Write(Node[i].ChildNodes[0].InnerText + "\n");
            Pause();
            Search();
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе с помощью метода SelectSingleNode
        /// </summary>
        private void GetBySingleNode()
        {
            //Console.WriteLine("Время начала встречи в комнате 3");
            Console.WriteLine("\nEnter monster name:");
            string line = Console.ReadLine();
            //string.TryParse(line, out string n);

            XmlNode Node = myDocument.SelectSingleNode($"//Monster[name='{line}']");
            if (Node != null)
            {
                Console.Write(Node.ChildNodes[1].InnerText + "\r\n");
            }
            Pause();
            Search();
        }

        /// <summary>
        /// Доступ к содержимому узлов
        /// </summary>
        private void NodeUsage()
        {
            Console.Clear();
            Console.WriteLine("\nAccess to node content:");
            Console.WriteLine("1. to type nodes XmlElement");
            Console.WriteLine("2. to type nodes XmlText");
            Console.WriteLine("3. to type nodes XmlComment");
            Console.WriteLine("4. to type nodes XmlProcessingInstruction");
            Console.WriteLine("5. to node atributes");
            Console.WriteLine("6. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 6)
            {
                switch (option)
                {
                    case 1: AccessElement(); break;
                    case 2: AccessText(); break;
                    case 3: AccessComment(); break;
                    case 4: AccessInstruction(); break;
                    case 5: AccessAtr(); break;
                    case 6: MainMenu(); break;
                }
            }
            else
            {
                Console.WriteLine("Something wrong. Try again");
                Pause();
                Search();
            }
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlElement
        /// </summary>
        private void AccessElement()
        {
            Console.WriteLine("\nGetting ");
            XmlElement monsters = (XmlElement)myDocument.DocumentElement.ChildNodes[0];
            Console.Write(monsters.ChildNodes[0].Name + "\r\n");
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlТext
        /// </summary>
        private void AccessText()
        {
            Console.WriteLine("\nEnter number of node:");
            string input = Console.ReadLine();
            int.TryParse(input, out int i);
            Console.Write(myDocument.DocumentElement.ChildNodes[i - 1].InnerText + "\r\n");
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlComment
        /// </summary>
        private void AccessComment()
        {
            Console.WriteLine("\nEnter number of node:");
            string input = Console.ReadLine();
            int.TryParse(input, out int i);
            Console.Write(myDocument.DocumentElement.ChildNodes[i + 2].Value + "\r\n");
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlProcessingInstruction
        /// </summary>
        private void AccessInstruction()
        {
            if (myDocument.FirstChild is XmlProcessingInstruction)
            {
                XmlProcessingInstruction processInfo = (XmlProcessingInstruction)myDocument.FirstChild;
                Console.WriteLine(processInfo.Value);
                Pause();
                MainMenu();
            }
        }

        /// <summary>
        /// Доступ к содержимому узлов к атрибутам узлов
        /// </summary>
        private void AccessAtr()
        {
            Console.WriteLine("\nLevel 1 atributes");

            Console.Write("The monster attributes are:\n" + myDocument.DocumentElement.GetAttribute("name"));

            foreach (XmlNode node in myDocument.ChildNodes)
            {
                foreach (XmlNode nodeChildNode in node.ChildNodes)
                {
                    XmlAttributeCollection myAttributes1 = nodeChildNode.Attributes;
                    if (myAttributes1 != null)
                    {
                        foreach (XmlAttribute atr in myAttributes1)
                        {
                            Console.Write("Attribute: " + atr.Name + " = " + atr.Value + "\r\n");
                        }
                    }
                }
            }
            Pause();
            MainMenu();
        }



        /// <summary>
        /// Внесение изменений в документ.
        /// </summary>
        private void Change()
        {
            Console.Clear();
            Console.WriteLine("\nDocument changes:");
            Console.WriteLine("1. Delete content");
            Console.WriteLine("2. Making changes to the content");
            Console.WriteLine("3. Create new content");
            Console.WriteLine("4. Insert content");
            Console.WriteLine("5. Adding attributes");
            Console.WriteLine("6. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 6)
            {
                switch (option)
                {
                    case 1: ChangeRemove(); break;
                    case 2: ChangeChange(); break;
                    case 3: ChangeNew(); break;
                    case 4: ChangeInsert(); break;
                    case 5: ChangeAddAtr(); break;
                    case 6: MainMenu(); break;
                }
            }
            else
            {
                Console.WriteLine("Something wrong. Try again");
                Pause();
                Search();
            }
        }
        /// <summary>
        /// Внесение изменений в документ: удаление содержимого
        /// </summary>
        private void ChangeRemove()
        {
            Console.WriteLine("\nDelete first monster name");
            myDocument.DocumentElement.ChildNodes[0].RemoveChild(myDocument.DocumentElement.ChildNodes[0].ChildNodes[0]);
            Saver();
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Внесение изменений в документ: внесение изменений в содержимое
        /// </summary>
        private void ChangeChange()
        {
            Console.WriteLine("\nAdded ! to and of first name");
            XmlNodeList times = myDocument.SelectNodes("//Monster/name/text()");
            for (int i = 0; i < times.Count; i++)
                times[i].Value = times[i].Value + "!";
            Saver();
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Внесение изменений в документ: создание нового содержимого
        /// </summary>
        private void ChangeNew()
        {
            Console.WriteLine("Add monster to the end");
            XmlElement newElement = myDocument.CreateElement("Monster");
            XmlElement newName = myDocument.CreateElement("name");
            XmlElement newType = myDocument.CreateElement("type");
            XmlText newNameText = myDocument.CreateTextNode("NEW");
            XmlText newTypeText = myDocument.CreateTextNode("ELEMENT");
            newElement.AppendChild(newName);
            newElement.AppendChild(newType);
            newName.AppendChild(newNameText);
            newType.AppendChild(newTypeText);
            myDocument.DocumentElement.AppendChild(newElement);
            Saver();
            Pause();
            MainMenu();
        }


        /// <summary>
        /// Внесение изменений в документ: вставка содержимого
        /// </summary>
        private void ChangeInsert()
        {
            Console.WriteLine("Enter content into monster with id 1");
            Console.Write("Enter tag name:");
            string tag = Console.ReadLine();

            Console.Write("Enter tag content:");
            string text = Console.ReadLine();
            XmlElement newElement = myDocument.CreateElement(tag);
            XmlText newText = myDocument.CreateTextNode(text);
            newElement.AppendChild(newText);
            myDocument.DocumentElement.FirstChild.AppendChild(newElement);
            Saver();
            Pause();
            MainMenu();
        }

        /// <summary>
        /// Внесение изменений в документ: добавление атрибутов
        /// </summary>
        private void ChangeAddAtr()
        {
            myDocument.DocumentElement.SetAttribute("NEW", "ATTR");
            Saver();
            Pause();
            MainMenu();
        }

        private void Saver()
        {
            Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "task1-explicit*.xml");
            var last = files.Max(file => file);
            last = last.Replace("task1-explicit", "");
            last = last.Replace(".xml", "");
            last = last.Replace(Directory.GetCurrentDirectory() + "\\", "");
            int n = 0;
            int.TryParse(last, out n);
            n += 1;
            string filename = $"task1-explicit{n}.xml";
            myDocument.Save(filename);
            Console.WriteLine($"File {filename} saved");

        }

        private void Task()
        {
            Console.WriteLine("\nConverted all comments in tags");

            XmlNodeList taglist = myDocument.GetElementsByTagName("row");
            if (taglist.Count == 0)
            {
                Console.WriteLine("Nothing found");
            }
            foreach (XmlNode element in taglist)
            {
                XmlNodeList childs = element.ChildNodes;
                foreach (XmlNode child in childs)
                {
                    XmlNodeType type = child.NodeType;
                    if (type == XmlNodeType.Comment)
                    {
                        Console.WriteLine(child.Value);
                        XmlElement newTag = myDocument.CreateElement(child.Value);
                        XmlText newTagText = myDocument.CreateTextNode("NEW");
                        newTag.AppendChild(newTagText);
                        element.AppendChild(newTag);

                    }
                }
            }


            Saver();
            Pause();
            MainMenu();
        }

        private void Pause()
        {
            do {
                while(! Console.KeyAvailable) {
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n1. Opening a document located in a file.");
            Console.WriteLine("2. Search for information contained in the document.");
            Console.WriteLine("3. Access to node content");
            Console.WriteLine("4. Document changes.");
            Console.WriteLine("5. Task.");
            Console.WriteLine("\n0. Exit.");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 5 || option == 0)
            {
                switch (option)
                {
                    case 1: LoadFile(); break;
                    case 2: Search(); break;
                    case 3: NodeUsage(); break;
                    case 4: Change(); break;
                    case 5: Task(); break;
                    case 0: Console.Clear(); break;
                }
            }
            else
            {
                Console.WriteLine("Something wrong. Try again");
                Pause();
                MainMenu();
            }
        }

        public Worker()
        {
            MainMenu();
        }
   }
}

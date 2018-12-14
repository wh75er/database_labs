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
            Console.WriteLine("\n0. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 4 || option == 0)
            {
                switch (option)
                {
                    case 1: GetByTag(); break;
                    case 2: GetByID(); break;
                    case 3: GetByNode(); break;
                    case 4: GetBySingleNode(); break;
                    case 0: MainMenu(); break;
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

            Console.Write("\nPrice less than: ");
            string value = Console.ReadLine();
            //linConsole.ReadLine()
            XmlNodeList Nodes = myDocument.SelectNodes("//Build[Price < " + value +"]");
            foreach (dynamic node in Nodes) {
                foreach (dynamic info in node)
                    Console.WriteLine(info.InnerText);
                Console.WriteLine("----");
            }
            Pause();
            Search();
        }

        /// <summary>
        /// Поиск информации, содержащейся в документе с помощью метода SelectSingleNode
        /// </summary>
        private void GetBySingleNode()
        {
            Console.Write("\nPrice less than: ");
            string value = Console.ReadLine();

            XmlNode Node = myDocument.SelectSingleNode($"//Build[Price<"+ value +"]");
            if (Node != null)
            {
                foreach(dynamic info in Node) {
                    Console.WriteLine(info.InnerText);
                }
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
            Console.WriteLine("\n0. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 6 || option == 0)
            {
                switch (option)
                {
                    case 1: AccessElement(); break;
                    case 2: AccessText(); break;
                    case 3: AccessComment(); break;
                    case 4: AccessInstruction(); break;
                    case 5: AccessAtr(); break;
                    case 0: MainMenu(); break;
                }
            }
            else
            {
                Console.WriteLine("Something wrong. Try again");
                Pause();
                NodeUsage();
            }
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlElement
        /// </summary>
        private void AccessElement()
        {
            XmlElement build = (XmlElement)myDocument.DocumentElement.ChildNodes[0];
            Console.WriteLine(build);
            Console.WriteLine(build.ChildNodes[0].InnerText);
            Pause();
            NodeUsage();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlТext
        /// </summary>
        private void AccessText()
        {
            Console.WriteLine("\nEnter number of node:");
            string input = Console.ReadLine();
            int.TryParse(input, out int i);
            Console.WriteLine(myDocument.DocumentElement.ChildNodes[i - 1].InnerText);
            Pause();
            NodeUsage();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlComment
        /// </summary>
        private void AccessComment()
        {
            Console.WriteLine("\nEnter number of node:");
            string input = Console.ReadLine();
            int.TryParse(input, out int i);

            foreach (dynamic node in myDocument.DocumentElement.ChildNodes[i - 1])
                if (node is XmlComment)
                    Console.WriteLine(node.InnerText);
            Pause();
            NodeUsage();
        }

        /// <summary>
        /// Доступ к содержимому узлов к узлам типа XmlProcessingInstruction
        /// </summary>
        private void AccessInstruction()
        {
            XmlProcessingInstruction pi = (XmlProcessingInstruction)myDocument.SelectSingleNode("processing-instruction('xml-stylesheet')");

            if (pi != null)
            {
                Console.WriteLine(pi.Value);
                Pause();
                NodeUsage();
            }
            else
            {
                Console.WriteLine("Something went wrong...");
                Pause();
                NodeUsage();
            }
        }

        /// <summary>
        /// Доступ к содержимому узлов к атрибутам узлов
        /// </summary>
        private void AccessAtr()
        {
            Console.Write("Build attribute:\n" + myDocument.DocumentElement.GetAttribute("Id"));

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
            NodeUsage();
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
            Console.WriteLine("\n0. Return to main menu");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 5 || option == 0)
            {
                switch (option)
                {
                    case 1: ChangeRemove(); break;
                    case 2: ChangeChange(); break;
                    case 3: ChangeNew(); break;
                    case 4: ChangeInsert(); break;
                    case 5: ChangeAddAtr(); break;
                    case 0: MainMenu(); break;
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
            Console.WriteLine("\nDelete BodyId");
            foreach (dynamic node in myDocument.DocumentElement.ChildNodes[0])
                if (node.Name == "BodyId")
                    myDocument.DocumentElement.ChildNodes[0].RemoveChild(node);
            Saver();
            Pause();
            Change();
        }

        /// <summary>
        /// Внесение изменений в документ: внесение изменений в содержимое
        /// </summary>
        private void ChangeChange()
        {
            Console.WriteLine("\nMaking all builds free");
            XmlNodeList times = myDocument.SelectNodes("//Build/Price/text()");
            for (int i = 0; i < times.Count; i++)
                times[i].Value = "Free!";
            Saver();
            Pause();
            Change();
        }

        /// <summary>
        /// Внесение изменений в документ: создание нового содержимого
        /// </summary>
        private void ChangeNew()
        {
            Console.WriteLine("Add build to the end");
            XmlElement newElement = myDocument.CreateElement("Build");
            XmlElement newBody = myDocument.CreateElement("BodyId");
            XmlElement newLens = myDocument.CreateElement("LensId");
            XmlElement newFilter = myDocument.CreateElement("FilterId");
            XmlElement newPrice = myDocument.CreateElement("Price");
            XmlElement newYear = myDocument.CreateElement("Year");
            XmlText newBodyText = myDocument.CreateTextNode("999");
            XmlText newLensText = myDocument.CreateTextNode("LensEMOS");
            XmlText newFilterText = myDocument.CreateTextNode("Filter99");
            XmlText newPriceText = myDocument.CreateTextNode("300$");
            XmlText newYearText = myDocument.CreateTextNode("3010-20-10");

            newElement.SetAttribute("Id", "9999");
            newElement.AppendChild(newBody);
            newElement.AppendChild(newLens);
            newElement.AppendChild(newFilter);
            newElement.AppendChild(newPrice);
            newElement.AppendChild(newYear);
            newBody.AppendChild(newBodyText);
            newLens.AppendChild(newLensText);
            newFilter.AppendChild(newFilterText);
            newPrice.AppendChild(newPriceText);
            newYear.AppendChild(newYearText);
            myDocument.DocumentElement.AppendChild(newElement);
            Saver();
            Pause();
            Change();
        }


        /// <summary>
        /// Внесение изменений в документ: вставка содержимого
        /// </summary>
        private void ChangeInsert()
        {
            Console.WriteLine("Insert to build element with Id = 1");
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
            Change();
        }

        /// <summary>
        /// Внесение изменений в документ: добавление атрибутов
        /// </summary>
        private void ChangeAddAtr()
        {
            myDocument.DocumentElement.SetAttribute("NEW", "ATTR");
            Saver();
            Pause();
            Change();
        }

        private void Saver()
        {
            Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "common*.xml");
            var last = files.Max(file => file);
            last = last.Replace("common", "");
            last = last.Replace(".xml", "");
            last = last.Replace(Directory.GetCurrentDirectory() + "\\", "");
            int n = 0;
            int.TryParse(last, out n);
            n += 1;
            string filename = $"common{n}.xml";
            myDocument.Save(filename);
            Console.WriteLine($"File {filename} saved");

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
            Console.WriteLine("\n0. Exit.");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= 4 || option == 0)
            {
                switch (option)
                {
                    case 1: LoadFile(); break;
                    case 2: Search(); break;
                    case 3: NodeUsage(); break;
                    case 4: Change(); break;
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

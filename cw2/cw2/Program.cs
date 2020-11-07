using System;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Podaj sciezkę źródłową"); //D:\OLGIERD\Szkola\Semestr 5\APBD\dane.csv
                string csvPath = Console.ReadLine();
                if (csvPath.Equals(""))
                    csvPath = "data.csv";
                Console.WriteLine("Podaj ścieżkę docelową"); //D:\OLGIERD\Szkola\Semestr 5\APBD\
                string xmlPath = Console.ReadLine();
                if (xmlPath.Equals(""))
                    xmlPath = "result.xml";
                Console.WriteLine("Podaj format");
                string format = Console.ReadLine();
                if (format.Equals(""))
                    format = "xml";

                if (File.Exists(csvPath) && Directory.Exists(xmlPath) && format.Equals("xml"))
                {
                    string[] source = File.ReadAllLines(csvPath);
                    XElement xml = new XElement("Root",
                        from str in source
                        let fields = str.Split(',')
                        select new XElement("studenci",
                                new XAttribute("student_indexNumber", "s" + fields[4]),
                                new XElement("fname", fields[0]),
                                new XElement("lname", fields[1]),
                                new XElement("birthdate", fields[5]),
                                new XElement("email", fields[6]),
                                new XElement("mothersName", fields[7]),
                                new XElement("fathersname", fields[8]),
                                new XElement("studies",
                                new XElement("name", fields[2]),
                                new XElement("mode", fields[3])
                            )
                        )
                    );
                    xml.Save(String.Concat(xmlPath + "result.xml"));
                }
                else
                {
                    if(!File.Exists(csvPath))
                    {
                        Console.WriteLine("Podany plik csv nie istnieje");
                        throw new FileNotFoundException("Podny plik csv nie istnieje");
                    }
                    else if(!Directory.Exists(xmlPath))
                    {
                        Console.WriteLine("Podana ścieżka nie istnieje");
                        throw new DirectoryNotFoundException("Podana ścieżka nie istnieje");
                    }
                    else if(!format.Equals("xml"))
                    {
                        Console.WriteLine("Podany format nie jest obsługiwany");
                        throw new Exception("Podany format nie jest obsługiwany");
                    }
                }
            }
            catch(Exception ex)
            {
                logException(ex);
            }
        }

        static void logException(Exception ex)
        {
            try
            {
                string path = @"D:\OLGIERD\Szkola\Semestr 5\APBD\log";
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(DateTime.Now);
                    writer.WriteLine("Exception: " + ex.Message);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Sciezka nie istnieje");
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ConsoleApplication2
{
    class Program
    {
        static private TextReader conR = Console.In;
        static private TextWriter conW = Console.Out;
        static Type Student = typeof(Student);
        static void Main(string[] args)
        {   
                    
            Student st1 = new Student(1, "Nemo", "Legoscript", "CS", new string [] {"serialize","SQL","Ado_Net"});
            Student st2 = new Student(2, "Turing", "Enigma", "HardMath", new string[] { "crypto", "military", "art" });
            List<Student> stList = new List <Student> ();
            stList.AddRange(new Student[] { st1, st2 });
            SerializeS(st2);
            DeSerializeS();
        }

        static void SerializeS(Student stud)
        {
            Stream streamFileSoap = File.Open(@"D:\Vick\CSharp\tempis\soapStudent.soap", FileMode.Create,FileAccess.Write, FileShare.None);
            Stream streamFileBin = File.Open(@"D:\Vick\CSharp\tempis\binStudent.dat", FileMode.Create);
            Stream streamFileXml = File.Open(@"D:\Vick\CSharp\tempis\student.xml", FileMode.Create);

            IFormatter formatterSoap = new SoapFormatter();
            BinaryFormatter formatterBinnary = new BinaryFormatter();
            XmlSerializer serXML = new XmlSerializer(Student);
            //XmlSerializer serXML = new XmlSerializer(typeof(List<Student>)); //soap does not support by default, so all left as single obj

           formatterSoap.Serialize(streamFileSoap, stud);
            conW.WriteLine("Object serialzed Soap method");
            formatterBinnary.Serialize(streamFileBin, stud);
            conW.WriteLine("Object serialzed Bin method");
            serXML.Serialize(streamFileXml, stud);
            conW.WriteLine("Object serialzed XML method");
            streamFileSoap.Close();
            streamFileBin.Close();
            streamFileXml.Close();
            conW.WriteLine("Objects serialzed, press any key to continue with de-serizlize");
            Console.ReadKey();
//          stud = null;
        }


        static void DeSerializeS()
        {
            //Stream StreamFile = File.Open(path, FileMode.Open);
            Stream streamFileSoap = File.Open(@"D:\Vick\CSharp\tempis\soapStudent.soap", FileMode.Open); //or bin or xml
            Stream streamFileBin = File.Open(@"D:\Vick\CSharp\tempis\binStudent.dat", FileMode.OpenOrCreate); //or bin
            Stream streamFileXml = File.Open(@"D:\Vick\CSharp\tempis\student.xml", FileMode.OpenOrCreate);

            IFormatter formatterSoap = new SoapFormatter();
            BinaryFormatter formatterBinnary = new BinaryFormatter();
            //XmlSerializer serXML = new XmlSerializer(typeof(Student));
            XmlSerializer serXML = new XmlSerializer(Student);

            Student stSoapDeser = (Student)formatterSoap.Deserialize(streamFileSoap);
            conW.WriteLine("Soap xml is de-ser-d: \n{0}",stSoapDeser.ToString());
            Student stBinDeser = (Student)formatterBinnary.Deserialize(streamFileBin);
            conW.WriteLine("BIN xml is de-ser-d: \n{0}", stBinDeser.ToString());
            Student stXMLDeser = (Student)serXML.Deserialize(streamFileXml);
            conW.WriteLine("XML xml is de-ser-d: \n{0}", stXMLDeser.ToString());
            Console.ReadKey();
            //StreamFile.Close();
            streamFileSoap.Close();
            streamFileBin.Close();
            streamFileXml.Close();
        }
        internal static void WriteStreamToFile(string pathTo, string pathFrom)
        {
            StreamReader sr = new StreamReader(pathFrom);
            FileStream recordFile = new FileStream(pathTo, FileMode.Create);
            StreamWriter sw = new StreamWriter(recordFile, Encoding.GetEncoding(1251));
            string line = sr.ReadLine();

            while (line.Any())
            {
                line = sr.ReadLine();
                sw.WriteLine(line);
                Console.WriteLine(line);
            }
            Console.ReadKey();
            sw.Close();
        }
    }
}
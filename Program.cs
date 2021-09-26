using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Loadingassembly_reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //FileInfo f = new FileInfo("assemblies\\ChordFileGenerator.dll");
                //Assembly assembly = Assembly.LoadFrom(f.FullName);
                //string s = @"C:\Users\Sanket V\source\repos\ChordFileGenerator\ChordFileGenerator\bin\Debug\assemblies\ChordFileGenerator.dll";
                //Assembly assembly = Assembly.LoadFile(s);
                Assembly assembly = Assembly.LoadFile(@"C:\Users\Sanket V\source\repos\ChordFileGenerator\ChordFileGenerator.dll");
                Type type = assembly.GetType("ChordFileGenerator.Class1");
                Console.WriteLine(type.Name);
                object obj = Activator.CreateInstance(type);
                Console.Write("Enter Song Name : ");
                string songName = Console.ReadLine();
                SetPropertyValue(obj, "SongName", songName);
                Console.Write("Enter Artist : ");
                string artist = Console.ReadLine();
                SetPropertyValue(obj, "Artist", artist);

                Console.Write("Enter File Type : ");
                string fileTypeName = Console.ReadLine();
                Type fileTypeEnum = assembly.GetType("ChordFileGenerator.FileType");
                object fileTypeValue = GetEnumValue(fileTypeEnum, fileTypeName);
                SetPropertyValue(obj, "FileType", fileTypeValue);
                string lineType;
                do
                {

                    Console.Write("Line Type - Lyric(L), Chords &amp;amp; Lyric (CL) or blank to cancel: ");
                    lineType = Console.ReadLine();

                    if (lineType == "L")
                    {

                        string lyrics = Console.ReadLine();

                        InvokeMethod(obj, "AddLine", new object[] { lyrics });
                    }
                    else if (lineType == "CL")
                    {

                        string chords = Console.ReadLine();
                        string lyrics = Console.ReadLine();

                        InvokeMethod(obj, "AddLine", new object[] { chords, lyrics });
                    }
                } while (lineType == "L" || lineType == "CL");


                InvokeMethod(obj, "SaveFile", new object[] { "test.txt" });

                Console.WriteLine("Saved file!");

                Console.WriteLine(assembly);
            }
            catch(Exception ex)
            {
                ErrorLogging(ex);
                ReadError();
            }












            Console.Read();

        }
        public static object GetPropertyValue(object obj, string propertyName)
        {

            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            return property.GetValue(obj);
        }
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
           
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            property.SetValue(obj, value);
        }
        public static object InvokeMethod(object obj, string methodName, object[] arguments)
        {
            
            Type[] types = arguments.Select(x => x.GetType()).ToArray();

            
            MethodInfo method = obj.GetType().GetMethod(methodName, types);

            return method.Invoke(obj, arguments);
        }
        public static object GetEnumValue(Type enumType, string enumItemName)
        {
            
            Type enumUnderlyingType = enumType.GetEnumUnderlyingType();

           
            List<string> enumNames = enumType.GetEnumNames().ToList();
         
            Array enumValues = enumType.GetEnumValues();

           
            object enumValue = enumValues.GetValue(enumNames.IndexOf(enumItemName));

           
            return Convert.ChangeType(enumValue, enumUnderlyingType);
        }
        public static void ErrorLogging(Exception ex)
        {
            string strPath = @"C:\Training\EuroTraining\Files\log.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }
        public static void ReadError()
        {
            string strPath = @"C:\Training\EuroTraining\Files\log.txt";
            using (StreamReader sr = new StreamReader(strPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}

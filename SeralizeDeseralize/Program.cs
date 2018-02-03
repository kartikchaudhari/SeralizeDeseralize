using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SeralizeDeseralize
{
    [XmlRoot("ToDoUsers")]
    public class ToDo
    {
        public string ToDoString;
        public DateTime Date;
    }
    class Program
    {
        static List<ToDo> ToDoList = new List<ToDo>();
        public static void Ser(ToDo t) {
            ToDoList.Add(t);
            XmlSerializer mySerializer = new XmlSerializer(ToDoList.GetType(), new XmlRootAttribute("user_list"));
            StreamWriter myWriter = new StreamWriter("myXML.xml");
            mySerializer.Serialize(myWriter, ToDoList);
            myWriter.Close();
        }
        public static int Des(List<ToDo> tdList) {
            var myDeserializer = new XmlSerializer(typeof(List<ToDo>), new XmlRootAttribute("user_list"));
            using (var myFileStream = new FileStream("myXML.xml", FileMode.Open))
            {
                tdList = (List<ToDo>)myDeserializer.Deserialize(myFileStream);
            }
            return tdList.Count();
        }
        static void Main(string[] args)
        {
            ToDo MyToDo= new ToDo();
            ToDo MyToDo2 = new ToDo();
            MyToDo.ToDoString = "String 1";
            MyToDo2.ToDoString = "String 2";
            Ser(MyToDo);
            Console.WriteLine("No. of Object:{0}", Des(ToDoList));
            Ser(MyToDo2);
            Console.WriteLine("No. of Object:{0}", Des(ToDoList));
            Console.Read();
        }
    }
}

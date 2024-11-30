using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Maui.Controls;

namespace Lab2MAUI
{
    public static class MauiProgram
    {
        public class Student
        {
            public string Name { get; set; }
            public string Faculty { get; set; }
            public string Department { get; set; }
            public string Course { get; set; }
            public string RoomNumber { get; set; }
            public string PhoneNumber { get; set; }
            public string PaymentStatus { get; set; }
            public string AvailabilityOfBenefits { get; set; }
            public string Subordination { get; set; }
            public string Address { get; set; }

            public Student() { }
        }

        static private string xmlFilePath;

        public interface IStrategy
        {
            List<Student> Search(Student student);
        }
        public class Searcher
        {
            private Student student;
            private IStrategy strategy;

            public Searcher(Student p, IStrategy str, string path)
            {
                student = p;
                strategy = str;
                xmlFilePath = path;
            }

            public List<Student> SearchAlgorithm()
            {
                return strategy.Search(student);
            }
        }

        public class Sax : IStrategy
        {
            public List<Student> Search(Student student)
            {
                List<Student> results = new List<Student>();
                XmlTextReader xmlReader;

                try
                {
                    xmlReader = new XmlTextReader(xmlFilePath);
                }
                catch
                {
                    return null;
                }

                while (xmlReader.Read())
                {
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            string name = "";
                            string faculty = "";
                            string department = "";
                            string course = "";
                            string roomNumber = "";
                            string phoneNumber = "";
                            string paymentStatus = "";
                            string availabilityOfBenefits = "";
                            string subordination = "";
                            string address = "";

                            if (xmlReader.Name.Equals("NAME") && (xmlReader.Value.Equals(student.Name) || student.Name == null))
                            {
                                name = xmlReader.Value;
                                xmlReader.MoveToNextAttribute();
                                if (xmlReader.Name.Equals("FACULTY") && (xmlReader.Value.Equals(student.Faculty) || student.Faculty == null))
                                {
                                    faculty = xmlReader.Value;
                                    xmlReader.MoveToNextAttribute();
                                    if (xmlReader.Name.Equals("DEPARTMENT") && (xmlReader.Value.Equals(student.Department) || student.Department == null))
                                    {
                                        department = xmlReader.Value;
                                        xmlReader.MoveToNextAttribute();
                                        if (xmlReader.Name.Equals("COURSE") && (xmlReader.Value.Equals(student.Course) || student.Course == null))
                                        {
                                            course = xmlReader.Value;
                                            xmlReader.MoveToNextAttribute();
                                            if (xmlReader.Name.Equals("ROOMNUMBER") && (xmlReader.Value.Equals(student.RoomNumber) || student.RoomNumber == null))
                                            {
                                                roomNumber = xmlReader.Value;
                                                xmlReader.MoveToNextAttribute();
                                                if (xmlReader.Name.Equals("PHONENUMBER") && (xmlReader.Value.Equals(student.PhoneNumber) || student.PhoneNumber == null))
                                                {
                                                    phoneNumber = xmlReader.Value;
                                                    xmlReader.MoveToNextAttribute();
                                                    if (xmlReader.Name.Equals("PAYMENTSTATUS") && (xmlReader.Value.Equals(student.PaymentStatus) || student.PaymentStatus == null))
                                                    {
                                                        paymentStatus = xmlReader.Value;
                                                        xmlReader.MoveToNextAttribute();
                                                        if (xmlReader.Name.Equals("AVAILABILITYOFBENEFITS") && (xmlReader.Value.Equals(student.AvailabilityOfBenefits) || student.AvailabilityOfBenefits == null))
                                                        {
                                                            availabilityOfBenefits = xmlReader.Value;
                                                            xmlReader.MoveToNextAttribute();
                                                            if (xmlReader.Name.Equals("SUBORDINATION") && (xmlReader.Value.Equals(student.Subordination) || student.Subordination == null))
                                                            {
                                                                subordination = xmlReader.Value;
                                                                xmlReader.MoveToNextAttribute();
                                                                if (xmlReader.Name.Equals("ADDRESS") && (xmlReader.Value.Equals(student.Address) || student.Address == null))
                                                                {
                                                                    address = xmlReader.Value;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (name != "" && faculty != "" && department != "" && course != "" && roomNumber != "" && phoneNumber != "" && paymentStatus != "" && availabilityOfBenefits != "" && subordination != "" && address != "")
                            {
                                Student newStudent = new Student { Name = name, Department = department, Faculty = faculty, Course = course, RoomNumber = roomNumber, PhoneNumber = phoneNumber, PaymentStatus = paymentStatus, AvailabilityOfBenefits = availabilityOfBenefits, Subordination = subordination, Address = address };
                                results.Add(newStudent);
                            }
                        }
                    }
                }
                xmlReader.Close();
                return results;
            }
        }

        public class Dom : IStrategy
        {
            public List<Student> Search(Student student)
            {
                List<Student> results = new List<Student>();
                XmlDocument doc = new XmlDocument();

                try
                {
                    doc.Load(xmlFilePath);
                }
                catch
                {
                    return null;
                }

                XmlNode node = doc.DocumentElement;
                foreach (XmlNode n in node.ChildNodes)
                {
                    string name = "";
                    string faculty = "";
                    string department = "";
                    string course = "";
                    string roomNumber = "";
                    string phoneNumber = "";
                    string paymentStatus = "";
                    string availabilityOfBenefits = "";
                    string subordination = "";
                    string address = "";

                    foreach (XmlAttribute attribute in n.Attributes)
                    {
                        if (attribute.Name.Equals("NAME") && (attribute.Value.Equals(student.Name) || student.Name == null))
                            name = attribute.Value;
                        if (attribute.Name.Equals("FACULTY") && (attribute.Value.Equals(student.Faculty) || student.Faculty == null))
                            faculty = attribute.Value;
                        if (attribute.Name.Equals("DEPARTMENT") && (attribute.Value.Equals(student.Department) || student.Department == null))
                            department = attribute.Value;
                        if (attribute.Name.Equals("COURSE") && (attribute.Value.Equals(student.Course) || student.Course == null))
                            course = attribute.Value;
                        if (attribute.Name.Equals("ROOMNUMBER") && (attribute.Value.Equals(student.RoomNumber) || student.RoomNumber == null))
                            roomNumber = attribute.Value;
                        if (attribute.Name.Equals("PHONENUMBER") && (attribute.Value.Equals(student.PhoneNumber) || student.PhoneNumber == null))
                            phoneNumber = attribute.Value;
                        if (attribute.Name.Equals("PAYMENTSTATUS") && (attribute.Value.Equals(student.PaymentStatus) || student.PaymentStatus == null))
                            paymentStatus = attribute.Value;
                        if (attribute.Name.Equals("AVAILABILITYOFBENEFITS") && (attribute.Value.Equals(student.AvailabilityOfBenefits) || student.AvailabilityOfBenefits == null))
                            availabilityOfBenefits = attribute.Value;
                        if (attribute.Name.Equals("SUBORDINATION") && (attribute.Value.Equals(student.Subordination) || student.Subordination == null))
                            subordination = attribute.Value;
                        if (attribute.Name.Equals("ADDRESS") && (attribute.Value.Equals(student.Address) || student.Address == null))
                            address = attribute.Value;
                    }

                    if (name != "" && faculty != "" && department != "" && course != "" && roomNumber != "" && phoneNumber != "" && paymentStatus != "" && availabilityOfBenefits != "" && subordination != "" && address != "")
                    {
                        Student newStudent = new Student { Name = name, Department = department, Faculty = faculty, Course = course, RoomNumber = roomNumber, PhoneNumber = phoneNumber, PaymentStatus = paymentStatus, AvailabilityOfBenefits = availabilityOfBenefits, Subordination = subordination, Address = address };
                        results.Add(newStudent);
                    }
                }
                return results;
            }
        }

        public class Linq : IStrategy
        {
            public List<Student> Search(Student student)
            {
                List<Student> results = new List<Student>();
                XDocument doc;

                Debug.WriteLine(1);

                doc = XDocument.Load(xmlFilePath);

                var result = from obj in doc.Descendants("student")
                             where
                             (
                             (obj.Attribute("NAME").Value.Equals(student.Name) || student.Name == null) &&
                             (obj.Attribute("FACULTY").Value.Equals(student.Faculty) || student.Faculty == null) &&
                             (obj.Attribute("DEPARTMENT").Value.Equals(student.Department) || student.Department == null) &&
                             (obj.Attribute("COURSE").Value.Equals(student.Course) || student.Course == null) &&
                             (obj.Attribute("ROOMNUMBER").Value.Equals(student.RoomNumber) || student.RoomNumber == null) &&
                             (obj.Attribute("PHONENUMBER").Value.Equals(student.PhoneNumber) || student.PhoneNumber == null) &&
                             (obj.Attribute("PAYMENTSTATUS").Value.Equals(student.PaymentStatus) || student.PaymentStatus == null) &&
                             (obj.Attribute("AVAILABILITYOFBENEFITS").Value.Equals(student.AvailabilityOfBenefits) || student.AvailabilityOfBenefits == null) &&
                             (obj.Attribute("SUBORDINATION").Value.Equals(student.Subordination) || student.Subordination == null) &&
                             (obj.Attribute("ADDRESS").Value.Equals(student.Address) || student.Address == null)
                             )
                             select new
                             {
                                 name = (string)obj.Attribute("NAME"),
                                 faculty = (string)obj.Attribute("FACULTY"),
                                 department = (string)obj.Attribute("DEPARTMENT"),
                                 course = (string)obj.Attribute("COURSE"),
                                 roomNumber = (string)obj.Attribute("ROOMNUMBER"),
                                 phoneNumber = (string)obj.Attribute("PHONENUMBER"),
                                 paymentStatus = (string)obj.Attribute("PAYMENTSTATUS"),
                                 availabilityOfBenefits = (string)obj.Attribute("AVAILABILITYOFBENEFITS"),
                                 subordination = (string)obj.Attribute("SUBORDINATION"),
                                 address = (string)obj.Attribute("ADDRESS"),
                             };

                Debug.WriteLine(2);

                foreach (var p in result)
                {
                    Debug.WriteLine(3);
                    Student newStudent = new Student { Name = p.name, Department = p.department, Faculty = p.faculty, Course = p.course, RoomNumber = p.roomNumber, PhoneNumber = p.phoneNumber, PaymentStatus = p.paymentStatus, AvailabilityOfBenefits = p.availabilityOfBenefits, Subordination = p.subordination, Address = p.address };
                    results.Add(newStudent);
                }
                return results;
            }
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

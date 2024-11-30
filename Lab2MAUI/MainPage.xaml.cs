using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.Maui;


namespace Lab2MAUI
{
    public partial class MainPage : ContentPage
    {
        private string xmlFilePath = "";
        public MainPage()
        {
            InitializeComponent();
            SaxBtn.IsChecked = true;
        }

        private async void GetAllAuthors()
        {
            XmlDocument doc = new XmlDocument();
            var appLocation = Assembly.GetEntryAssembly().Location;
            var appPath = Path.GetDirectoryName(appLocation);
            Directory.SetCurrentDirectory(appPath);

            try
            {
                doc.Load(xmlFilePath);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Incorrect file!", "OK");
                return;
            }

            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("student");

            Debug.WriteLine(childNodes.Count);
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode n = childNodes.Item(i);
                addItems(n);
            }
        }

        private void addItems(XmlNode n)
        {
            if (!NamePicker.Items.Contains(n.SelectSingleNode("@NAME").Value))
                NamePicker.Items.Add(n.SelectSingleNode("@NAME").Value);
            if (!FacultyPicker.Items.Contains(n.SelectSingleNode("@FACULTY").Value))
                FacultyPicker.Items.Add(n.SelectSingleNode("@FACULTY").Value);
            if (!DepartmentPicker.Items.Contains(n.SelectSingleNode("@DEPARTMENT").Value))
                DepartmentPicker.Items.Add(n.SelectSingleNode("@DEPARTMENT").Value);
            if (!CoursePicker.Items.Contains(n.SelectSingleNode("@COURSE").Value))
                CoursePicker.Items.Add(n.SelectSingleNode("@COURSE").Value);
            if (!RoomNumberPicker.Items.Contains(n.SelectSingleNode("@ROOMNUMBER").Value))
                RoomNumberPicker.Items.Add(n.SelectSingleNode("@ROOMNUMBER").Value);
            if (!PhoneNumberPicker.Items.Contains(n.SelectSingleNode("@PHONENUMBER").Value))
                PhoneNumberPicker.Items.Add(n.SelectSingleNode("@PHONENUMBER").Value);
            if (!PaymentStatusPicker.Items.Contains(n.SelectSingleNode("@PAYMENTSTATUS").Value))
                PaymentStatusPicker.Items.Add(n.SelectSingleNode("@PAYMENTSTATUS").Value);
            if (!AvailabilityOfBenefitsPicker.Items.Contains(n.SelectSingleNode("@AVAILABILITYOFBENEFITS").Value))
                AvailabilityOfBenefitsPicker.Items.Add(n.SelectSingleNode("@AVAILABILITYOFBENEFITS").Value);
            if (!SubordinationPicker.Items.Contains(n.SelectSingleNode("@SUBORDINATION").Value))
                SubordinationPicker.Items.Add(n.SelectSingleNode("@SUBORDINATION").Value);
            if (!AddressPicker.Items.Contains(n.SelectSingleNode("@ADDRESS").Value))
                AddressPicker.Items.Add(n.SelectSingleNode("@ADDRESS").Value);
        }

        private void SearchBtnHandler(object sender, EventArgs e)
        {
            editor.Text = "";

            MauiProgram.Student student = GetSelectedParameters();
            MauiProgram.IStrategy analyzer = GetSelectedAnalyzer();
            PerformSearch(student, analyzer);
        }

        private MauiProgram.Student GetSelectedParameters()
        {
            MauiProgram.Student student = new MauiProgram.Student();

            if (NameCheckBox.IsChecked)
            {
                if (NamePicker.SelectedIndex != -1)
                    student.Name = NamePicker.SelectedItem.ToString();
                else
                    student.Name = "";
            }
            if (FacultyCheckBox.IsChecked)
            {
                if (FacultyPicker.SelectedIndex != -1)
                    student.Faculty = FacultyPicker.SelectedItem.ToString();
                else
                    student.Faculty = "";
            }
            if (DepartmentCheckBox.IsChecked)
            {
                if (DepartmentPicker.SelectedIndex != -1)
                    student.Department = DepartmentPicker.SelectedItem.ToString();
                else
                    student.Department = "";
            }
            if (CourseCheckBox.IsChecked)
            {
                if (CoursePicker.SelectedIndex != -1)
                    student.Course = CoursePicker.SelectedItem.ToString();
                else
                    student.Course = "";
            }
            if (RoomNumberCheckBox.IsChecked)
            {
                if (RoomNumberPicker.SelectedIndex != -1)
                    student.RoomNumber = RoomNumberPicker.SelectedItem.ToString();
                else
                    student.RoomNumber = "";
            }
            if (PhoneNumberCheckBox.IsChecked)
            {
                if (PhoneNumberPicker.SelectedIndex != -1)
                    student.PhoneNumber = PhoneNumberPicker.SelectedItem.ToString();
                else
                    student.PhoneNumber = "";
            }
            if (PaymentStatusCheckBox.IsChecked)
            {
                if (PaymentStatusPicker.SelectedIndex != -1)
                    student.PaymentStatus = PaymentStatusPicker.SelectedItem.ToString();
                else
                    student.PaymentStatus = "";
            }
            if (AvailabilityOfBenefitsCheckBox.IsChecked)
            {
                if (AvailabilityOfBenefitsPicker.SelectedIndex != -1)
                    student.AvailabilityOfBenefits = AvailabilityOfBenefitsPicker.SelectedItem.ToString();
                else
                    student.PaymentStatus = "";
            }
            if (SubordinationCheckBox.IsChecked)
            {
                if (SubordinationPicker.SelectedIndex != -1)
                    student.Subordination = SubordinationPicker.SelectedItem.ToString();
                else
                    student.Subordination = "";
            }
            if (AddressCheckBox.IsChecked)
            {
                if (AddressPicker.SelectedIndex != -1)
                    student.Address = AddressPicker.SelectedItem.ToString();
                else
                    student.Address = "";
            }

            return student;
        }

        private MauiProgram.IStrategy GetSelectedAnalyzer()
        {
            MauiProgram.IStrategy analyzer = null;

            try
            {
                if (SaxBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Sax();
                }
                if (DomBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Dom();
                }
                if (LinqBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Linq();
                }
            }
            catch
            {
                return null;
            }

            return analyzer;
        }

        private void PerformSearch(MauiProgram.Student student, MauiProgram.IStrategy analyzer)
        {
            MauiProgram.Searcher search = new MauiProgram.Searcher(student, analyzer, xmlFilePath);
            List<MauiProgram.Student> results = search.SearchAlgorithm();

            if (results == null) return;

            foreach (MauiProgram.Student p in results)
            {
                editor.Text += "Name: " + p.Name + "\n";
                editor.Text += "Faculty: " + p.Faculty + "\n";
                editor.Text += "Department: " + p.Department + "\n";
                editor.Text += "Course: " + p.Course + "\n";
                editor.Text += "Room Number: " + p.RoomNumber + "\n";
                editor.Text += "Phone Number: " + p.PhoneNumber + "\n";
                editor.Text += "Payment Status: " + p.PaymentStatus + "\n";
                editor.Text += "Availability of Benefits: " + p.AvailabilityOfBenefits + "\n";
                editor.Text += "Subordination: " + p.Subordination + "\n";
                editor.Text += "Address: " + p.Address + "\n";
                editor.Text += "\n";
            }
        }

        private void ClearFields(object sender, EventArgs e)
        {
            editor.Text = "";

            NameCheckBox.IsChecked = false;
            FacultyCheckBox.IsChecked = false;
            DepartmentCheckBox.IsChecked = false;
            CourseCheckBox.IsChecked = false;
            RoomNumberCheckBox.IsChecked = false;
            PhoneNumberCheckBox.IsChecked = false;
            PaymentStatusCheckBox.IsChecked = false;
            AvailabilityOfBenefitsCheckBox.IsChecked = false;
            SubordinationCheckBox.IsChecked = false;
            AddressCheckBox.IsChecked = false;

            NamePicker.SelectedItem = null;
            FacultyPicker.SelectedItem = null;
            DepartmentPicker.SelectedItem = null;
            CoursePicker.SelectedItem = null;
            RoomNumberPicker.SelectedItem = null;
            PhoneNumberPicker.SelectedItem = null;
            PaymentStatusPicker.SelectedItem = null;
            AvailabilityOfBenefitsPicker.SelectedItem = null;
            SubordinationPicker.SelectedItem = null;
            AddressPicker.SelectedItem = null;
        }

        private async void OnTransformToHTMLBtnClicked(object sender, EventArgs e)
        {
            XslCompiledTransform xct = LoadXSLT();
            if (xct == null) return;

            string xmlPath = xmlFilePath;

            if (xmlFilePath.Length == 0) return;
            string htmlPath = xmlFilePath.Substring(0, xmlFilePath.Length - 4) + ".html";

            XsltArgumentList xslArgs = await CreateXSLTArguments();

            TransformXMLToHTML(xct, xslArgs, xmlPath, htmlPath);

            await Application.Current.MainPage.DisplayAlert("Message", "File saved.", "Ok");
        }

        private async void OnOpenFileButton(object sender, EventArgs e)
        {
            var fileResult = await FilePicker.PickAsync();

            if (fileResult != null)
            {
                xmlFilePath = fileResult.FullPath;
            }

            editor.Text = "";

            GetAllAuthors();
        }

        private XslCompiledTransform LoadXSLT()
        {
            XslCompiledTransform xct = new XslCompiledTransform();

            try
            {
                xct.Load("C:\\Users\\admin\\Desktop\\Lab2-MAUI\\Lab2MAUI\\Transform.xslt");
            }
            catch { }

            return xct;
        }

        private Task CreateXSLError()
        {
            return Application.Current.MainPage.DisplayAlert("Error", "Fill in all attributes!", "Ok");
        }

        private async Task<XsltArgumentList> CreateXSLTArguments()
        {
            XsltArgumentList xslArgs = new XsltArgumentList();

            AddParamIfNotNull(xslArgs, "name", NamePicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "faculty", FacultyPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "department", DepartmentPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "course", CoursePicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "roomNumber", RoomNumberPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "phoneNumber", PhoneNumberPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "paymentStatus", PaymentStatusPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "availabilityOfBenefits", AvailabilityOfBenefitsPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "subordination", SubordinationPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "address", AddressPicker.SelectedItem?.ToString());

            xslArgs.AddParam("showName", "", NameCheckBox.IsChecked);
            xslArgs.AddParam("showFaculty", "", FacultyCheckBox.IsChecked);
            xslArgs.AddParam("showDepartment", "", DepartmentCheckBox.IsChecked);
            xslArgs.AddParam("showCourse", "", CourseCheckBox.IsChecked);
            xslArgs.AddParam("showRoomNumber", "", RoomNumberCheckBox.IsChecked);
            xslArgs.AddParam("showPhoneNumber", "", PhoneNumberCheckBox.IsChecked);
            xslArgs.AddParam("showPaymentStatus", "", PaymentStatusCheckBox.IsChecked);
            xslArgs.AddParam("showAvailabilityOfBenefits", "", AvailabilityOfBenefitsCheckBox.IsChecked);
            xslArgs.AddParam("showSubordination", "", SubordinationCheckBox.IsChecked);
            xslArgs.AddParam("showAddress", "", AddressCheckBox.IsChecked);

            return xslArgs;
        }

        private void AddParamIfNotNull(XsltArgumentList xslArgs, string name, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                xslArgs.AddParam(name, "", value);
            }
        }

        private void TransformXMLToHTML(XslCompiledTransform xct, XsltArgumentList xslArgs, string xmlPath, string htmlPath)
        {
            using (XmlReader xr = XmlReader.Create(xmlPath))
            {
                using (XmlWriter xw = XmlWriter.Create(htmlPath))
                {
                    try
                    {
                        xct.Transform(xr, xslArgs, xw);
                    }
                    catch { }
                }
            }
        }

        private async void OnExitBtnClicked(object sender, EventArgs e)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Exit", "Are you sure you want to exit the program?", "Yes", "No");
            if (result)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            int textLength = editor.Text.Length;
            int fontSize = CalculateFontSize(textLength);
            editor.FontSize = fontSize;
        }

        private int CalculateFontSize(int textLength)
        {
            if (textLength < 100)
            {
                return 18;
            }
            else if (textLength < 500)
            {
                return 14;
            }
            else
            {
                return 10;
            }
        }
    }
}
using FinalTask;

namespace FinalTaskTest
{
    [TestClass]
    public class FinalTaskUnitTests
    {
        [TestMethod]
        public void ReadFromTxtFileTest()
        {
            List<Doctor> doctors = new();
            doctors = DoctorWorkWithFile.ReadFromFile(doctors, "doctorsTest.txt");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 7);
        }

        [TestMethod]
        public void ReadFromJsonFileTest()
        {
            List<Doctor> doctors = new();
            doctors = DoctorWorkWithFile.ReadFromFile(doctors, "doctorsTest.json");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 7);
        }

        [TestMethod]
        public void ReadFromXmlFileTest()
        {
            List<Doctor> doctors = new();
            doctors = DoctorWorkWithFile.ReadFromFile(doctors, "doctorsTest.xml");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 3);
        }

        [TestMethod]
        public void WrongFileExtensionTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"Wrong file type is given as argument in Doctor.ReadFromFile(arg1, arg2).{Environment.NewLine}";
            DoctorWorkWithFile.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.abc");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ReadFromTxtFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"Format exception in .txt file on line 1: File should contain doctor`s specialty." +
                              $"{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            DoctorWorkWithFile.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.txt");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ReadFromJsonFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"Format exception in .json file on line 1: Cannot get the value of a token type 'String' as a number." +
                              $"{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            DoctorWorkWithFile.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.json");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ReadFromXmlFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"There is an error in XML document (6, 19).{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            DoctorWorkWithFile.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.xml");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void WriteToTxtFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("Name", "Surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.txt");
            DoctorWorkWithFile.WriteToTxtFile(doctors, "doctorsWriteTest.txt");
            Assert.IsTrue(new FileInfo("doctorsWriteTest.txt").Length != 0);
        }

        [TestMethod]
        public void WriteToJsonFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("Name", "Surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.json");
            DoctorWorkWithFile.WriteToJsonFile(doctors, "doctorsWriteTest.json");
            Assert.IsTrue(new FileInfo("doctorsWriteTest.json").Length != 0);
        }

        [TestMethod]
        public void WriteToXmlFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("Name", "Surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.xml");
            DoctorWorkWithFile.WriteToXmlFile(doctors, "doctorsWriteTest.xml");
            Assert.IsTrue(new FileInfo("doctorsWriteTest.xml").Length != 0);
        }

        [TestMethod]
        public void ToStringTest()
        {
            string expected = "Surgeon: Mike Anderson, 42, Male, 11, 10350.7, 4111";
            string actual = new Surgeon("Mike", "Anderson", 42, Gender.Male, 11, 10350.7, 4111).ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EqualsTest()
        {
            Surgeon doctor1 = new("Name", "Surname", 31, Gender.Male, 7, 6500, 2000);
            Surgeon doctor2 = new("Name", "Surname", 31, Gender.Male, 7, 6500, 2000);
            Assert.IsTrue(doctor1.Equals(doctor2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            Surgeon doctor1 = new("Name", "Surname", 31, Gender.Male, 7, 6500, 2000);
            Surgeon doctor2 = new("Othername", "Surname", 31, Gender.Male, 7, 6500, 2000);
            Assert.IsFalse(doctor1.Equals(doctor2));
        }
    }
}
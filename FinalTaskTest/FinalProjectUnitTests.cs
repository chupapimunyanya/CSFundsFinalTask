using FinalTask;

namespace FinalTaskTest
{
    [TestClass]
    public class FinalProjectUnitTests
    {
        [TestMethod]
        public void ReadFromTxtFileTest()
        {
            List<Doctor> doctors = new();
            doctors = Doctor.ReadFromFile(doctors, "doctorsTest.txt");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 7);
        }

        [TestMethod]
        public void ReadFromJsonFileTest()
        {
            List<Doctor> doctors = new();
            doctors = Doctor.ReadFromFile(doctors, "doctorsTest.json");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 7);
        }

        [TestMethod]
        public void ReadFromXmlFileTest()
        {
            List<Doctor> doctors = new();
            doctors = Doctor.ReadFromFile(doctors, "doctorsTest.xml");
            Assert.IsNotNull(doctors);
            Assert.IsTrue(doctors.Count == 7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongFileExtensionTest()
        {
            Doctor.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.abc");
        }

        [TestMethod]
        public void ReadFromTxtFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"Format exception on line 1: File should contain doctor`s specialty." +
                              $"{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            Doctor.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.txt");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ReadFromJsonFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"The JSON value could not be converted to System.Int32. Path: $.PatientsCount | LineNumber: 0 | BytePositionInLine: 19." +
                              $"Fix the file text and try again.{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            Doctor.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.json");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ReadFromXmlFileErrorTest()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            string expected = $"There is an error in XML document (12, 27).{Environment.NewLine}Total doctors added: 1{Environment.NewLine}";
            Doctor.ReadFromFile(new List<Doctor>(), "doctorsErrorTest.xml");
            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void WriteToTxtFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("name", "surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.txt");
            Doctor.WriteToTxtFile(doctors, "doctorsWriteTest.txt");
            Assert.IsTrue(new FileInfo("doctorsWriteTest.txt").Length != 0);
        }

        [TestMethod]
        public void WriteToJsonFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("name", "surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.json");
            Doctor.WriteToJsonFile(doctors, "doctorsWriteTest.json");
            Assert.IsTrue(new FileInfo("doctorsWriteTest.json").Length != 0);
        }

        [TestMethod]
        public void WriteToXmlFileTest()
        {
            List<Doctor> doctors = [new Surgeon(), new Surgeon("name", "surname", 31, Gender.Male, 7, 6500, 2000)];
            File.Delete("doctorsWriteTest.xml");
            Doctor.WriteToXmlFile(doctors, "doctorsWriteTest.xml");
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
            Surgeon doctor1 = new("name", "surname", 31, Gender.Male, 7, 6500, 2000);
            Surgeon doctor2 = new("name", "surname", 31, Gender.Male, 7, 6500, 2000);
            Assert.IsTrue(doctor1.Equals(doctor2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            Surgeon doctor1 = new("name", "surname", 31, Gender.Male, 7, 6500, 2000);
            Surgeon doctor2 = new("", "surname", 31, Gender.Male, 7, 6500, 2000);
            Assert.IsFalse(doctor1.Equals(doctor2));
        }
    }
}
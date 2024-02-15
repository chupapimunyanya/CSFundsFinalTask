using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;

namespace FinalTask
{
    public class DoctorWorkWithFile
    {

        public static List<Doctor> ReadFromFile(List<Doctor> doctors, string path)
        {
            int startCount = doctors.Count;
            try
            {
                if (!Enum.TryParse(Path.GetExtension(path)?.TrimStart('.'), out FileType fileType))
                {
                    throw new ArgumentException($"Wrong file type is given as argument in Doctor.ReadFromFile(arg1, arg2).");
                }

                switch (fileType)
                {
                    case FileType.txt:
                        DoctorWorkWithFile.ReadFromTxtFile(doctors, path);
                        break;
                    case FileType.json:
                        DoctorWorkWithFile.ReadFromJsonFile(doctors, path);
                        break;
                    case FileType.xml:
                        DoctorWorkWithFile.ReadFromXmlFile(doctors, path);
                        break;
                }
                Console.WriteLine($"Total doctors added: {doctors.Count - startCount}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return doctors;
        }

        protected static List<Doctor> ReadFromTxtFile(List<Doctor> doctors, string path)
        {
            List<string> lines = lines = File.ReadAllLines(path).ToList();
            int lineNum = 0;
            foreach (string line in lines)
            {
                lineNum++;
                try
                {
                    if (null != line)
                    {
                        doctors.Add(ReadFromTxt(line));
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Format exception in .txt file on line {lineNum}: {ex.Message}");
                    ProgramUtils._log.Info($"Format exception in .txt file on line {lineNum}: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument exception in .txt file on line {lineNum}: {ex.Message}");
                    ProgramUtils._log.Info($"Argument exception in .txt file on line {lineNum}: {ex.Message}");
                }
            }
            return doctors;
        }

        protected static Doctor ReadFromTxt(string line)
        {
            Doctor? doctor = null;

            switch (true)
            {
                case bool _ when line.Contains("Surgeon: "):
                    Surgeon s = new();
                    doctor = s.ReadFromTxt(line);
                    break;
                case bool _ when line.Contains("Pediatrician: "):
                    Pediatrician p = new();
                    doctor = p.ReadFromTxt(line);
                    break;
                case bool _ when line.Contains("Cardiologist: "):
                    Cardiologist c = new();
                    doctor = c.ReadFromTxt(line);
                    break;
                case bool _ when line.Contains("Neurologist: "):
                    Neurologist n = new();
                    doctor = n.ReadFromTxt(line);
                    break;
                default:
                    throw new FormatException("File should contain doctor`s specialty.");
            }

            return doctor ?? throw new FormatException($"Failed to read from .txt file. Check file format.");
        }

        protected static List<Doctor> ReadFromJsonFile(List<Doctor> doctors, string path)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            int lineNum = 0;
            foreach (string line in lines)
            {
                lineNum++;
                try
                {
                    if (null != line)
                    {
                        doctors.Add(ReadFromJson(line));
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Format exception in .json file on line {ex.LineNumber + 1}: {ex.InnerException.Message}");
                    ProgramUtils._log.Info(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Format exception in .json file on line {lineNum}: {ex.Message}");
                    ProgramUtils._log.Info($"Format exception in .json file on line {lineNum}: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument exception in .json file on line {lineNum}: {ex.Message}");
                    ProgramUtils._log.Info($"Argument exception in .json file on line {lineNum}: {ex.Message}");
                }
            }

            return doctors;
        }

        protected static Doctor ReadFromJson(string line)
        {
            Doctor? doctor = null;

            switch (true)
            {
                case var _ when line.Contains("OperationsCount"):
                    doctor = JsonSerializer.Deserialize<Surgeon>(line);
                    break;
                case var _ when line.Contains("PatientsCount"):
                    doctor = JsonSerializer.Deserialize<Pediatrician>(line);
                    break;
                case var _ when line.Contains("ProceduresCount"):
                    doctor = JsonSerializer.Deserialize<Cardiologist>(line);
                    break;
                case var _ when line.Contains("Specialty"):
                    doctor = JsonSerializer.Deserialize<Neurologist>(line);
                    break;
                default:
                    doctor = JsonSerializer.Deserialize<Doctor>(line);
                    break;
            }

            return doctor ?? throw new FormatException($"Failed to read from .json file. Check file format.");
        }

        protected static List<Doctor> ReadFromXmlFile(List<Doctor> doctors, string path)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.ReadToFollowing("Doctor"))
                {
                    try
                    {
                        doctors.Add(ReadFromXml(reader));
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Format exception in .xml file: {ex.Message}");
                        ProgramUtils._log.Info(ex.Message);
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine($"Exception in .xml file: {ex.Message}");
                        ProgramUtils._log.Info(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Argument exception in .xml file: {ex.Message}");
                        ProgramUtils._log.Info($"Argument exception in .xml file: {ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        ProgramUtils._log.Info(ex.Message);
                    }
                }
            }
            return doctors;
        }

        protected static Doctor ReadFromXml(XmlReader reader)
        {
            Doctor? doctor = null;
            XmlSerializer serializer = new(typeof(Doctor), new Type[] { typeof(Surgeon), typeof(Pediatrician) });
            doctor = (Doctor)serializer.Deserialize(reader);

            return doctor ?? throw new FormatException($"Failed to read from .xml file. Check file format.");
        }

        public static void WriteToFile(List<Doctor> doctors, string path)
        {
            if (!Enum.TryParse(Path.GetExtension(path)?.TrimStart('.'), out FileType fileType))
            {
                throw new ArgumentException($"Wrong file type is given as argument in Doctor.WriteToFile(arg1, arg2).");
            }

            switch (fileType)
            {
                case FileType.txt:
                    DoctorWorkWithFile.WriteToTxtFile(doctors, path);
                    break;
                case FileType.json:
                    DoctorWorkWithFile.WriteToJsonFile(doctors, path);
                    break;
                case FileType.xml:
                    DoctorWorkWithFile.WriteToXmlFile(doctors, path);
                    break;
            }
        }

        public static void WriteToTxtFile(List<Doctor> doctors, string path)
        {
            using (StreamWriter sw = new(path))
            {
                foreach (Doctor d in doctors)
                {
                    sw.WriteLine(d.ToString());
                }
            }
            Console.WriteLine($"Check out the .txt file at: {Path.GetFullPath(path)}.");
        }

        public static void WriteToJsonFile(List<Doctor> doctors, string path)
        {
            string jsonstring = "";

            foreach (Doctor d in doctors)
            {
                switch (d)
                {
                    case Pediatrician p:
                        jsonstring += p.WriteToJson() + "\n";
                        break;
                    case Surgeon s:
                        jsonstring += s.WriteToJson() + "\n";
                        break;
                    case Neurologist n:
                        jsonstring += n.WriteToJson() + "\n";
                        break;
                    case Cardiologist c:
                        jsonstring += c.WriteToJson() + "\n";
                        break;
                }
            }
            File.WriteAllText(path, jsonstring);
            Console.WriteLine($"Check out the .json file at: {Path.GetFullPath(path)}.");
        }

        public static void WriteToXmlFile(List<Doctor> doctors, string path)
        {
            XmlSerializer serializer = new(typeof(List<Doctor>));
            using (StreamWriter sr = new(path))
            {
                serializer.Serialize(sr, doctors);
            }
            Console.WriteLine($"Check out the .xml file at: {Path.GetFullPath(path)}.");
        }

        public enum FileType { txt = 0, json, xml }
    }
}

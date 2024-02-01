using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace FinalTask
{
    [Serializable]
    [XmlInclude(typeof(Surgeon))]
    [XmlInclude(typeof(Pediatrician))]
    public class Doctor : Person
    {
        public int WorkExp { get; }
        public double Salary { get; set; }

        public Doctor() : base()
        {
            WorkExp = 6;
            Salary = 6430.58;
        }
        public Doctor(string name, string surname, int age, Gender gender, int workExp, double salary) : base(name, surname, age, gender)
        {
            WorkExp = workExp;
            Salary = salary;
        }

        //TODO: Think on adding some methods/functionality as 'operations' or 'examine the patient'

        public static List<T> ReadFromFile<T>(List<T> doctors, string path) where T : Doctor, new()
        {
            int startCount = doctors.Count;
            try
            {
                if (!Enum.TryParse(Path.GetExtension(path)?.TrimStart('.'), out FileType fileType))
                {
                    throw new ArgumentException($"Wrong file type is given as argument in Doctor.ReadFromFile(arg1, arg2).");
                }
                List<string> lines = lines = File.ReadAllLines(path).ToList();
                int lineNum = 0;
                if (fileType == FileType.xml)
                {
                    using (XmlReader reader = XmlReader.Create(path))
                    {
                        while (reader.ReadToFollowing("Doctor"))
                        {
                            try
                            {
                                T d = new();
                                doctors.Add((T)ReadFromXml(reader));
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var line in lines)
                    {
                        lineNum++;
                        try
                        {
                            T d = new();
                            if (null != line)
                            {
                                switch (fileType)
                                {
                                    case FileType.txt:
                                        doctors.Add((T)d.ReadFromTxt(line));
                                        break;
                                    case FileType.json:
                                        doctors.Add((T)d.ReadFromJson(line));
                                        break;
                                }
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"{ex.Message}Fix the file text and try again.");
                            Program._log.Info(ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Format exception on line {lineNum}: {ex.Message}");
                            Program._log.Info($"Format exception on line {lineNum}: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Program._log.Info(ex.Message);
                        }
                    }
                }
                Console.WriteLine($"Total doctors added: {doctors.Count - startCount}");
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Program._log.Info(ex.Message);
            }
            return doctors;
        }
        protected virtual Doctor ReadFromTxt(string line)
        {
            Doctor? doctor = null;
            if (line.Contains("Surgeon: "))
            {
                Surgeon s = new();
                doctor = s.ReadFromTxt(line);
            }
            else if (line.Contains("Pediatrician: "))
            {
                Pediatrician p = new();
                doctor = p.ReadFromTxt(line);
            }
            else
            {
                throw new FormatException("File should contain doctor`s specialty.");
            }

            if (null == doctor)
            {
                throw new FormatException("Check your file format.");
            }
            return doctor;
        }

        protected virtual Doctor ReadFromJson(string line)// where T : Doctor
        {
            Doctor? doctor = null;
            if (line.Contains("OperationsCount"))
            {
                doctor = JsonSerializer.Deserialize<Surgeon>(line);
            }
            else if (line.Contains("PatientsCount"))
            {
                doctor = JsonSerializer.Deserialize<Pediatrician>(line);
            }
            else
            {
                doctor = JsonSerializer.Deserialize<Doctor>(line);
            }
            if (null == doctor)
            {
                throw new Exception($"Failed to read from .json.");
            }
            return doctor;
        }

        public static void WriteToTxtFile(List<Doctor> doctors, string path)
        {
            if (Path.GetExtension(path)?.TrimStart('.') != FileType.txt.ToString())
            {
                throw new ArgumentException($"Wrong file type is given as argument in Doctor.WriteToTxtFile(arg1, arg2).");
            }
            using (StreamWriter sw = new(path))
            {
                foreach (var d in doctors)
                {
                    sw.WriteLine(d.ToString());
                }
            }
            Console.WriteLine($"Check out the .txt file at: {Path.GetFullPath(path)}.");
        }

        public static void WriteToJsonFile<T>(List<T> doctors, string path) where T : Doctor, new()
        {
            if (Path.GetExtension(path)?.TrimStart('.') != FileType.json.ToString())
            {
                throw new ArgumentException($"Wrong file type is given as argument in Doctor.WriteToJsonFile(arg1, arg2).");
            }
            string jsonstring = "";
            foreach (var d in doctors)
            {
                jsonstring += d.WriteToJson() + "\n";
            }
            File.WriteAllText(path, jsonstring);
            Console.WriteLine($"Check out the .json file at: {Path.GetFullPath(path)}.");
        }

        protected virtual string WriteToJson()
        {
            return JsonSerializer.Serialize<Doctor>(this);
        }

        public static void WriteToXmlFile<T>(List<T> doctors, string path) where T : Doctor, new()
        {
            if (Path.GetExtension(path)?.TrimStart('.') != FileType.xml.ToString())
            {
                throw new ArgumentException($"Wrong file type is given as argument in Doctor.WriteToXmlFile(arg1, arg2).");
            }
            XmlSerializer serializer = new(typeof(List<Doctor>));
            using (StreamWriter sr = new(path))
            {
                serializer.Serialize(sr, doctors);
            }
            Console.WriteLine($"Check out the .xml file at: {Path.GetFullPath(path)}.");
        }

        protected static Doctor ReadFromXml(XmlReader reader)
        {
            Doctor? doctor = null;
            XmlSerializer serializer = new(typeof(Doctor), new Type[] { typeof(Surgeon), typeof(Pediatrician) });
            doctor = (Doctor)serializer.Deserialize(reader);
            if (null == doctor)
            {
                throw new FormatException();
            }
            return doctor;
        }

        public virtual new string ToString()
        {
            return $"Doctor: {Name}, {Age}, {Gender}, {WorkExp}, {Salary}";
        }

        public override bool Equals(object? o)
        {
            var item = o as Doctor;
            if (base.Equals(item) && WorkExp == item.WorkExp && Salary == item.Salary)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public enum FileType { txt = 0, json, xml }
}

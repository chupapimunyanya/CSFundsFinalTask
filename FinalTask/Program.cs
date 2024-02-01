using log4net;
using log4net.Config;

namespace FinalTask
{
    internal class Program
    {
        public static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("loggerConfig.xml"));
            _log.Info("Entering app.");
            do
            {
                try
                {
                    List<Doctor> doctors = new List<Doctor>();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.txt");

                    Console.WriteLine("--------------");

                    WriteRes1(doctors);
                    WriteRes2(doctors);
                    
                    Console.WriteLine("--------------");

                    doctors.Clear();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.json");
                    Doctor.WriteToJsonFile(doctors, "doctors.json");


                    Console.WriteLine("--------------");
                    
                    Doctor.WriteToXmlFile(doctors, "doctors.xml");
                    doctors.Clear();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.xml");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument exception: {ex.Message}");
                    _log.Info($"Argument exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _log.Info(ex.Message);
                }
            } while (Console.ReadLine() != "exit");
            _log.Info("Quitting app.");
        }

        static void WriteRes1(List<Doctor> doctors)
        {
            List<Doctor> res = new();
            int count = 0;
            foreach (Surgeon surgeon in doctors.Where(d => d is Surgeon).Where(d => d.WorkExp > 5))
            {
                if (surgeon.OperationsCount > 1000)
                {
                    res.Add(surgeon);
                    count++;
                }
            }
            Doctor.WriteToTxtFile(res, "result1.txt");
            Console.WriteLine($"Total amount of surgeons with work experience over 5 years and over 1000 operations is {count}.");
        }

        static void WriteRes2(List<Doctor> doctors)
        {
            List<Doctor> res = new();
            int count = 0;
            foreach (Pediatrician pediatrician in doctors.Where(d => d is Pediatrician))
            {
                if (pediatrician.PatientsCount > 1000)
                {
                    res.Add(pediatrician);
                    count++;
                }
            }
            Doctor.WriteToTxtFile(res, "result2.txt");
            Console.WriteLine($"Total number of pediatricians with over 1000 patients is {count}.");
        }
    }
}
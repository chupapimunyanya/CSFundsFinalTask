using log4net;
using log4net.Config;

namespace FinalTask
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("loggerConfig.xml"));
            ProgramUtils._log.Info("Entering app.");
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    List<Doctor> doctors = new List<Doctor>();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.txt");

                    ProgramUtils.BlockLine();

                    ProgramUtils.WriteRes1(doctors);
                    Console.WriteLine();
                    ProgramUtils.WriteRes2(doctors);

                    ProgramUtils.BlockLine();

                    doctors.Clear();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.json");
                    Doctor.WriteToJsonFile(doctors, "doctors.json");


                    ProgramUtils.BlockLine();
                    
                    Doctor.WriteToXmlFile(doctors, "doctors.xml");
                    doctors.Clear();
                    doctors = Doctor.ReadFromFile(doctors, "doctors.xml");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument exception: {ex.Message}");
                    ProgramUtils._log.Info($"Argument exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ProgramUtils._log.Info(ex.Message);
                }
                Console.ForegroundColor = ConsoleColor.Red;
            } while (Console.ReadLine() != "exit");
            ProgramUtils._log.Info("Quitting app.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
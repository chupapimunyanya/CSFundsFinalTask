using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using static FinalTask.Neurologist;

namespace FinalTask
{
    internal class ProgramUtils
    {

        public static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        internal static void WriteRes1(List<Doctor> doctors)
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
            DoctorWorkWithFile.WriteToTxtFile(res, "result1.txt");
            Console.WriteLine($"Total amount of surgeons with work experience over 5 years and over 1000 operations is {count}.");
        }

        internal static void WriteRes2(List<Doctor> doctors)
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
            DoctorWorkWithFile.WriteToTxtFile(res, "result2.txt");
            Console.WriteLine($"Total number of pediatricians with over 1000 patients is {count}.");
        }

        internal static void BlockLine()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--------------\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static string GetFileName()
        {
            Console.Write("Input the name of the file: ");
            return Console.ReadLine();
        }

        internal static Doctor AddDoctorManually()
        {
            Console.Write("Input doctor's specialty (Pediatrician/Surgeon/Neurologist/Cardiologist): ");

            if (!Enum.TryParse(Console.ReadLine(), out DoctorSpecialty specialty))
            {
                throw new ArgumentException("Invalid specialty.");
            }

            Doctor doctor = new();
            Console.Write($"Input {specialty}'s name: ");
            doctor.Name = Console.ReadLine();
            Console.Write($"Input {specialty}'s surname: ");
            doctor.Surname = Console.ReadLine();
            Console.Write($"Input {specialty}'s age: ");
            doctor.Age = int.Parse(Console.ReadLine());
            Console.Write($"Input {specialty}'s gender (Male/Female): ");
            doctor.Gender = (Gender)Enum.Parse(typeof(Gender),Console.ReadLine());
            Console.Write($"Input {specialty}'s work experience: ");
            doctor.WorkExp = int.Parse(Console.ReadLine());
            Console.Write($"Input {specialty}'s salary: ");
            doctor.Salary = double.Parse(Console.ReadLine());

            Doctor specializedDoctor;

            switch (specialty)
            {
                case DoctorSpecialty.Pediatrician:
                    specializedDoctor = AddPediatricianDetails(doctor);
                    break;

                case DoctorSpecialty.Surgeon:
                    specializedDoctor = AddSurgeonDetails(doctor);
                    break;

                case DoctorSpecialty.Neurologist:
                    specializedDoctor = AddNeurologistDetails(doctor);
                    break;

                case DoctorSpecialty.Cardiologist:
                    specializedDoctor = AddCardiologistDetails(doctor);
                    break;

                default:
                    throw new ArgumentException("Invalid specialty.");
            }

            Console.WriteLine($"Doctor was successfully added.");
            return specializedDoctor;
        }

        private static Pediatrician AddPediatricianDetails(Doctor doctor)
        {
            Console.Write("Input Pediatrician's patients count: ");
            Pediatrician pediatrician = new Pediatrician();
            CopyCommonDoctorProperties(doctor, pediatrician);
            pediatrician.PatientsCount = int.Parse(Console.ReadLine());
            return pediatrician;
        }

        private static Surgeon AddSurgeonDetails(Doctor doctor)
        {
            Console.Write("Input Surgeon's operations count: ");
            Surgeon surgeon = new Surgeon();
            CopyCommonDoctorProperties(doctor, surgeon);
            surgeon.OperationsCount = int.Parse(Console.ReadLine());
            return surgeon;
        }

        private static Neurologist AddNeurologistDetails(Doctor doctor)
        {
            Console.Write("Input Neurologist's specialty area (Practical/Research/Teaching): ");
            //Enum.TryParse(Console.ReadLine(), out SpecialtyArea specialtyArea);
            SpecialtyArea specialtyArea = (SpecialtyArea)Enum.Parse(typeof(SpecialtyArea), Console.ReadLine());
            Neurologist neurologist = new Neurologist();
            CopyCommonDoctorProperties(doctor, neurologist);
            neurologist.Specialty = specialtyArea;
            return neurologist;
        }

        private static Cardiologist AddCardiologistDetails(Doctor doctor)
        {
            Console.Write("Input Cardiologist's procedures count: ");
            Cardiologist cardiologist = new Cardiologist();
            CopyCommonDoctorProperties(doctor, cardiologist);
            cardiologist.ProceduresCount = int.Parse(Console.ReadLine());
            return cardiologist;
        }

        private static void CopyCommonDoctorProperties(Doctor source, Doctor destination)
        {
            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.Age = source.Age;
            destination.Gender = source.Gender;
            destination.WorkExp = source.WorkExp;
            destination.Salary = source.Salary;
        }

        internal static List<Doctor> RemoveDoctorById(List<Doctor> doctors)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"{i} - {doctors[i].ToString()}");
            }

            Console.WriteLine();
            Console.Write("Input doctor's id to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new ArgumentException("Wrong doctor id.");
            }
            if (id < 0 || id > doctors.Count - 1)
            {
                throw new ArgumentException("Wrong doctor id.");
            }
            doctors.RemoveAt(id);
            Console.WriteLine($"Doctor #{id} was successfully removed.");
            return doctors;
        }

        internal static void Work(List<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                Console.WriteLine($"{doctor.GetType().ToString().Substring("FinalTask.".Length)} {doctor.Name} {doctor.Surname}: ");
                doctor.Work();
                Console.WriteLine();
            }
        }

        internal static void PrintAllDoctors(List<Doctor> doctors)
        {
            Console.WriteLine("Doctors:");
            Console.WriteLine();
            foreach (Doctor doctor in doctors)
            {
                Console.WriteLine(doctor.ToString());
            }
        }

        internal static bool CheckIfListIsEmpty(List<Doctor> doctors)
        {
            if (doctors.Count == 0)
            {
                Console.WriteLine("There are no doctors yet.");
                return false;
            }
            return true;
        }

        internal static void IsNeededToClear()
        {
            Console.ReadKey();
            Console.Clear();
        }

        internal static StartMenu StartMenuFunc()
        {
            StartMenu item;
            do
            {
                Console.Write("1 - Add doctor(s)\n2 - Work\n3 - Print all doctors\n4 - Write doctors to .txt/.json/.xml file\n5 - Remove doctor by id\n0 - Exit\n\nChoose option: ");
            } while (!(StartMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(StartMenu), item)));
            return item;
        }

        internal static AddDoctorsMenu AddDoctorsMenuFunc()
        {
            AddDoctorsMenu item;
            do
            {
                Console.Write("1 - Read doctors from .txt/.json/.xml file\n2 - Add doctor manually\n0 - Exit\n\nChoose option: ");
            } while (!(AddDoctorsMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(AddDoctorsMenu), item)));
            return item;
        }
    }

    enum StartMenu { AddDoctors = 1, Work, PrintAll, WriteToFile, RemoveDoctorByID, Exit = 0 };
    enum AddDoctorsMenu { ReadFromFile = 1, AddManually, Exit = 0 };
}

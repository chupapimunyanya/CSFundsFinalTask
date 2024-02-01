using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Doctor.WriteToTxtFile(res, "result1.txt");
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
            Doctor.WriteToTxtFile(res, "result2.txt");
            Console.WriteLine($"Total number of pediatricians with over 1000 patients is {count}.");
        }

        internal static void BlockLine()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--------------\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

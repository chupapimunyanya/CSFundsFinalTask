﻿using log4net;
using log4net.Config;

namespace FinalTask
{
    internal class Program
    {

        static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure(new FileInfo("loggerConfig.xml"));
                ProgramUtils._log.Info("Entering app.");

                List<Doctor> doctors = new();
                StartMenu startMenuItem;
                AddDoctorsMenu addDoctorsMenuItem;

                do
                {
                    startMenuItem = ProgramUtils.StartMenuFunc();
                    Console.Clear();
                    switch (startMenuItem)
                    {
                        case StartMenu.AddDoctors:

                            do
                            {
                                //Console.Clear();
                                addDoctorsMenuItem = ProgramUtils.AddDoctorsMenuFunc();
                                Console.Clear();
                                try
                                {
                                    switch (addDoctorsMenuItem)
                                    {
                                        case AddDoctorsMenu.ReadFromFile:
                                            try
                                            {
                                                doctors = DoctorWorkWithFile.ReadFromFile(doctors, ProgramUtils.GetFileName());
                                                ProgramUtils.IsNeededToClear();
                                            }
                                            catch (FileNotFoundException ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                                ProgramUtils._log.Info(ex.Message);
                                                ProgramUtils.IsNeededToClear();
                                            }
                                            break;
                                        case AddDoctorsMenu.AddManually:
                                            doctors.Add(ProgramUtils.AddDoctorManually());
                                            ProgramUtils.IsNeededToClear();
                                            break;
                                    }

                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    ProgramUtils._log.Info(ex.Message);
                                    ProgramUtils.IsNeededToClear();
                                }
                            } while (addDoctorsMenuItem != AddDoctorsMenu.Exit);

                            Console.Clear();
                            break;

                        case StartMenu.Work:
                            if (ProgramUtils.CheckIfListIsEmpty(doctors))
                            {
                                ProgramUtils.Work(doctors);
                            }
                            ProgramUtils.IsNeededToClear();
                            break;

                        case StartMenu.PrintAll:
                            if (ProgramUtils.CheckIfListIsEmpty(doctors))
                            {
                                ProgramUtils.PrintAllDoctors(doctors);
                            }
                            ProgramUtils.IsNeededToClear();
                            break;

                        case StartMenu.WriteToFile:
                            try
                            {
                                if (ProgramUtils.CheckIfListIsEmpty(doctors))
                                {
                                    DoctorWorkWithFile.WriteToFile(doctors, ProgramUtils.GetFileName());
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                ProgramUtils._log.Info(ex.Message);
                                //ProgramUtils.IsNeededToClear();
                            }
                            ProgramUtils.IsNeededToClear();
                            break;

                        case StartMenu.RemoveDoctorByID:
                            try
                            {
                                if (ProgramUtils.CheckIfListIsEmpty(doctors))
                                {
                                    doctors = ProgramUtils.RemoveDoctorById(doctors);
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                ProgramUtils._log.Info(ex.Message);
                                //ProgramUtils.IsNeededToClear();
                            }
                            ProgramUtils.IsNeededToClear();
                            break;
                    }
                } while (startMenuItem != StartMenu.Exit);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("HAVE A GOOD DAY");
                Console.ForegroundColor = ConsoleColor.White;
                ProgramUtils._log.Info("Quitting app.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ProgramUtils._log.Info(ex.Message);
            }
        }
    }
}
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Pediatrician : Doctor
    {
        public int PatientsCount { get; set; }

        public Pediatrician() : base()
        {
            PatientsCount = 49;
        }

        public Pediatrician(string name, string surname, int age, Gender gender, int workExp, double salary, int patientsCount) : base(name, surname, age, gender, workExp, salary)
        {
            PatientsCount = patientsCount;
        }

        protected override Pediatrician? ReadFromTxt(string line)
        {
            string pattern = @"Pediatrician: (\w+) (\w+), (\d+), (\w+), (\d+), (\d+\.\d+), (\d+)";
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                string surname = match.Groups[2].Value;
                int age = int.Parse(match.Groups[3].Value);
                Enum.TryParse(match.Groups[4].Value, out Gender gender);
                int workExp = int.Parse(match.Groups[5].Value);
                double salary = double.Parse(match.Groups[6].Value);
                int patientsCount = int.Parse(match.Groups[7].Value);
                return new Pediatrician(name, surname, age, gender, workExp, salary, patientsCount);
            }
            return null;
        }

        protected override string WriteToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public override string ToString()
        {
            return $"Pediatrician: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {PatientsCount}";
        }

        public override bool Equals(object? o)
        {
            var item = o as Pediatrician;
            if (base.Equals(item) && PatientsCount == item.PatientsCount)
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
}

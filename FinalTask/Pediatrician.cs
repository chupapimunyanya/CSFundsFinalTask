using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Pediatrician : Doctor
    {
        [JsonPropertyName("PatientsCount")]

        private int _patientsCount;

        public int PatientsCount
        {
            get => _patientsCount;
            set
            {
                this.SetPatientsCount(value);
            }
        }

        private void SetPatientsCount(int value)
        {
            if (value >= 0 && value <= Age * 365)
            {
                _patientsCount = value;
                return;
            }
            throw new ArgumentException($"Invalid value of patients count.\nValid range for this person: [0..{Age * 365}].");
        }

        public Pediatrician() : base()
        {
            _patientsCount = 49;
        }

        [JsonConstructor]
        public Pediatrician(string name, string surname, int age, Gender gender, int workExp, double salary, int patientsCount) : base(name, surname, age, gender, workExp, salary)
        {
            PatientsCount = patientsCount;
        }

        internal Pediatrician? ReadFromTxt(string line)
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

        internal string WriteToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public override void Work()
        {
            Random random = new Random();
            Console.WriteLine(random.Next(100) > 90 ? "Can not diagnose the disease. Poor patient :(" : "Disease diagnosed. Patient will be fine :)");
        }

        public override string ToString()
        {
            return $"Pediatrician: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {_patientsCount}";
        }

        public override bool Equals(object? o)
        {
            Pediatrician? item = o as Pediatrician;
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

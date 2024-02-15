using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Cardiologist : Doctor
    {
        [JsonPropertyName("ProceduresCount")]

        private int _proceduresCount;

        public int ProceduresCount
        {
            get => _proceduresCount;
            set
            {
                this.SetProceduresCount(value);
            }
        }

        private void SetProceduresCount(int value)
        {
            if (value >= 0 && value <= Age * 365)
            {
                _proceduresCount = value;
                return;
            }
                throw new ArgumentException($"Invalid value of procedures count.\nValid range for this person: [0..{Age * 365}].");
        }

        public Cardiologist() : base()
        {
            _proceduresCount = 2500;
        }

        [JsonConstructor]
        public Cardiologist(string name, string surname, int age, Gender gender, int workExp, double salary, int proceduresCount) : base(name, surname, age, gender, workExp, salary)
        {
            ProceduresCount = proceduresCount;
        }

        internal Cardiologist? ReadFromTxt(string line)
        {
            string pattern = @"Cardiologist: (\w+) (\w+), (\d+), (\w+), (\d+), (\d+\.\d+), (\d+)";
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                string surname = match.Groups[2].Value;
                int age = int.Parse(match.Groups[3].Value);
                Gender gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());
                int workExp = int.Parse(match.Groups[5].Value);
                double salary = double.Parse(match.Groups[6].Value);
                int proceduresCount = int.Parse(match.Groups[7].Value);
                return new Cardiologist(name, surname, age, gender, workExp, salary, proceduresCount);
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
            Console.WriteLine(random.Next(100) > 90 ? "Patient will die in near future :(" : "Patient will be fine :)");
        }

        public override string ToString()
        {
            return $"Cardiologist: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {_proceduresCount}";
        }
    }
}

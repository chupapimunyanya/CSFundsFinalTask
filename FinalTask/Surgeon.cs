using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Surgeon : Doctor
    {
        private int _operationsCount;

        [JsonPropertyName("OperationsCount")]
        public int OperationsCount {
            get => _operationsCount;
            set
            {
                if (value >= 0 && value <= Age * 365)
                {
                    _operationsCount = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid value of operations count.\nValid range for this person: [0..{Age * 365}].");
                }
            }
        }

        public Surgeon() : base()
        {
            _operationsCount = 1001;
        }

        [JsonConstructor]
        public Surgeon(string name, string surname, int age, Gender gender, int workExp, double salary, int operationsCount) : base(name, surname, age, gender, workExp, salary)
        {
            OperationsCount = operationsCount;
        }

        internal Surgeon? ReadFromTxt(string line)
        {
            string pattern = @"Surgeon: (\w+) (\w+), (\d+), (\w+), (\d+), (\d+\.\d+), (\d+)";
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                return new Surgeon(match.Groups[1].Value,
                    match.Groups[2].Value,
                    int.Parse(match.Groups[3].Value),
                    (Gender)Enum.Parse(typeof(Gender), match.Groups[4].Value),
                    int.Parse(match.Groups[5].Value),
                    double.Parse(match.Groups[6].Value),
                    int.Parse(match.Groups[7].Value));
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
            Console.WriteLine(random.Next(100) > 90 ? "That was wrong limb. Poor patient :(" : "The Operation was successful. Patient will be fine :)");
        }

        public override string ToString()
        {
            return $"Surgeon: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {_operationsCount}";
        }

        public override bool Equals(object? o)
        {
            Surgeon? item = o as Surgeon;
            if (base.Equals(item) && OperationsCount == item.OperationsCount)
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

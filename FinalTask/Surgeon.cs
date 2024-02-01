using System.Text.Json;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Surgeon : Doctor
    {
        public int OperationsCount { get; set; }
        public Surgeon() : base()
        {
            OperationsCount = 1001;
        }
        public Surgeon(string name, string surname, int age, Gender gender, int workExp, double salary, int operationsCount) : base(name, surname, age, gender, workExp, salary)
        {
            OperationsCount = operationsCount;
        }

        protected override Surgeon? ReadFromTxt(string line)
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

        protected override string WriteToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public override string ToString()
        {
            return $"Surgeon: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {OperationsCount}";
        }

        public override bool Equals(object? o)
        {
            var item = o as Surgeon;
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

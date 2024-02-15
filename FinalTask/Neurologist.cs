using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FinalTask
{
    [Serializable]
    public class Neurologist : Doctor
    {
        [JsonPropertyName("Specialty")]
        public SpecialtyArea Specialty { get; set; }

        public Neurologist() : base()
        {
            Specialty = SpecialtyArea.Practical;
        }

        [JsonConstructor]
        public Neurologist(string name, string surname, int age, Gender gender, int workExp, double salary, SpecialtyArea specialty) : base(name, surname, age, gender, workExp, salary)
        {
            Specialty = specialty;
        }

        internal Neurologist? ReadFromTxt(string line)
        {
            string pattern = @"Neurologist: (\w+) (\w+), (\d+), (\w+), (\d+), (\d+\.\d+), (\w+)";
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                string surname = match.Groups[2].Value;
                int age = int.Parse(match.Groups[3].Value);
                Enum.TryParse(match.Groups[4].Value, out Gender gender);
                int workExp = int.Parse(match.Groups[5].Value);
                double salary = double.Parse(match.Groups[6].Value);
                Enum.TryParse(match.Groups[7].Value, out SpecialtyArea specialty);
                return new Neurologist(name, surname, age, gender, workExp, salary, specialty);
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
            Console.WriteLine(random.Next(100) > 90 ? "Should have eaten less burgers. Poor patient :(" : "Patient is in good health :)");
        }

        public override string ToString()
        {
            return $"Neurologist: {Name} {Surname}, {Age}, {Gender}, {WorkExp}, {Salary}, {Specialty}";
        }

        public enum SpecialtyArea { Practical = 0, Research, Teaching}
    }
}

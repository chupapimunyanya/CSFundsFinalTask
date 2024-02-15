using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace FinalTask
{
    [Serializable]
    [XmlInclude(typeof(Surgeon))]
    [XmlInclude(typeof(Pediatrician))]
    [XmlInclude(typeof(Neurologist))]
    [XmlInclude(typeof(Cardiologist))]
    public class Doctor : Person
    {

        private int _workExp;

        [JsonPropertyName("WorkExp")]
        public int WorkExp
        {
            get => _workExp;
            set
            {
                if(value <= Age-12 && value >= 0)
                {
                    _workExp = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid value of work experiance.\nValid range for this person: [0..{Age-12}].");
                }
            }
        }

        private double _salary;

        [JsonPropertyName("Salary")]
        public double Salary
        {
            get => _salary;
            set
            {
                if(value>100 && value <= 25000)
                {
                    _salary = value;
                }
                else
                {
                    throw new ArgumentException("Invalid value of salary.\nValid range: [100..25000].");
                }
            }
        }

        public Doctor() : base()
        {
            _workExp = 6;
            _salary = 6430.58;
        }

        public Doctor(string name, string surname, int age, Gender gender, int workExp, double salary) : base(name, surname, age, gender)
        {
            WorkExp = workExp;
            Salary = salary;
        }


        public virtual void Work()
        {
            Random random = new Random();
            Console.WriteLine(random.Next(100) > 90 ? "Good result." : "Bad result.");
        } 

        public override string ToString()
        {
            return $"Doctor: {Name}, {Age}, {Gender}, {_workExp}, {_salary}";
        }

        internal virtual string WriteToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public override bool Equals(object? o)
        {
            Doctor? item = o as Doctor;
            if (base.Equals(item) && _workExp == item.WorkExp && _salary == item.Salary)
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
    enum DoctorSpecialty { Pediatrician = 0, Surgeon, Neurologist, Cardiologist };
}

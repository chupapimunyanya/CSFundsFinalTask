using System.Text.RegularExpressions;

namespace FinalTask
{
    public class Person
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                this.SetName(value);
            }
        }

        private void SetName(string value)
        {
            if (Regex.IsMatch(value, @"^[A-Z][a-z]{1,}$"))
            {
                _name = value;
                return;
            }
            throw new ArgumentException("Wrong Name format.");
        }

        private string _surname;

        public string Surname
        {
            get => _surname;
            set
            {
                this.SetSurname(value);
            }
        }

        private void SetSurname(string value)
        {
            if (Regex.IsMatch(value, @"^[A-Z][a-z]{1,}$"))
            {
                _surname = value;
                return;
            }
            throw new ArgumentException("Wrong surname format.");
        }

        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                this.SetAge(value);
            }
        }

        private void SetAge(int value)
        {
            if (value >= 20 && value <= 92)
            {
                _age = value;
                return;
            }
            throw new ArgumentException("Invalid value for age.\nPerson is too young or too old to be a doctor.");
        }

        public Gender Gender { get; set; }

        public Person()
        {
            _name = string.Empty;
            _surname = string.Empty;
            _age = 0;
            Gender = Gender.Male;
        }


        public Person(string name, string surname, int age, Gender gender)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        public virtual new string ToString()
        {
            return $"Person: {_name}, {_age}, {Gender}";
        }

        public virtual new bool Equals(object? o)
        {
            Person? item = o as Person;
            if (item != null && _name.Equals(item.Name) && _surname.Equals(item.Surname) && _age == item.Age && Gender == item.Gender)
            {
                return true;
            }
            return false;
        }

        public virtual new int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum Gender { Male = 0, Female }
}

namespace FinalTask
{
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; }

        public Gender Gender { get; }

        public Person()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Age = 32;
            Gender = Gender.Male;
        }

        public Person(string name, string surname, int age, Gender gender)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        public virtual new bool Equals(object? o)
        {
            Person? item = o as Person;
            if (item!=null && Name.Equals(item.Name) && Surname.Equals(item.Surname) && Age == item.Age && Gender == item.Gender)
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

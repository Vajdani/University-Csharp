namespace HSZF_01.ConsoleUI.Models
{
    public class Student
    {
        public string Name { get; private set; }
        public string Neptun { get; private set; }
        public int Credit { get; private set; }
        public int Age { get; private set; }
        public bool IsMale { get; private set; }

        public Student(string name, string neptun, int credit, int age, bool isMale)
        {
            Name = name;
            Neptun = neptun;
            Credit = credit;
            Age = age;
            IsMale = isMale;
        }

        public override string ToString()
        {
            return $"{Name} ({Neptun}), Age: {Age}, Credit: {Credit}, Is Male: {(IsMale ? "yes" : "no")}";
        }
    }
}

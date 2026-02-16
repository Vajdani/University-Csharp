namespace SZFA_LAB01
{
    internal enum Species
    {
        Dog,
        Panda,
        Rabbit
    }

    internal class Animal
    {
        string name;
        bool sex;
        int weight;
        Species species;

        public string Name { get => name; }
        public bool Sex { get => sex; }
        public int Weight { get => weight; }
        internal Species Species { get => species; }

        public Animal(string name, bool sex, int weight, Species species)
        {
            this.name = name;
            this.sex = sex;
            this.weight = weight;
            this.species = species;
        }

        public override string ToString()
        {
            return $"{name} ({species} {(sex ? "boy" : "girl")}, {weight}kg)";  
        }
    }
}

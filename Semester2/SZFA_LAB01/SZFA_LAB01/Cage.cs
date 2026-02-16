namespace SZFA_LAB01
{
    internal class Cage
    {
        Animal[] animals;
        int animalCount;

        public int AnimalCount { get => animalCount; }

        public Cage(int count)
        {
            if (count > 10)
            {
                count = 10;
                Console.WriteLine("The given cage size was larger than 10, clamped.");
            }

            animals = new Animal[count];
        }

        public Cage(string filePath)
        {
            StreamReader reader = new(filePath);
            string[] rows = reader.ReadToEnd().Split('\n');
            int count = rows.Length;
            if (count > 10)
            {
                count = 10;
                Console.WriteLine("The given cage size was larger than 10, clamped.");
            }

            animals = new Animal[count];
            foreach (string data in rows)
            {
                string[] split = data.Split(',');
                Add(new Animal(split[0], split[1] == "fiú", int.Parse(split[2]), Enum.Parse<Species>(split[3])));
            }
        }

        public bool Add(Animal animal)
        {
            if (animalCount >= animals.Length)
            {
                Console.WriteLine("Failed to add animal, cage is full.");
                return false;
            }

            animals[animalCount++] = animal;

            return true;
        }

        public void Delete(string name)
        {
            Animal[] animalNew = new Animal[animals.Length];
            int count = 0;
            for (int i = 0; i < animals.Length; i++)
            {
                if (animals[i].Name != name)
                {
                    animalNew[count++] = animals[i];
                }
            }

            animals = animalNew;
            animalCount--;
        }

        public int CountOfSpecies(Species species)
        {
            return animals.Count(x => x.Species == species);
        }

        public bool AnimalExists(Species species, bool sex)
        {
            foreach (Animal animal in animals)
            {
                if (animal.Species == species && animal.Sex == sex)
                {
                    return true;
                }
            }

            return false;
        }

        public Animal[] FindBySpecies(Species species)
        {
            return [.. animals.Where(x => x.Species == species)];
        }

        public float AverageWeightOfSpecies(Species species)
        {
            Animal[] ofSpecies = FindBySpecies(species);
            return ofSpecies.Sum(x => x.Weight) / (float)ofSpecies.Length;
        }

        public bool AnimalSexPairOfSpeciesExists(Species species)
        {
            Animal? half = Array.Find(animals, x => x.Species == species);
            if (half == null)
            {
                return false;
            }

            foreach (Animal animal in animals)
            {
                if (half.Species == animal.Species && half.Sex != animal.Sex)
                {
                    return true;
                }
            }

            return false;
        }

        public static int WhichCageContainsMostOf(Cage[] cages, Species species)
        {
            int index = 0;
            int count = cages[0].animals.Count(x => x.Species == species);
            for (int i = 1; i < cages.Length; i++)
            {
                int nextCount = cages[i].animals.Count(x => x.Species == species);
                if (nextCount > count)
                {
                    index = i;
                }
            }

            return index;
        }

        public void PrintContents()
        {
            Console.WriteLine("Cage contents:");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"\t{animal}");
            }
        }

        public static Cage[] MakeZooFromFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.txt");
            int fileCount = files.Length;
            if (fileCount == 0)
            {
                return [];
            }

            Cage[] cages = new Cage[fileCount];
            for (int i = 0; i < fileCount; i++)
            {
                cages[i] = new Cage(files[i]);
            }

            return cages;
        }
    }
}

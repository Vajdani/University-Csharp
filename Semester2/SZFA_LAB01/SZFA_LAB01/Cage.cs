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
            for (int i = 0; i < animalCount; i++)
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
            int count = 0;
            foreach (Animal animal in animals)
            {
                if (animal.Species == species)
                {
                    count++;
                }
            }

            return count;
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
            List<Animal> found = [];
            foreach (Animal animal in animals)
            {
                if (animal.Species == species)
                {
                    found.Add(animal);
                }
            }

            return [.. found];
        }

        public float AverageWeightOfSpecies(Species species)
        {
            int speciesCount = 0;
            float weight = 0;
            foreach (Animal animal in animals)
            {
                if (animal.Species == species)
                {
                    speciesCount++;
                    weight += animal.Weight;
                }
            }

            return weight / speciesCount;
        }

        public bool AnimalSexPairOfSpeciesExists(Species species)
        {
            int index = 0;
            while (index < animalCount && animals[index].Species != species)
            {
                index++;
            }
            
            if (index >= animalCount)
            {
                return false;
            }

            Animal half = animals[index];
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
            int count = cages[0].CountOfSpecies(species);
            for (int i = 1; i < cages.Length; i++)
            {
                int nextCount = cages[i].CountOfSpecies(species);
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

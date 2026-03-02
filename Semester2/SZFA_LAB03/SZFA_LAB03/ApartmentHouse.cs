namespace SZFA_LAB03
{
    internal class ApartmentHouse
    {
        Flat[] flats;
        Garage[] garages;

        int flatCount;
        int garageCount;
        int maxFlatCount;
        int maxGarageCount;

        public int InhabitantsCount {
            get {
                int sum = 0;
                foreach (Flat flat in flats)
                {
                    sum += flat.InhabitantCount;
                }

                return sum;
            }
        }

        internal Flat[] Flats { get => flats; set => flats = value; }
        internal Garage[] Garages { get => garages; set => garages = value; }

        public ApartmentHouse(int maxFlatCount, int maxGarageCount)
        {
            this.maxFlatCount = maxFlatCount;
            this.maxGarageCount = maxGarageCount;
        
            flats = new Flat[maxFlatCount];
            garages = new Garage[maxGarageCount];
        }

        public bool AddUnit(object obj)
        {
            if (obj is Flat flat)
            {
                if (flatCount + 1 >  maxFlatCount) { return false; }

                flats[flatCount++] = flat;

                return true;
            }
            else if (obj is Garage garage)
            {
                if (garageCount + 1 > maxGarageCount) { return false; }

                garages[garageCount++] = garage;

                return true;
            }

            return false;
        }

        public float TotalValue()
        {
            float sum = 0;
            foreach (Flat flat in flats)
            {
                if (flat.InhabitantCount > 0)
                {
                    sum += flat.TotalValue();
                }
            }

            foreach (Garage garage in garages)
            {
                if (garage.IsBooked())
                {
                    sum += garage.TotalValue();
                }
            }

            return sum;
        }

        public static ApartmentHouse LoadFromFile(string fileName)
        {
            StreamReader reader = new(fileName);
            List<Flat> flats = [];
            List<Garage> garages = [];

            while (!reader.EndOfStream)
            {
                string[] split = reader.ReadLine()!.Split(' ');
                switch(split[0])
                {
                    case "Alberlet":
                        flats.Add(
                            new Lodgings(float.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]))
                        );
                        break;
                    case "CsaladiApartman":
                        flats.Add(new FamilyApartment(float.Parse(split[1]), 2, int.Parse(split[2]), int.Parse(split[3])));
                        break;
                    case "Garazs":
                        garages.Add(new Garage(float.Parse(split[1]), int.Parse(split[2]), split[3] == "futott"));
                        break;
                }
            }

            ApartmentHouse apartment = new(flats.Count, garages.Count);
            foreach (Flat flat in flats)
            {
                apartment.AddUnit(flat);
            }

            foreach (Garage garage in garages)
            {
                apartment.AddUnit(garage);
            }


            return apartment;
        }
    }
}

namespace PMP_LAB09
{
    internal class Player
    {
        Position position;
        ConsoleSprite sprite;
        double fillingRatio;
        int health;
        int ammo;
        bool alive;
        int combatPoints;
        int sightRange;
        int minDamage;
        int maxDamage;
        int bfgCells;

        public Position Position { get { return position; } set { position = value; } }
        public ConsoleSprite Sprite { get { return sprite; } }
        public double FillingRatio { get { return fillingRatio; } }
        public int Health { get { return health; }
            set
            { 
                if (value <= MaxHealth && value >= 0)
                {
                    health = value;
                }
            }
        }
        public int Ammo { get { return ammo; }
            set
            {
                if (value <= MaxAmmo && value >= 0)
                {
                    ammo = value;
                }
            }
        }
        public bool Alive { get { return alive; } }
        public int CombatPoints { get { return combatPoints; } }
        public int MaxHealth { get { return 100 + combatPoints / 10; } }
        public int MaxAmmo { get { return 10 + combatPoints / 50; } }
        public int SightRange { get { return sightRange; } }
        public int BFGCells { get { return bfgCells; } }
        public int MaxBFGCells { get { return 3; } }

        public Player(int x, int y)
        {
            position = new(x, y);
            sprite = new(ConsoleColor.Black, ConsoleColor.Green, '0');
            fillingRatio = 0.4;
            health = 100;
            sightRange = 8;
            alive = true;
            ammo = 10;
            minDamage = 35;
            maxDamage = 105;
        }

        public void Shoot()
        {
            ammo--;
        }

        public void ShootBFG()
        {
            bfgCells--;
        }

        public void AddCombatPoints(int points)
        {
            combatPoints += points;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                alive = false;
            }
        }

        public void PickUpAmmo(int ammo)
        {
            this.ammo = Math.Min(this.ammo + ammo, MaxAmmo);
        }

        public void PickUpHealth(int health)
        {
            this.health = Math.Min(this.health + health, MaxHealth);
        }

        public void PickUpBFGCell()
        {
            bfgCells++;
        }



        public int GetDamage()
        {
            return Random.Shared.Next(minDamage, maxDamage + 1);
        }

        public int GetBFGDamage()
        {
            return Random.Shared.Next(100, 801);
        }
    }
}

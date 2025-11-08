namespace PMP_LAB09
{
    internal class Demon
    {
        Position position;
        ConsoleSprite sprite;
        DemonType type;
        double fillingRatio;
        int health;
        bool alive;
        int sightRange;
        int attackRange;
        DemonStateType state;
        int minDamage;
        int maxDamage;

        public Position Position { get { return position; } set { position = value; } }
        public ConsoleSprite Sprite { get { return sprite; } }
        public double FillingRatio { get { return fillingRatio; } }
        public DemonType Type { get { return type; } }
        public int Health { get { return health; }
            set {
                if (value > 0) { health = value; }
            }
        }
        public bool Alive { get { return alive; } }
        public int SightRange { get { return sightRange; } }
        public int AttackRange { get { return attackRange; } }
        public DemonStateType State { get { return state; } set { state = value; } }

        public Demon(int x, int y, DemonType type)
        {
            position = new(x, y);
            this.type = type;
            state = DemonStateType.Idle;
            alive = true;

            SetInitialProperties();
        }

        void SetInitialProperties()
        {
            switch (type)
            {
                case DemonType.Zombieman:
                    fillingRatio = 0.4;
                    health = 20;
                    sightRange = 3;
                    attackRange = 1;
                    sprite = new(ConsoleColor.Black, ConsoleColor.White, 'o');
                    minDamage = 3;
                    maxDamage = 15;
                    break;
                case DemonType.Imp:
                    fillingRatio = 0.4;
                    health = 60;
                    sightRange = 6;
                    attackRange = 3;
                    sprite = new(ConsoleColor.Black, ConsoleColor.Red, 'o');
                    minDamage = 3;
                    maxDamage = 24;
                    break;
                case DemonType.Mancubus:
                    fillingRatio = 0.96;
                    health = 600;
                    sightRange = 9;
                    attackRange = 6;
                    sprite = new(ConsoleColor.Black, ConsoleColor.Magenta, '0');
                    minDamage = 8;
                    maxDamage = 64;
                    break;
            }
        }

        public void UpdateState(Player player)
        {
            double distance = Position.Distance(position, player.Position);
            if (distance <= attackRange)
            {
                state = DemonStateType.Attack;
            }
            else if (distance <= sightRange)
            {
                state = DemonStateType.Move;
            }
            else
            {
                state = DemonStateType.Idle;
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                alive = false;
            }
        }

        public int GetDamage()
        {
            return Random.Shared.Next(minDamage, maxDamage + 1);
        }
    }
}

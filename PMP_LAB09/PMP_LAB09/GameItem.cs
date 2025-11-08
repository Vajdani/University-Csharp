namespace PMP_LAB09
{
    internal class GameItem
    {
        Position position;
        ConsoleSprite sprite;
        ItemType type;
        double fillingRatio;
        bool available;

        public Position Position { get { return position; } set { position = value; } }
        public ConsoleSprite Sprite { get { return sprite; } set { sprite = value; } }
        public ItemType Type { get { return type; } }
        public double FillingRatio { get { return fillingRatio; } }
        public bool Available { get { return available; } set { available = value; } }

        public GameItem(int x, int y, ItemType type)
        {
            position = new(x, y);
            this.type = type;
            available = true;

            SetInitialProperties();
        }
    
        void SetInitialProperties()
        {
            switch (type)
            {
                case ItemType.Ammo:
                    sprite = new(ConsoleColor.Red, ConsoleColor.Yellow, 'A');
                    break;
                case ItemType.BFGCell:
                    sprite = new(ConsoleColor.Green, ConsoleColor.White, 'B');
                    break;
                case ItemType.Door:
                    fillingRatio = 1;
                    sprite = new(ConsoleColor.Gray, ConsoleColor.Yellow, '/');
                    break;
                case ItemType.LevelExit:
                    fillingRatio = 1;
                    sprite = new(ConsoleColor.Blue, ConsoleColor.Black, 'E');
                    break;
                case ItemType.Medikit:
                    sprite = new(ConsoleColor.Gray, ConsoleColor.Red, '+');
                    break;
                case ItemType.ToxicWaste:
                    sprite = new(ConsoleColor.Green, ConsoleColor.Gray, ':');
                    break;
                case ItemType.Wall:
                    fillingRatio = 1;
                    sprite = new(ConsoleColor.Gray, ConsoleColor.Gray, '|');
                    break;
            }
        }
    
        public void Interact()
        {
            switch (type)
            {
                case ItemType.Ammo:
                    available = false;
                    break;
                case ItemType.BFGCell:
                    available = false;
                    break;
                case ItemType.Door:
                    if (fillingRatio == 0)
                    {
                        fillingRatio = 1;
                        sprite = new(ConsoleColor.Gray, ConsoleColor.Yellow, '/');
                    }
                    else
                    {
                        fillingRatio = 0;
                        sprite = new(ConsoleColor.Gray, ConsoleColor.Red, '/');
                    }

                    break;
                case ItemType.LevelExit:
                    break;
                case ItemType.Medikit:
                    available = false;
                    break;
            }
        }
    }
}

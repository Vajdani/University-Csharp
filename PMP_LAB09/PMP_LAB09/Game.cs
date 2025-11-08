using System.Diagnostics;
using WMPLib;

namespace PMP_LAB09
{
    internal class Game
    {
        Player player;
        bool exited;
        List<GameItem> items;
        List<Demon> demons;

        ConsoleRenderer renderer;
        GameLogic logic;

        Stopwatch stopwatchLogic;
        Stopwatch stopwatchRenderer;

        WindowsMediaPlayer musicPlayer;
        WindowsMediaPlayer sfxPlayer;

        public Player Player { get { return player; } }
        public bool Exited { get { return exited; } set { exited = value; } }
        public List<GameItem > Items { get { return items; } }
        public List<Demon> Demons { get { return demons; } }

        public Game()
        {
            items = [];
            demons = [];

            renderer = new(this);
            logic = new(this);

            stopwatchLogic = new();
            stopwatchRenderer = new();

            musicPlayer = new();
            musicPlayer.settings.setMode("loop", true);

            sfxPlayer = new();
        }

        private void UserAction()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            ConsoleKeyInfo pressed = Console.ReadKey(true);
            switch (pressed.Key)
            {
                case ConsoleKey.Escape:
                    Exited = true;
                    break;
                case ConsoleKey.LeftArrow:
                    if (player.Position.X == 0) { break; }
                    logic.Move(player, Position.Add(player.Position, new(-1, 0)));
                    break;
                case ConsoleKey.RightArrow:
                    if (player.Position.X == Console.WindowWidth - 1) { break; }
                    logic.Move(player, Position.Add(player.Position, new(1, 0)));
                    break;
                case ConsoleKey.UpArrow:
                    if (player.Position.Y == 0) { break; }
                    logic.Move(player, Position.Add(player.Position, new(0, -1)));
                    break;
                case ConsoleKey.DownArrow:
                    if (player.Position.Y == Console.WindowHeight - 1) { break; }
                    logic.Move(player, Position.Add(player.Position, new(0, 1)));
                    break;
                case ConsoleKey.D:
                    logic.PlayerDirectInteractionLogic();
                    break;
                case ConsoleKey.A:
                    logic.PlayerAttackLogic();
                    break;
                case ConsoleKey.S:
                    logic.PlayerBFGAttackLogic();
                    break;
            }

            logic.PlayerIndirectInteractionLogic();
        }

        public void Run()
        {
            //foreach (SoundEffectType type in Enum.GetValues<SoundEffectType>())
            //{
            //    PlaySoundEffect(type);
            //    Thread.Sleep(2000);
            //}

            //if (true)
            //{
            //    return;
            //}

            PlayMusic("sounds/doom_music.mp3");

            stopwatchLogic.Start();
            stopwatchRenderer.Start();

            while (!Exited && player.Alive)
            {
                //logic.UpdateGameState();
                //renderer.RenderGame();
                //UserAction();

                //Thread.Sleep(16);

                if (stopwatchLogic.ElapsedMilliseconds > 500)
                {
                    logic.UpdateGameState();
                    stopwatchLogic.Restart();
                }

                if (stopwatchRenderer.ElapsedMilliseconds > 25)
                {
                    renderer.RenderGame();
                    stopwatchRenderer.Restart();
                }

                UserAction();
            }

            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            if (player.Alive)
            {
                PlayMusic("sounds/level_complete.mp3");
            }
            else
            {
                PlaySoundEffect(SoundEffectType.PlayerDeath);
            }

            Console.ReadKey();
        }

        public void LoadMapFromPlainText(string path)
        {
            if (!Path.Exists(path))
            {
                throw new Exception($"Incorrect map file location: {path}");
            }

            StreamReader reader = new(path);
            string size = reader.ReadLine()!;
            if (size == "")
            {
                throw new Exception($"No map size found in map file: {path}");
            }

            string[] xy = size.Split(',');
            if (xy.Length != 2 ||
                !int.TryParse(xy[0], out int y) ||
                !int.TryParse(xy[1], out int x))
            {
                throw new Exception($"Map size must be 2 integers, got: {size} in {path}");
            }

            int row = -1;
            while (!reader.EndOfStream)
            {
                row++;
                if (row > y)
                {
                    throw new Exception($"Map file contains more rows than declared({y})");
                }

                string line = reader.ReadLine()!;
                if (line.Length != x)
                {
                    throw new Exception($"Map row is longer than declared({x}): {line}");
                }

                for (int i = 0; i < x; i++)
                {
                    switch (line[i])
                    {
                        case 'A':
                            items.Add(new(i, row, ItemType.Ammo));
                            break;
                        case 'B':
                            items.Add(new(i, row, ItemType.BFGCell));
                            break;
                        case 'D':
                            items.Add(new(i, row, ItemType.Door));
                            break;
                        case 'E':
                            items.Add(new(i, row, ItemType.LevelExit));
                            break;
                        case 'M':
                            items.Add(new(i, row, ItemType.Medikit));
                            break;
                        case 'T':
                            items.Add(new(i, row, ItemType.ToxicWaste));
                            break;
                        case 'W':
                            items.Add(new(i, row, ItemType.Wall));
                            break;
                        case 'z':
                            demons.Add(new(i, row, DemonType.Zombieman));
                            break;
                        case 'i':
                            demons.Add(new(i, row, DemonType.Imp));
                            break;
                        case 'm':
                            demons.Add(new(i, row, DemonType.Mancubus));
                            break;
                        case 'p':
                            player = new(i, row);
                            break;
                        case '_':
                            break;
                        default:
                            throw new Exception($"Unknown map tile: {line[i]}");
                    }
                }
            }

            if (player == null)
            {
                throw new Exception("Map doesn't contain player spawn point.");
            }

            //Console.SetWindowSize(x, y);

            reader.Close();
        }

        public void PlaySoundEffect(SoundEffectType type)
        {
            switch (type)
            {
                case SoundEffectType.BFG:
                    sfxPlayer.URL = "sounds/bfg.mp3";
                    break;
                case SoundEffectType.Door:
                    sfxPlayer.URL = "sounds/door.mp3";
                    break;
                case SoundEffectType.ItemPickup:
                    sfxPlayer.URL = "sounds/item_pickup.mp3";
                    break;
                case SoundEffectType.Pain:
                    sfxPlayer.URL = "sounds/pain.mp3";
                    break;
                case SoundEffectType.PlayerDeath:
                    sfxPlayer.URL = "sounds/player_death.mp3";
                    break;
                case SoundEffectType.Shotgun:
                    sfxPlayer.URL = "sounds/shotgun.mp3";
                    break;
            }

            sfxPlayer.controls.play();
        }

        public void PlayMusic(string path)
        {
            musicPlayer.URL = path;
            musicPlayer.controls.play();
        }
    }
}

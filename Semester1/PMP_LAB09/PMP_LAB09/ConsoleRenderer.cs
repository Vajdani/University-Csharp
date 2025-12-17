namespace PMP_LAB09
{
    internal class ConsoleRenderer
    {
        Game game;

        public ConsoleRenderer(Game game)
        {
            this.game = game;
        }

        void RenderSingleSprite(Position position, ConsoleSprite sprite)
        {
            if (Position.Distance(game.Player.Position, position) > game.Player.SightRange) { return; }

            if (position.X < 0 || position.X >= Console.WindowWidth ||
                position.Y < 0 || position.Y >= Console.WindowHeight)
            {
                return;
            }

            Console.SetCursorPosition(position.X, position.Y);
            Console.BackgroundColor = sprite.Background;
            Console.ForegroundColor = sprite.Foreground;
            Console.Write(sprite.Glyph);
        }

        void RenderHUD()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write($"HP: {game.Player.Health:000}/{game.Player.MaxHealth} | Ammo: {game.Player.Ammo:000}/{game.Player.MaxAmmo:000} | BFG: {game.Player.BFGCells}/{game.Player.MaxBFGCells}");
        }

        public void RenderGame()
        {
            Console.CursorVisible = false;
            Console.ResetColor();
            Console.Clear();

            for (int i = 0; i < game.Items.Count; i++)
            {
                GameItem item = game.Items[i];
                RenderSingleSprite(item.Position, item.Sprite);
            }

            for (int i = 0; i < game.Demons.Count; i++)
            {
                Demon demon = game.Demons[i];
                RenderSingleSprite(demon.Position, demon.Sprite);
            }

            RenderSingleSprite(game.Player.Position, game.Player.Sprite);

            RenderHUD();
        }
    }
}

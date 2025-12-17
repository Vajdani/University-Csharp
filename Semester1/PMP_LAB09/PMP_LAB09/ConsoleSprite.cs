namespace PMP_LAB09
{
    internal class ConsoleSprite
    {
        public ConsoleColor Background;
        public ConsoleColor Foreground;
        public char Glyph;

        public ConsoleSprite(ConsoleColor Background, ConsoleColor Foreground, char Glyph)
        { 
            this.Background = Background;
            this.Foreground = Foreground;
            this.Glyph = Glyph;
        }
    }
}

namespace PMP_F1_ZH1
{
    internal class Game
    {
        public string Title;
        public string Genre;
        public string Publisher;
        public string PlatformReleaseDate;
        public string OriginalReleaseDate;

        public Game(string line)
        {
            string[] split = line.Split(";");
            Title = split[0];
            Genre = Program.genres[int.Parse(split[1])];
            Publisher = split[2];
            PlatformReleaseDate = split[3];
            OriginalReleaseDate = split[4];
        }
    }
}

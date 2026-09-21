namespace HSZF_03.Models
{
    public class Movie
    {
        public string Title { get; set; }
        /// <summary>
        /// Length in minutes.
        /// </summary>
        public int Length { get; set; }
        public int Released { get; set; }

        public Movie() { }

        public Movie(string title, int length, int released)
        {
            Title = title;
            Length = length;
            Released = released;
        }

        public override string ToString()
        {
            return $"{Title} ({Released}) - {Length} minutes";
        }
    }
}

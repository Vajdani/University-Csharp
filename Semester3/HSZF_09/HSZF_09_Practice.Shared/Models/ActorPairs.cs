namespace HSZF_09_Practice.Shared.Models
{
    public class ActorPairs
    {
        public string FirstActorName { get; set; }
        public string SecondActorName { get; set; }
        public int CommonMoviesCount { get; set; }

        public override string ToString()
        {
            return $"Actor Pair: {FirstActorName} & {SecondActorName}, Common Movies count: {CommonMoviesCount}";
        }
    }
}

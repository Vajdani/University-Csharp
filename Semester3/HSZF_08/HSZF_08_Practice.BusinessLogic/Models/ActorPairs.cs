namespace HSZF_08_Practice.BusinessLogic.Models
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

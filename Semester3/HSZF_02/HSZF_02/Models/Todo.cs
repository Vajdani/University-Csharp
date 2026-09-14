namespace HSZF_02.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public Todo(int id, string title, bool isCompleted = false)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, IsCompleted: {IsCompleted}";
        }
    }
}

namespace HSZF_01.ConsoleUI.Models
{
    public class StudentMessage
    {
        static int IdCounter = 0;

        public int Id { get; private set; }
        public Student Sender { get; private set; }
        public Student Reciever { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Message { get; private set; }

        public StudentMessage(Student sender, Student reciever, string message, DateTime createdAt = default)
        {
            Id = IdCounter++;

            Sender = sender;
            Reciever = reciever;
            Message = message;
            CreatedAt = createdAt == default ? DateTime.Now : createdAt;
        }

        public override string ToString()
        {
            return $"[{CreatedAt}] {Sender.Name} to {Reciever.Name}: {Message}";
        }
    }
}

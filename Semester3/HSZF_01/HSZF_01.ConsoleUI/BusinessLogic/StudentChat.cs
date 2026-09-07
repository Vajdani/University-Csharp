using HSZF_01.ConsoleUI.Models;

namespace HSZF_01.ConsoleUI.BusinessLogic
{
    public class StudentChat
    {
        public Student Participant1 { get; private set; }
        public Student Participant2 { get; private set; }
        public DateTime StartedAt { get; private set; }
        public List<StudentMessage> Messages { get; private set; } = [];

        public StudentChat(Student participant1, Student participant2, DateTime startedAt = default)
        {
            Participant1 = participant1;
            Participant2 = participant2;
            StartedAt = startedAt == default ? DateTime.Now : startedAt;
        }

        public void SendMessage(Student sender, string message)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));

            // TODO: Check participant
            StudentMessage m = new(sender, sender == Participant1 ? Participant2 : Participant1, message);
            Messages.Add(m);
        }

        public void DisplayChat()
        {
            foreach (StudentMessage message in Messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}

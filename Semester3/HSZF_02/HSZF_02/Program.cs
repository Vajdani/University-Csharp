using HSZF_02.Managers;
using HSZF_02.Models;

namespace HSZF_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Todo todo1 = new(1, "Learn Git");
            Todo todo2 = new(2, "Learn Git again");
            Todo todo3 = new(3, "Learn C#", true);

            Console.WriteLine(todo1);
            Console.WriteLine(todo2);
            Console.WriteLine(todo3);

            TodoManager manager = new();
            manager.Add(todo1);
            manager.Add(todo2);
            manager.Add(todo3);
            manager.Add(new Todo(4, "Cooking"));

            DisplayTodos(manager);

            manager.UpdateTitle(2, "Learn movie making");

            DisplayTodos(manager);

            manager.MarkAsCompleted(2);
            
            DisplayTodos(manager);

            var todoForDelete = manager.GetById(4);
            Console.WriteLine($"Todo for delete: {todoForDelete}");
            
            manager.Delete(4);

            DisplayTodos(manager);
        }

        static void DisplayTodos(TodoManager manager)
        {
            Console.WriteLine("\nTodos list:\n");
            foreach (Todo todo in manager.GetAll())
            {
                Console.WriteLine(todo);
            }
        }
    }
}

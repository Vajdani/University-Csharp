using HSZF_02.Models;

namespace HSZF_02.Managers
{
    public class TodoManager
    {
        List<Todo> todos = [];

        public List<Todo> GetAll()
        {
            return todos;
        }

        public Todo? GetById(int id)
        {
            return todos.SingleOrDefault(t => t.Id == id);
        }

        public void Add(Todo todo)
        {
            todos.Add(todo);
        }

        public void Delete(int id)
        {
            Todo todo = GetById(id);
            if (todo is not null)
            {
                todos.Remove(todo);
            }
        }

        public void UpdateTitle(int id, string title)
        {
            Todo todo = GetById(id);
            if (todo is not null)
            {
                todo.Title = title;
            }
        }

        public void MarkAsCompleted(int id)
        {
            Todo todo = GetById(id);
            if (todo is not null)
            {
                todo.IsCompleted = true;
            }
        }
    }
}

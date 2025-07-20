using APiAplicaionesMoviles.Interfaces;
using APiAplicaionesMoviles.Models;
using Microsoft.EntityFrameworkCore;

namespace APiAplicaionesMoviles.Services
{
    public class ToDoService : ITodo
    {
        private readonly ToDoContext _context;

        public ToDoService(ToDoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ToDo> AddTodoAsync(ToDo todo)
        {
            return Task.Run(() =>
            {
                if (todo == null)
                {
                    throw new ArgumentNullException(nameof(todo));
                }
                _context.ToDos.Add(todo);
                _context.SaveChanges();
                return todo;
            });
        }

        public Task<bool> DeleteAllTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTodoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ToDo> GetTodoAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var todo = _context.ToDos.Find(id);
                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo with ID {id} not found.");
                }
                return todo;
            });
        }

        public async Task<List<ToDo>> GetTodosAsync()
        {
            return await _context.ToDos.ToListAsync();
        }

        public Task<bool> MarkTodoAsCompletedAsync(Guid id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with ID {id} not found.");
            }
            todo.Status = true;
            _context.ToDos.Update(todo);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> MarkTodoAsPendingAsync(Guid id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with ID {id} not found.");
            }
            todo.Status = false;
            _context.ToDos.Update(todo);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<ToDo> UpdateTodoAsync(ToDo todo)
        {
            throw new NotImplementedException();
        }
    }
}

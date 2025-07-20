using APiAplicaionesMoviles.Models;

namespace APiAplicaionesMoviles.Interfaces
{
    public interface ITodo
    {
        public Task<List<ToDo>> GetTodosAsync();
        public Task<ToDo> GetTodoAsync(Guid id);
        public Task<ToDo> AddTodoAsync(ToDo todo);
        public Task<ToDo> UpdateTodoAsync(ToDo todo);
        public Task<bool> DeleteTodoAsync(Guid id);
        public Task<bool> DeleteAllTodosAsync();
        public Task<bool> MarkTodoAsCompletedAsync(Guid id);
        public Task<bool> MarkTodoAsPendingAsync(Guid id);
    }
}

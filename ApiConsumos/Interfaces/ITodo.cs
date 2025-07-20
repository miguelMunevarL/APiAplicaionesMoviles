using ApiConsumos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumos.Interfaces
{
    public interface ITodo
    {
        public Task<List<Todo>> GetTodosAsync();
        public Task<Todo> GetTodoAsync(int id);
        public Task<Todo> CreateTodoAsync(Todo todo);

        public Task<bool> MarkTodoAsCompletedAsync(Guid id);
        public Task<bool> MarkTodoAsPendingAsync(Guid id);


    }
}

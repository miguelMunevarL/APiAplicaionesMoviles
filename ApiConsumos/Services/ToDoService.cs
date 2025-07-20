using ApiConsumos.Interfaces;
using ApiConsumos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumos.Services
{
    internal class ToDoService : ITodo
    {

        private readonly string _baseUrl = "";

        private readonly HttpClient _httpClient;
        public ToDoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://192.168.100.12:45455/";
        }
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl + "api/Todo", todo);
                if (response.IsSuccessStatusCode)
                {
                    var createdTodo = await response.Content.ReadFromJsonAsync<Todo>();
                    return createdTodo;
                }
                else
                {
                    throw new Exception($"Error creating todo: {response.ReasonPhrase}");
                }
            }
            catch
            {
                throw new Exception("An error occurred while creating the todo.");
            }
        }

        public Task<Todo> GetTodoAsync(int id)
        {
            try
            {
                var response = _httpClient.GetAsync(_baseUrl + "api/Todo/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var todo = response.Content.ReadFromJsonAsync<Todo>().Result;
                    return Task.FromResult(todo);
                }
                else
                {
                    throw new Exception($"Error fetching todo: {response.ReasonPhrase}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the todo: {ex.Message}", ex);
            }
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl+ "api/Todo");
                if (response.IsSuccessStatusCode)
                {
                    var todos = await response.Content.ReadFromJsonAsync<List<Todo>>();
                    return todos;
                }
                else
                {
                    throw new Exception($"Error fetching todos: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching todos: {ex.Message}", ex);
            }
        }

        public Task<bool> MarkTodoAsCompletedAsync(Guid id)
        {
            try
            {
                var response = _httpClient.PostAsync(_baseUrl + "api/Todo/Complete/" + id, null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    throw new Exception($"Error marking todo as completed: {response.ReasonPhrase}");
                }

            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while marking todo as completed: {ex.Message}", ex);
            }
        }

        public Task<bool> MarkTodoAsPendingAsync(Guid id)
        {
            try
            {
                var response = _httpClient.PostAsync(_baseUrl + "api/Todo/Pending/" + id, null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    throw new Exception($"Error marking todo as completed: {response.ReasonPhrase}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while marking todo as completed: {ex.Message}", ex);
            }
        }
    }
}

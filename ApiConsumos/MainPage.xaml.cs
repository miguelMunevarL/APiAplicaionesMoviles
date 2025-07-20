using ApiConsumos.Interfaces;
using ApiConsumos.Models;
using CommunityToolkit.Maui.Extensions;

namespace ApiConsumos
{
    public partial class MainPage : ContentPage
    {

        private readonly ITodo _todoService;

        public MainPage(ITodo todoService)
        {
            InitializeComponent();
            _todoService = todoService;
            CargarTareas();


        }

        private async void CargarTareas()
        {
            try
            {
                var todos = await _todoService.GetTodosAsync();
                TodosListView.ItemsSource = todos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void TodosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Todo selectedTodo)
            {
                var popup = new TodoDetailPopup(selectedTodo, _todoService);
                await this.ShowPopupAsync(popup);
                CargarTareas();  
                TodosListView.SelectedItem = null;
            }
        }



    }
}

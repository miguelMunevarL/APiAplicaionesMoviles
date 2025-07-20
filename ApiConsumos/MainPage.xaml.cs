using ApiConsumos.Interfaces;

namespace ApiConsumos
{
    public partial class MainPage : ContentPage
    {

        private readonly ITodo _todoService;

        public MainPage(ITodo todoService)
        {
            InitializeComponent();
            _todoService = todoService;
        }

        private async void OnLoadTodosClicked(object sender, EventArgs e)
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


    }
}

using ApiConsumos.Interfaces;
using ApiConsumos.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace ApiConsumos;

public partial class TodoDetailPopup : Popup
{
    private ITodo _todoService;
    private Guid _todoId;
    private bool _isHandlingToggle = false;
    private bool _isSwitchInitializing = true;


    public TodoDetailPopup(Todo todo, ITodo todoService)
    {
        InitializeComponent();

        _todoService = todoService;
        _todoId = todo.Id;
        NameLabel.Text = todo.Name;
        StatusSwitch.Toggled += StatusSwitch_Toggled;

        _isSwitchInitializing = true;
        StatusSwitch.IsToggled = todo.Status;
        _isSwitchInitializing = false;
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void StatusSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (_isSwitchInitializing)
            return;

        if (_isHandlingToggle)
            return;

        _isHandlingToggle = true;

        bool nuevoEstado = e.Value;

        bool confirmado = false;
        string mensaje = nuevoEstado ?
            "¿Desea marcar esta tarea como completada?" :
            "¿Desea marcar esta tarea como pendiente?";

        confirmado = await Application.Current.MainPage.DisplayAlert("Confirmar", mensaje, "Sí", "No");

        if (confirmado)
        {
            if (nuevoEstado)
            {
                await _todoService.MarkTodoAsCompletedAsync(_todoId);
                await _todoService.GetTodosAsync();
                var snackbar = Snackbar.Make("Tarea Completa", actionButtonText: "OK");
                await snackbar.Show();

            }
            else
            {
                await _todoService.MarkTodoAsPendingAsync(_todoId);
                await _todoService.GetTodosAsync();
                var snackbar = Snackbar.Make("Tarea pendiente", actionButtonText: "OK");
                await snackbar.Show();

            }
        }
        else
        {
            // Revertir el estado del switch porque el usuario no confirmó
            StatusSwitch.Toggled -= StatusSwitch_Toggled; // Desuscribir para evitar loop
            StatusSwitch.IsToggled = !nuevoEstado;
            StatusSwitch.Toggled += StatusSwitch_Toggled;
        }

        _isHandlingToggle = false;
    }


}
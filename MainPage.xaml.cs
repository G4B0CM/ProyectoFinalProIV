using Avance2Progreso.Models;
using Avance2Progreso.Services;

namespace Avance2Progreso;
public partial class MainPage : ContentPage
{
    private readonly UserService _userService;

    public MainPage(UserService userService)
    {
        InitializeComponent();
        _userService = userService;
    }

    // Evento para agregar usuario
    private async void OnCreateUserClicked(object sender, EventArgs e)
    {
        try
        {
            // Obtén los datos ingresados por el usuario
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            bool isAdmin = IsAdminSwitch.IsToggled;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Username and Password are required.", "OK");
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = password,
                IsAdmin = isAdmin
            };

            // Llama al servicio para crear el usuario
            var createdUser = await _userService.CreateUserAsync(newUser);
            await DisplayAlert("User Created", $"User created with ID: {createdUser.Id}", "OK");

            // Limpia los campos
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            IsAdminSwitch.IsToggled = false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    // Evento para eliminar usuario
    private async void OnDeleteUserClicked(object sender, EventArgs e)
    {
        try
        {
            if (int.TryParse(UserIdEntry.Text, out int userId))
            {
                // Llama al servicio para eliminar el usuario
                await _userService.DeleteUserAsync(userId);
                await DisplayAlert("User Deleted", $"User with ID {userId} has been deleted.", "OK");

                // Limpia el campo
                UserIdEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid User ID.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    private async void OnFetchUsersClicked(object sender, EventArgs e)
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            UsersCollection.ItemsSource = users;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnFetchUserByIdClicked(object sender, EventArgs e)
    {
        try
        {
            if (int.TryParse(FetchUserIdEntry.Text, out int userId))
            {
                var user = await _userService.GetUserByIdAsync(userId);
                FetchedUserLabel.Text = $"ID: {user.Id}, Username: {user.Username}, IsAdmin: {user.IsAdmin}";
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid User ID.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    private async void OnUpdateUserClicked(object sender, EventArgs e)
    {
        try
        {
            if (int.TryParse(UpdateUserIdEntry.Text, out int userId))
            {
                var updatedUser = new User
                {
                    Id = userId,
                    Username = UpdateUsernameEntry.Text,
                    Password = UpdatePasswordEntry.Text,
                    IsAdmin = UpdateIsAdminSwitch.IsToggled
                };

                await _userService.UpdateUserAsync(userId, updatedUser);
                await DisplayAlert("User Updated", $"User with ID {userId} has been updated.", "OK");

                // Limpia los campos
                UpdateUserIdEntry.Text = string.Empty;
                UpdateUsernameEntry.Text = string.Empty;
                UpdatePasswordEntry.Text = string.Empty;
                UpdateIsAdminSwitch.IsToggled = false;
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid User ID.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
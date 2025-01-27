namespace Avance2Progreso.Views.Admin;

public partial class AdministarView : ContentPage
{

    public AdministarView()
    {
        InitializeComponent();
    }

    // Evento para agregar usuario
    private async void OnCreateUserClicked(object sender, EventArgs e)
    {

        UsernameEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        IsAdminSwitch.IsToggled = false;

    }

    // Evento para eliminar usuario
    private async void OnDeleteUserClicked(object sender, EventArgs e)
    {

        UserIdEntry.Text = string.Empty;

    }
    private async void OnFetchUsersClicked(object sender, EventArgs e)
    {

    }

    private async void OnFetchUserByIdClicked(object sender, EventArgs e)
    {
        FetchUserIdEntry.Text = string.Empty;
    }
    private async void OnUpdateUserClicked(object sender, EventArgs e)
    {

        UpdateUserIdEntry.Text = string.Empty;
        UpdateUsernameEntry.Text = string.Empty;
        UpdatePasswordEntry.Text = string.Empty;
        UpdateIsAdminSwitch.IsToggled = false;

    }
}
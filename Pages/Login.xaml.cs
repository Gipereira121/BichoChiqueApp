namespace BichoChiqueApp.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Login realizado", "Bem-vindo de volta!", "Ok");
        await Navigation.PushAsync(new Home());
    }
}
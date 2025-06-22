namespace BichoChiqueApp.Pages;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}


    private async void OnFrameCadastroTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cadastro());
    }

 

    private async void OnFrameSobreNosTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuemSomos());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}
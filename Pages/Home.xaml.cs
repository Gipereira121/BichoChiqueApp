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

    private async void OnFrameAgendamentoTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Consultas());
    }

    private async void OnFrameMeusClientesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MeusClientes());
    }
    private async void OnFrameSobreNosTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuemSomos());
    }


}
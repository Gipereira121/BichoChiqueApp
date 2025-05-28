namespace BichoChiqueApp.Pages;

public partial class NovoCliente : ContentPage
{
	public NovoCliente()
	{
		InitializeComponent();
	}

    private async void btnAdicionarCliente_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MeusClientes());
    }

}
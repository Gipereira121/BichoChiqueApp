namespace BichoChiqueApp.Pages;

public partial class MeusClientes : ContentPage
{
	public MeusClientes()
	{
		InitializeComponent();
	}

    private async void OnStackNovoClienteTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoCliente());
    }
}
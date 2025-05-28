namespace BichoChiqueApp.Pages;

public partial class MeusClientes : ContentPage
{
	public MeusClientes()
	{
		InitializeComponent();
	}


    private async void btnInsert_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoCliente());
    }

    private void searchBarChanged(object sender, TextChangedEventArgs e)
    {
        

    }




}
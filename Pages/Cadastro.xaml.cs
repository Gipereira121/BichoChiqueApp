namespace BichoChiqueApp.Pages;

public partial class Cadastro : ContentPage
{
	public Cadastro()
	{
		InitializeComponent();
	}

        private async void btnCadastro_Clicked(object sender, EventArgs e)
        {
        DisplayAlert("Cadastro Concluído", "Seu cadastro foi realizado com sucesso!", "Ok");
        await Navigation.PushAsync(new Home());
        return;
        }
}

using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace BichoChiqueApp.Pages.Editar;
// namespace BichoChiqueApp;

public partial class EditEspecie : ContentPage
{
	public EditEspecie()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Especies especieParaEditar)
        {
            etrEspecie.Text = especieParaEditar.espNome;
            edtOBSEspecie.Text = especieParaEditar.espObs;
        }
        else
        {
            await DisplayAlert("Erro", "N�o foi poss�vel carregar a especie para edi��o.", "OK");
        }
    }

    private async void btnEdtEspecieOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is Especies especieParaAtualizar)
        {
            especieParaAtualizar.espNome = etrEspecie.Text;
            especieParaAtualizar.espObs = edtOBSEspecie.Text;

            await App.Db.UpdateEspecie(especieParaAtualizar);
            await DisplayAlert("Aten��o", "Especie editada!", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Nenhuma especie v�lida para atualizar.", "OK");
        }
    }
}
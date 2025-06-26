using Microsoft.Maui.Controls;

using BichoChiqueApp.Models;
using BichoChiqueApp.Helpers;

namespace BichoChiqueApp.Pages.Editar;

public partial class EditCliente : ContentPage
{
	public EditCliente()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is BichoChiqueApp.Models.Cliente clienteParaEditar)
        {
            etrCliente.Text = clienteParaEditar.cliNome;
            edtOBSCliente.Text = clienteParaEditar.cliObs;
        }
        else
        {
            DisplayAlert("Erro", "N�o foi poss�vel carregar o modelo para edi��o.", "OK");
        }
    }

    private async void btnEdtClienteOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is BichoChiqueApp.Models.Cliente clienteParaAtualizar)
        {
            clienteParaAtualizar.cliNome = etrCliente.Text;
            clienteParaAtualizar.cliObs = edtOBSCliente.Text;

            await App.Db.UpdateCliente(clienteParaAtualizar);
            await DisplayAlert("Aten��o", "Cliente editado!", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Nenhum cliente v�lido para atualizar.", "OK");
        }
    }
}
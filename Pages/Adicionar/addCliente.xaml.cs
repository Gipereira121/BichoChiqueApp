using Microsoft.Maui.Controls;

using BichoChiqueApp.Models;
using BichoChiqueApp.Helpers;

namespace BichoChiqueApp.Pages.Adicionar;

public partial class addCliente : ContentPage
{
	public addCliente()
	{
		InitializeComponent();
        etrDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }

    public addCliente(IEnumerable<BichoChiqueApp.Models.Cliente> cli, object cliNome)
    {
        InitializeComponent();
        btnAddCliente.Clicked += btnAddClienteOnClicked;
    }

    private async void btnAddClienteOnClicked(object? sender, System.EventArgs e)
    {
        BichoChiqueApp.Models.Cliente cli = new BichoChiqueApp.Models.Cliente();
        cli.cliNome = etrCliente.Text;
        cli.cliObs = edtOBSCliente.Text;
        cli.cliCPF = etrCPF.Text;
        cli.cliEmail = etrEmail.Text;

        if (DateTime.TryParse(etrDataCadastro.Text, out var parsedDate))
            cli.cliDataCadastro = parsedDate;
        else
            cli.cliDataCadastro = DateTime.Now;

        await App.Db.InsertCliente(cli);
        await DisplayAlert("Sucesso!", "Cliente adicionado.", "OK");

        OnAppearing();

        if (string.IsNullOrWhiteSpace(cli.cliNome))
        {
            await DisplayAlert("ERRO", "O campo 'Cliente' precisa ser preenchido!", "Ok");
            return;
        }

        await Navigation.PopAsync();
    }

}
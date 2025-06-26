using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace BichoChiqueApp.Pages.Adicionar;

public partial class addAnimal : ContentPage
{
    private List<Especies>? _especies;
    private List<BichoChiqueApp.Models.Cliente>? _clientes;

    public addAnimal()
    {
        InitializeComponent();
        CarregarEsp();
        CarregarCli();
    }

    private async Task CarregarEsp()
    {
        _especies = await App.Db.GetAllEspecie();
        if (_especies != null && _especies.Any())
        {
            pkEsp.ItemsSource = _especies.Select(m => m.espNome).ToList();
        }
    }

    private async Task CarregarCli()
    {
        _clientes = await App.Db.GetAllCliente();
        if (_clientes != null && _clientes.Any())
        {
            pkCli.ItemsSource = _clientes.Select(m => m.cliNome).ToList();
        }
    }

    private async void btnAddAnimalOnClicked(object? sender, System.EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(etrAnimal.Text) ||
            string.IsNullOrWhiteSpace(etrAniApelido.Text) ||
            pkEsp.SelectedIndex == -1 ||
            pkCli.SelectedIndex == -1)
        {
            await DisplayAlert("ERRO", "Preencha todos os campos obrigatórios!", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(etrAniApelido.Text))
        {
            await DisplayAlert("ERRO", "Digite um apelido válido.", "Ok");
            return;
        }

        int? selectedEspecieId = null;
        if (pkEsp.SelectedIndex != -1 && _especies != null)
        {
            selectedEspecieId = _especies[pkEsp.SelectedIndex].espId;
        }

        int? selectedClienteId = null;
        if (pkCli.SelectedIndex != -1 && _clientes != null)
        {
            selectedClienteId = _clientes[pkCli.SelectedIndex].cliId;
        }

        if(!selectedEspecieId.HasValue || !selectedClienteId.HasValue)
        {
            await DisplayAlert("ERRO", "Não foi possível obter o ID da Especie ou Cliente selecionado.", "Ok");
            return;
        }

        Animal ani = new Animal
        {
            aniNome = etrAnimal.Text,
            aniApe = etrAniApelido.Text,
            aniObs = edtObsAnimal?.Text ?? string.Empty,
            codEsp = selectedEspecieId.Value,
            codCli = selectedClienteId.Value
        };

        try
        {
            await App.Db.InsertAnimal(ani);
            await DisplayAlert("Sucesso!", "Animal adicionado.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao adicionar o animal: {ex.Message}", "OK");
        }
    }
}
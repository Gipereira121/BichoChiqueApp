using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace BichoChiqueApp.Pages.Adicionar;

public partial class addEspecie : ContentPage
{
	public addEspecie()
	{
		InitializeComponent();
	}
    public void AddEspecie(IEnumerable<Especies> esp, object espNome)
    {
        InitializeComponent();
        btnAddEspecie.Clicked += btnAddEspecieOnClicked;
    }

    private async void btnAddEspecieOnClicked(object? sender, System.EventArgs e)
    {
        Especies esp = new Especies();
        esp.espNome = etrEspecie.Text;
        esp.espObs = edtOBSEspecie.Text;

        await App.Db.InsertEspecie(esp);
        await DisplayAlert("Sucesso!", "Especie adicionada.", "OK");

        OnAppearing();

        if (string.IsNullOrWhiteSpace(esp.espNome))
        {
            await DisplayAlert("ERRO", "O campo 'Especie' precisa ser preenchido!", "Ok");
            return;
        }

        await Navigation.PopAsync();
    }

}
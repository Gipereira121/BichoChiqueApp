using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BichoChiqueApp.Pages;

public partial class MeusPets : ContentPage
{
	public MeusPets()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarEspeciesNoContainer(string.Empty);
    }

    private async void btnAddMarcaOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Adicionar.addEspecie());
    }

    private async Task CarregarEspeciesNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<Especies> especies;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            especies = await App.Db.GetAllEspecie();
        }
        else
        {
            especies = await App.Db.SearchEspecie(searchTerm);
        }

        if (especies != null && especies.Any())
        {
            foreach (var esp in especies)
            {
                var frame = new Frame
                {
                    BackgroundColor = Color.FromArgb("#FFE3C3"),
                    Padding = 20,
                    WidthRequest = 320,
                    CornerRadius = 10,
                    Margin = new Thickness(12),
                    HasShadow = true,
                    Content = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star }
                        },
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        }
                    }
                };

                var gridContent = (Grid)frame.Content;

                var image = new Image
                {
                    Source = "species.png",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                };
                gridContent.Children.Add(image);


                var labelId = new Label
                {
                    Text = $"ID: {esp.espId}",
                    TextColor = Colors.Black,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelEspecie = new Label
                {
                    Text = $"Especie: {esp.espNome}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelEspecie, 1);
                Grid.SetRow(labelEspecie, 1);
                gridContent.Children.Add(labelEspecie);

                var labelObs = new Label
                {
                    Text = $"Obs: {esp.espObs}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelObs, 1);
                Grid.SetRow(labelObs, 2);
                gridContent.Children.Add(labelObs);


                var buttonsLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.End,
                    Spacing = 10,
                    Margin = new Thickness(0, 10, 0, 0),
                    Children =
                    {
                        new ImageButton
                        {
                            Source = "pencil.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                var editEspeciePage = new Pages.Editar.EditEspecie();
                                editEspeciePage.BindingContext = esp;
                                await Navigation.PushAsync(editEspeciePage);
                            })
                        },
                        new ImageButton
                        {
                            Source = "bin.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Tem certeza que deseja excluir a especie '{esp.espNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteEspecie(esp.espId);
                                    await DisplayAlert("Excluída", "Espécie Excluida!", "Ok");
                                    await CarregarEspeciesNoContainer(SearchBarEspecie.Text);
                                }
                            })
                        }
                    }
                };
                Grid.SetRow(buttonsLayout, 3);
                Grid.SetColumn(buttonsLayout, 0);
                Grid.SetColumnSpan(buttonsLayout, 2);
                gridContent.Children.Add(buttonsLayout);

                cardsContainer.Children.Add(frame);
            }
        }
    }
    private async void txtSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        await CarregarEspeciesNoContainer(q);
    }

    private async void btnAddEspecie_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Adicionar.addEspecie());
    }
}
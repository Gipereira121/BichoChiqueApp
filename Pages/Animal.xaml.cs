using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BichoChiqueApp.Pages.Editar;

namespace BichoChiqueApp.Pages;

public partial class Animal : ContentPage
{
    private List<Especies>? _especies;
    private List<BichoChiqueApp.Models.Cliente>? _clientes;

    public Animal()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReferencedData();
        await CarregarAnimalNoContainer(string.Empty);

    }

    private async Task LoadReferencedData()
    {
        if (App.Db == null)
        {
            Console.WriteLine("ERRO: App.Db não foi inicializado. Certifique-se de que seu App.xaml.cs inicializa o banco de dados.");
            return;
        }

        try
        {
            _especies = await App.Db.GetAllEspecie();
            _clientes = await App.Db.GetAllCliente();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar dados de referência: {ex.Message}");
            await DisplayAlert("Erro", "Não foi possível carregar as especies e clientes. Tente novamente.", "Ok");
        }
    }

    private async void btnAddAnimalOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Adicionar.addAnimal());
    }

    private async Task CarregarAnimalNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<BichoChiqueApp.Models.Animal> animais;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            animais = await App.Db.GetAllAnimal();
        }
        else
        {
            animais = await App.Db.SearchAnimal(searchTerm);
        }

        if (animais != null && animais.Any())
        {
            foreach (var ani in animais)
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
                    Source = "vehicles.png",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                };
                gridContent.Children.Add(image);
                Grid.SetColumn(image, 0);
                Grid.SetRow(image, 0);
                Grid.SetRowSpan(image, 2);


                var labelId = new Label
                {
                    Text = $"ID: {ani.aniId}",
                    TextColor = Colors.Black,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelAnimal = new Label
                {
                    Text = $"Animal: {ani.aniNome}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelAnimal, 1);
                Grid.SetRow(labelAnimal, 1);
                gridContent.Children.Add(labelAnimal);


                var labelAniApe = new Label
                {
                    Text = $"Apelido: {ani.aniApe}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelAniApe, 1);
                Grid.SetRow(labelAniApe, 2);
                gridContent.Children.Add(labelAniApe);

                var especieAssociada = _especies?.FirstOrDefault(m => m.espId == ani.codEsp);
                var clienteAssociado = _clientes?.FirstOrDefault(m => m.cliId == ani.codCli);

                var labelCodEsp = new Label
                {
                    Text = $"Especie:  {(especieAssociada != null ? especieAssociada.espNome : "N/A")}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelCodEsp, 1);
                Grid.SetRow(labelCodEsp, 3);
                gridContent.Children.Add(labelCodEsp);


                var labelCodCli = new Label
                {
                    Text = $"Cliente: {(clienteAssociado != null ? clienteAssociado.cliNome : "N/A")}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelCodCli, 1);
                Grid.SetRow(labelCodCli, 4);
                gridContent.Children.Add(labelCodCli);


                var labelObs = new Label
                {
                    Text = $"Observações: {ani.aniObs}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelObs, 1);
                Grid.SetRow(labelObs, 5);
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
                                var editAnimalPage = new Pages.Editar.edtAnimal();
                                editAnimalPage.BindingContext = ani;
                                await Navigation.PushAsync(editAnimalPage);
                            })
                        },
                        new ImageButton
                        {
                            Source = "bin.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir o animal '{ani.aniNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteAnimal(ani.aniId);
                                    await DisplayAlert("Excluída", "O animal foi deletado!", "Ok");
                                    await CarregarAnimalNoContainer(SearchBarAnimal.Text);
                                }
                            })
                        }
                    }
                };
                Grid.SetRow(buttonsLayout, 6);
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
        await CarregarAnimalNoContainer(q);
    }


}
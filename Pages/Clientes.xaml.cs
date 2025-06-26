using BichoChiqueApp.Helpers;
using BichoChiqueApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BichoChiqueApp.Pages.Editar;

namespace BichoChiqueApp.Pages;

public partial class Clientes : ContentPage
{
	public Clientes()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarClientesNoContainer(string.Empty);
    }

    private async void btnAddClienteOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Adicionar.addCliente());
    }

    private async Task CarregarClientesNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<BichoChiqueApp.Models.Cliente> clientes;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            clientes = await App.Db.GetAllCliente();
        }
        else
        {
            clientes = await App.Db.SearchCliente(searchTerm);
        }

        if (clientes != null && clientes.Any())
        {
            foreach (var cli in clientes)
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
                    Source = "myclient.png",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                };
                gridContent.Children.Add(image);


                var labelId = new Label
                {
                    Text = $"ID: {cli.cliId}",
                    TextColor = Colors.Black,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelCliente = new Label
                {
                    Text = $"Cliente: {cli.cliNome}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelCliente, 1);
                Grid.SetRow(labelCliente, 1);
                gridContent.Children.Add(labelCliente);

                var labelEmail = new Label
                {
                    Text = $"Email: {cli.cliEmail}",
                    TextColor = Colors.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelEmail, 1);
                Grid.SetRow(labelEmail, 2);
                gridContent.Children.Add(labelEmail);


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
                                var editClientePage = new EditCliente();
                                editClientePage.BindingContext = cli;
                                await Navigation.PushAsync(editClientePage);
                            })
                        },
                        new ImageButton
                        {
                            Source = "bin.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir o cliente '{cli.cliNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteCliente(cli.cliId);
                                    await DisplayAlert("Excluído", "O cliente foi deletado!", "Ok");
                                    await CarregarClientesNoContainer(SearchBarCliente.Text);
                                }
                            })
                        }
                    }
                };
                Grid.SetRow(buttonsLayout, 4);
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
        await CarregarClientesNoContainer(q);
    }
}
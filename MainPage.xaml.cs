namespace BichoChiqueApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.Login());
        }

        private async void btnCadastro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.Cadastro());
        }
    }

}

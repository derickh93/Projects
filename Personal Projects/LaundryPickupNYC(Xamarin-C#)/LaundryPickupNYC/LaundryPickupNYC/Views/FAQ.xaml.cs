using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaundryPickupNYC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FAQ : ContentPage
    {
        public FAQ()
        {
            InitializeComponent();
        }
        private async void NavigateBook_OnClicked(object sender, EventArgs e)
        {
            if (GlobalVar.loggedIn == true)
            {
                await Navigation.PushModalAsync(new NavigationPage(new Address()));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please login to place an order!", "OK");
                await Shell.Current.GoToAsync("//home");


            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pricing : ContentPage
    {
        public Pricing()
        {
            InitializeComponent();
        }

        private async void NavigateBook_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Booking());
        }

        private async void NavigateWash_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WashnFold());
        }
    }
}
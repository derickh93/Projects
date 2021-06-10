using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Booking : ContentPage
    {
        public Booking()
        {
            InitializeComponent();
            webview.Source = "https://app.acuityscheduling.com/schedule.php?owner=21155921";
        }
    }
}
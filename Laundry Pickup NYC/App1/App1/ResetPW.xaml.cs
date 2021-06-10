using Firebase.Auth;
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
    public partial class ResetPW : ContentPage
    {
        public string WebAPIkey = "AIzaSyAtDKWefoXXGlxMJN9x6MYy9nzgv1WMWag";

        public ResetPW()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1c2f74");
        }

        private void resetButton_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            String emailAddress = "derickhansraj@ymail.com";
            authProvider.SendPasswordResetEmailAsync(emailAddress);
        }
    }
}
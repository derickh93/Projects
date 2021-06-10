using SODA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lottery_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            var client = new SodaClient("https://data.ny.gov", "4kAeJM4FC1linQef7ldqrutXB");

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var datasetMega = client.GetResource<Dictionary<string, object>>("5xaw-6ayf");


            // Resource objects read their own data
            var rowsMega = datasetMega.GetRows(limit: 5);

            //DisplayAlert("output", "Got {0} results. Dumping first results: " + rows.Count(),"cancel");

            foreach (var keyValueMega in rowsMega.First())
            {

                DisplayAlert("output", keyValueMega.Value.ToString(), "cancel");

            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var datasetPower = client.GetResource<Dictionary<string, object>>("d6yy-54nr");


            // Resource objects read their own data
            var rowsPower = datasetPower.GetRows(limit: 5);

            //DisplayAlert("output", "Got {0} results. Dumping first results: " + rows.Count(),"cancel");

            foreach (var keyValuePower in rowsPower.First())
            {

                DisplayAlert("output", keyValuePower.Value.ToString(), "cancel");

            }
        }
    }
}
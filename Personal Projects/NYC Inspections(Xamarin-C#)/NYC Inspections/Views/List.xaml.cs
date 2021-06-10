using NYC_Inspections.Models;
using NYC_Inspections.Popups;
using NYC_Inspections.ViewModels;
using Rg.Plugins.Popup.Services;
using SODA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
///  This class loads a list of restaurants specific to the users current location. The user also has the option to search for a restaurant.
/// </summary>
namespace NYC_Inspections.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : ContentPage
    {
        private IEnumerable<Dictionary<string, object>> query;
        private SODA.Resource<Dictionary<string, object>> dataset;
        private string currentZip = "";
        private RestaurantItemViewModel rivm;
        int[] indexArr;

        public string Dba { set; get; }
        public string GradeDate { set; get; }
        public ImageSource ImageUri { get; set; }
        public string Camis { get; set; }
        public string Grade { get; set; }
        public int Index { get; set; }


        public List()
        {
            InitializeComponent();

            rivm = new RestaurantItemViewModel();

            var client = new SodaClient("https://data.ny.gov", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            //var task = Task.Run(async () => { await GetZip(); });

            //Task.WaitAll(task); //block and wait for task to complete

            dataset = client.GetResource<Dictionary<string, object>>("43nn-pn8j");

            getRestaurants(currentZip);
        }

        private async void RestaurantList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var i = (listRestaurants.ItemsSource as List<RestaurantItem>).IndexOf(e.SelectedItem as RestaurantItem);
            await PopupNavigation.Instance.PushAsync(new LoadingPopup(), true);
            //await PopupNavigation.Instance.PushAsync(new InfoPopup(query.ElementAt(rivm.RestaurantItems.ElementAt(i).Index)), true);
            await Navigation.PushAsync(new InspectionList(query.ElementAt(rivm.RestaurantItems.ElementAt(i).Index),dataset));
        }

        private void Button_Clicked_Search(object sender, EventArgs e)
        {
            getRestaurants(entry.Text.ToString());
        }

        private void Button_Clicked_Filter(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FilterPopup(dataset), true);
        }

        private void Button_Clicked_Reset(object sender, EventArgs e)
        {
            getRestaurants(currentZip);
        }

        private async void Button_Clicked_Map(object sender, EventArgs e)
        {

            indexArr = rivm.RestaurantItems.Select(i => i.Index).ToArray();
            await Navigation.PushAsync(new Map(query,indexArr));
        }

        private void getRestaurants(string searchText)
        {
            rivm = new RestaurantItemViewModel();
            var soql = new SoqlQuery().FullTextSearch(searchText).Limit(20);
            query = dataset.Query<Dictionary<string, object>>(soql);

            for (int i = 0; i < query.Count(); i++)
            {
                object keyValue = "";
                object grade = "";
                object gradeDate = "";
                Index = i;
                query.ElementAt(i).TryGetValue("dba", out keyValue);
                Dba = keyValue.ToString();

                object camis = "";
                query.ElementAt(i).TryGetValue("camis", out camis);
                Camis = camis.ToString();

                if (query.ElementAt(i).TryGetValue("grade_date", out gradeDate))
                {
                    GradeDate = gradeDate.ToString().Substring(0, gradeDate.ToString().IndexOf(" "));
                }
                else
                {
                    GradeDate = "Not Inspected";
                }

                if (query.ElementAt(i).TryGetValue("grade", out grade))
                {
                    //Grade = grade.ToString();
                    if (grade.ToString().Equals("A"))
                    {
                        Grade = "A";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_A.png");
                    }
                    else if (grade.ToString().Equals("B"))
                    {
                        Grade = "B";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_B.png");
                    }
                    else if (grade.ToString().Equals("C"))
                    {
                        Grade = "C";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_C.png");
                    }
                    else if (grade.ToString().Equals("Z"))
                    {
                        Grade = "Z";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_GP.png");
                    }
                    else if (grade.ToString().Equals("N"))
                    {
                        Grade = "N";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_NG.png");
                    }
                    else if (grade.ToString().Equals("P"))
                    {
                        Grade = "P";

                        ImageUri = ImageSource.FromFile("Images/NYCRestaurant_Closed.png");
                    }
                }
                else
                {
                    Grade = "";
                    grade = "";
                }
                if (grade.Equals(""))
                {
                }
                else
                {
                    Models.RestaurantItem temp = new Models.RestaurantItem(Dba, GradeDate, ImageUri, Camis, Grade,Index);

                    rivm.RestaurantItems.Add(temp);
                }
            }

            listRestaurants.ItemsSource = rivm.RestaurantItems.ToList();
            listRestaurants.ItemSelected += RestaurantList_ItemSelected;
        }

        private async Task GetZip()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                var lat = location.Latitude;
                var lon = location.Longitude;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);
                currentZip = placemarks.First().PostalCode;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Error", fnsEx.ToString(), "cancel");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Error", fneEx.ToString(), "cancel");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Error", pEx.ToString(), "cancel");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "cancel");
            }
        }
    }
}
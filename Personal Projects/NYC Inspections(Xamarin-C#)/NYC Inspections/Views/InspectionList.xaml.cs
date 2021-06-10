using NYC_Inspections.Models.NYC_Inspections.Models;
using NYC_Inspections.Popups;
using NYC_Inspections.ViewModels;
using Rg.Plugins.Popup.Services;
using SODA;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
///  This class loads a list of inspections performed at a specific restaurant. This is done using the initial dataset retrieved from the 
///  Socrata API.
/// </summary>

namespace NYC_Inspections.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspectionList : ContentPage
    {
        bool isTaskRunning;

        private IEnumerable<Dictionary<string, object>> query;
        private SODA.Resource<Dictionary<string, object>> dataset;

        private InspectionListViewModel iivm;

        public string Dba { set; get; }
        public string InspectionDate { set; get; }
        public string InspectionType { set; get; }
        public string Score { get; set; }

        public InspectionList(Dictionary<string, object> info, SODA.Resource<Dictionary<string, object>> ds)
        {
            InitializeComponent();

            iivm = new InspectionListViewModel();

            dataset = ds;


            object camis = "";
            info.TryGetValue("camis", out camis);
            permitNumber.Text = camis.ToString();

            object grade = "";
            info.TryGetValue("grade", out grade);
            loadGradeImage(grade.ToString());

            object dba = "";
            info.TryGetValue("dba", out dba);
            name.Text = dba.ToString();

            object addr = "";
            info.TryGetValue("building", out addr);
            address.Text = addr.ToString();

            object fooType = "";
            info.TryGetValue("cuisine_description", out fooType);
            foodType.Text = fooType.ToString();

            getInspections(camis.ToString());
        }

        private async void RestaurantList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var i = (listRestaurants.ItemsSource as List<InspectionItem>).IndexOf(e.SelectedItem as InspectionItem);
            await PopupNavigation.Instance.PushAsync(new InfoPopup(query.ElementAt(i)), true);
        }

        private async void getInspections(string searchText)
        {
            iivm = new InspectionListViewModel();
            var soql = new SoqlQuery().FullTextSearch(searchText).Limit(20);
            query = dataset.Query<Dictionary<string, object>>(soql);

            for (int i = 0; i < query.Count(); i++)
            {
                object inspectionDate = "";
                object inspectionType = "";
                object score = "";

                query.ElementAt(i).TryGetValue("inspection_date", out inspectionDate);

                InspectionDate = inspectionDate.ToString().Substring(0, inspectionDate.ToString().IndexOf(" "));

                query.ElementAt(i).TryGetValue("inspection_type", out inspectionType);
                InspectionType = inspectionType.ToString();

                query.ElementAt(i).TryGetValue("score", out score);
                Score = score.ToString();

                InspectionItem temp = new InspectionItem(InspectionDate, Score, InspectionType);

                iivm.InspectionItems.Add(temp);
            }

            listRestaurants.ItemsSource = iivm.InspectionItems.ToList();
            listRestaurants.ItemSelected += RestaurantList_ItemSelected;
            await PopupNavigation.PopAsync();

        }

        private void loadGradeImage(string grade)
        {
            //Grade = grade.ToString();
            if (grade.ToString().Equals("A"))
            {
                image.Source = ImageSource.FromFile("Images/NYCRestaurant_A.png");
            }
            else if (grade.ToString().Equals("B"))
            {
                image.Source = ImageSource.FromFile("Images/NYCRestaurant_B.png");
            }
            else if (grade.ToString().Equals("C"))
            {
                image.Source = ImageSource.FromFile("Images/NYCRestaurant_C.png");
            }
            else if (grade.ToString().Equals("Z"))
            {
                image.Source = ImageSource.FromFile("Images/NYCRestaurant_GP.png");
            }
            else if (grade.ToString().Equals("N"))
            {
                image.Source = ImageSource.FromFile("Images/NYCInspection_NG.png");
            }
            else if (grade.ToString().Equals("P"))
            {
                image.Source = ImageSource.FromFile("Images/NYCRestaurant_Closed.png");
            }

        }
    }
}
using System.Collections.Generic;
using Xamarin.Forms;

/// <summary>
///  This class represents a model used for a listview that contains specific details about an individual restaurant.
/// </summary>
namespace NYC_Inspections.Models

{
    internal class RestaurantItem
    {
		//variables
        public string Dba { get; set; }
        public string GradeDate { get; set; }
        public string Camis { get; set; }
        public string Grade { get; set; }
        public int Index { get; set; }
        public ImageSource ImageUri { get; set; }
        private List<RestaurantItem> gradings;

		//default constructor
        public RestaurantItem()
        {
        }
		
		//parameterized constructor
        public RestaurantItem(string dba, string gradeDate, ImageSource img, string camis, string grade, int index)
        {
            Dba = dba;
            GradeDate = gradeDate;
            ImageUri = img;
            Camis = camis;
            Grade = grade;
            Index = index;
        }

		//method that checks if two RestaurantItems are equal
        public bool Equals(RestaurantItem obj)
        {
            return this.Camis.Equals(obj.Camis);
        }

		//method that returns a List of RestaurantItems
        public List<RestaurantItem> GetRestaurantItems()
        {
            gradings = new List<RestaurantItem>()
            {
            };
            return gradings;
        }
    }
}
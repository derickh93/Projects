using NYC_Inspections.Models;
using System.Collections.Generic;

/// <summary>
///  This class represents a viewmodel used for a listview that contains specific details about a restaurant.
/// </summary>
namespace NYC_Inspections.ViewModels
{
    internal class RestaurantItemViewModel
    {
        public List<RestaurantItem> RestaurantItems { get; set; }

        public RestaurantItemViewModel()
        {
            RestaurantItems = new RestaurantItem().GetRestaurantItems();
        }
    }
}
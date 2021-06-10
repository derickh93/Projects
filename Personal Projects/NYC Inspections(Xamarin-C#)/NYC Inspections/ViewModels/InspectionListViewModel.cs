using NYC_Inspections.Models.NYC_Inspections.Models;
using System.Collections.Generic;

/// <summary>
///  This class represents a viewmodel used for a listview that contains specific details about an individual inspection.
/// </summary>

namespace NYC_Inspections.ViewModels
{
    internal class InspectionListViewModel
    {
        public List<InspectionItem> InspectionItems { get; set; }

        public InspectionListViewModel()
        {
            InspectionItems = new InspectionItem().GetInspectionItems();
        }
    }
}
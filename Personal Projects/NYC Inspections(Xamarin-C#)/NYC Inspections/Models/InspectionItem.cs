namespace NYC_Inspections.Models
{
    using System.Collections.Generic;
    using Xamarin.Forms;
	
	/// <summary>
///  This class represents a model used for a listview that contains specific details about an individual inspection.
/// </summary>

    namespace NYC_Inspections.Models
    {
        internal class InspectionItem
        {
			//variables
            public string InspectionDate { set; get; }
            public string Score { get; set; }
            public string InspectionType { get; set; }
            public string Detail { get; set; }
            private List<InspectionItem> inspections;

			//default constructor
            public InspectionItem()
            {
            }

			//parameterized constructor
            public InspectionItem(string inspectionDate, string score, string inspectionType)
            {
                InspectionDate = inspectionDate;
                Score = score;
                InspectionType = inspectionType;
                Detail = InspectionType + " Score: " + Score;
            }

			//method that returns the List of InspectionItems.
            public List<InspectionItem> GetInspectionItems()
            {
                inspections = new List<InspectionItem>()
                {
                };
                return inspections;
            }
        }
    }
}
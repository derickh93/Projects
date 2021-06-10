using NYC_Inspections.Models;
using NYC_Inspections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

/// <summary>
///  This class loads a platform specific map with markers placed at restaurant locations.
/// </summary>

namespace NYC_Inspections.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        public Map(IEnumerable<Dictionary<string, object>> query, int[] index)
        {
            InitializeComponent();
            var map = mapObj;
            List<Position> positions = new List<Position>();

            //////////////////////////////////////////////////////////
            for (int i = 0; i < index.Count(); i++)
            {
                object latitude = "";
                object longitude = "";
                object dba;
                object grade;
                query.ElementAt(index[i]).TryGetValue("latitude", out latitude);
                query.ElementAt(index[i]).TryGetValue("longitude", out longitude);
                query.ElementAt(index[i]).TryGetValue("dba", out dba);
                if (query.ElementAt(index[i]).TryGetValue("grade", out grade))
                {
                }
                else
                {
                    grade = "NULL";
                }
                if ((Double.Parse(latitude.ToString()) != 0 || Double.Parse(longitude.ToString()) != 0))
                {
                    Pin pin = new Pin
                    {
                        Label = dba.ToString(),
                        Address = grade.ToString(),
                        Type = PinType.Place,
                        Position = new Position(Double.Parse(latitude.ToString()), Double.Parse(longitude.ToString()))
                    };
                    map.Pins.Add(pin);
                    positions.Add(new Position(Double.Parse(latitude.ToString()), Double.Parse(longitude.ToString())));
                }
            }
            Distance d = new Distance(1000);

            map.MoveToRegion(FromPositions(positions));
        }

        private static MapSpan FromPositions(IEnumerable<Position> positions)
        {
            double minLat = double.MaxValue;
            double minLon = double.MaxValue;
            double maxLat = double.MinValue;
            double maxLon = double.MinValue;

            foreach (var p in positions)
            {
                minLat = Math.Min(minLat, p.Latitude);
                minLon = Math.Min(minLon, p.Longitude);
                maxLat = Math.Max(maxLat, p.Latitude);
                maxLon = Math.Max(maxLon, p.Longitude);
            }

            return new MapSpan(
                new Position((minLat + maxLat) / 2d, (minLon + maxLon) / 2d),
                maxLat - minLat,
                maxLon - minLon);
        }
    }
}
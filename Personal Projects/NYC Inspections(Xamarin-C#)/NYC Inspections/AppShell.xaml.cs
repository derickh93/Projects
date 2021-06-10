using NYC_Inspections.Views;
using Xamarin.Forms;

namespace NYC_Inspections
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(List), typeof(List));
        }
    }
}
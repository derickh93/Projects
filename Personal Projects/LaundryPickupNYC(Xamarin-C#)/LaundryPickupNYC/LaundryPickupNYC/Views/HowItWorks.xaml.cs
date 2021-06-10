using Syncfusion.SfCarousel.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaundryPickupNYC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HowItWorks : ContentPage
    {
        public HowItWorks()
        {
            InitializeComponent();
            SfCarousel carousel = new SfCarousel()
            {
                ItemWidth = 170,
                ItemHeight = 250
            };
            ObservableCollection<SfCarouselItem> carouselItems = new ObservableCollection<SfCarouselItem>();
            carouselItems.Add(new SfCarouselItem() { ImageName = "image1.jpg" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "image2.jpg" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "image3.jpg" });

            carousel.ItemsSource = carouselItems;

            this.Content = carousel;
        }
    }
}
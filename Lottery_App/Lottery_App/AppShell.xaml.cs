using Lottery_App.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Lottery_App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }

    }
}

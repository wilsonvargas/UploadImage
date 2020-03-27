using System;
using UploadImagesSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UploadImagesSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

            var navPage = MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex(ConstantsString.ColorNavigationBarBlue);
            navPage.BarTextColor = Color.White;
            SetBatchmode();
        }

        private void SetBatchmode()
        {
            if (!Application.Current.Properties.ContainsKey(ConstantsString.BatchModeType))
            {

                Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;

            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

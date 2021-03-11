using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AlgorandPayments.Services;
using AlgorandPayments.Views;

namespace AlgorandPayments
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new HomePage());
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

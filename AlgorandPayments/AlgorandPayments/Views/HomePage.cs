using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorand.Client;
using Algorand;
using Algorand.V2.Model;
using Algorand.V2;
using Account = Algorand.Account;

using Xamarin.Forms;

namespace AlgorandPayments.Views
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Algorand Payment App";
            StackLayout stackLayout = new StackLayout();
            Button button = new Button();
            button.BackgroundColor = Color.Black;
            button.Text = "Make Payment";
            button.Clicked += Button_Clicked;
            stackLayout.Children.Add(button);
            //Second Button
            button = new Button();
            button.BackgroundColor = Color.Black;
            button.Text = "View Transactions";
            button.Clicked += Button_Clicked1;
            stackLayout.Children.Add(button);
            //Third Button
            button = new Button();
            button.BackgroundColor = Color.Black;
            button.Text = "Get Account Balance";
            button.Clicked += Button_Clicked2;
            stackLayout.Children.Add(button);
            Content = stackLayout;
        }

        private async void Button_Clicked2(object sender, EventArgs e)
        {
            string ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps2"; //find in algod.net
            string ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"; //find in algod.token          
            var key = "pet rabbit charge admit cake chapter coyote mandate provide travel victory stamp sleep lizard absurd toward galaxy place kiwi economy indoor innocent grit abandon rose";
            string SRC_ACCOUNT = key;
            Account src = new Account(SRC_ACCOUNT);
            AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var accountInfo = algodApiInstance.AccountInformation(src.Address.ToString());
            await DisplayAlert("Account Balance", $"{accountInfo.Address} has {accountInfo.Amount} Microalgos", "Balance", "Ok");
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaymentPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransactionsPage());
        }
    }
}
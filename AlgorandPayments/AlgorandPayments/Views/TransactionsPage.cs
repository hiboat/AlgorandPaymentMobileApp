using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;
using Algorand.Client;
using Algorand;
using Algorand.V2.Model;
using Algorand.V2;
using Account = Algorand.Account;
using Xamarin.Forms;
using AlgorandPayments.Models;

namespace AlgorandPayments.Views
{
    public class TransactionsPage : ContentPage
    {
        private Entry _AddressEntry;
        private Entry _AmountEntry;
        private Button _SaveEntry;
        //Database
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mydbs.db3");
        public TransactionsPage()
        {
            this.Title = "Process Transaction";
            StackLayout stackLayout = new StackLayout();
            //Receiver Address Entry
            _AddressEntry = new Entry();
            _AddressEntry.Keyboard = Keyboard.Text;
            _AddressEntry.Placeholder = "Receiver Address";
            stackLayout.Children.Add(_AddressEntry);
            //Amount Entry
            _AmountEntry = new Entry();
            _AmountEntry.Keyboard = Keyboard.Text;
            _AmountEntry.Placeholder = "Amount";
            stackLayout.Children.Add(_AmountEntry);
            //Save Button
            _SaveEntry = new Button();
            _SaveEntry.Text = "Proceed with Transfer";
            _SaveEntry.Clicked += _SaveEntry_Clicked;
            stackLayout.Children.Add(_SaveEntry);
            Content = stackLayout;
        }

        private async void _SaveEntry_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Transfer>();
            var maxpk = db.Table<Transfer>().OrderByDescending(i => i.Id).FirstOrDefault();
            Transfer payment = new Transfer()
            {
                Id = (maxpk == null ? 1 : maxpk.Id + 1),
                SenderAddress = "4GIK2BGHFB3BTD2URC4FQLK7TO5XDJ6TYU7NSOZOHL7HZVUHDFFWUIOTNA",
                ReceiverAddress = _AddressEntry.Text,
                Amount = Convert.ToInt32(_AmountEntry.Text)
            };
            var key = "pet rabbit charge admit cake chapter coyote mandate provide travel victory stamp sleep lizard absurd toward galaxy place kiwi economy indoor innocent grit abandon rose";
            
            db.Insert(payment);
            FundMethod(key, payment.ReceiverAddress, payment.Amount, payment.SenderAddress);
            await DisplayAlert("Success", "Transfer of "+ payment.Amount + " algos to "+ payment.ReceiverAddress+ " was successfull.", $"Successfully Transfered {payment.Amount} Algos", "Ok");
            await Navigation.PopAsync();
        }
        public static void FundMethod(string key, string receiver, int amount, string senderAddr)
        {
            string ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps2"; //find in algod.net
            string ALGOD_API_TOKEN = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"; //find in algod.token          
            string SRC_ACCOUNT = key;
            string DEST_ADDR = receiver;
            Account src = new Account(SRC_ACCOUNT);
            AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            try
            {
                var trans = algodApiInstance.TransactionParams();
            }
            catch (ApiException e)
            {
                Console.WriteLine("Exception when calling algod#getSupply:" + e.Message);
            }

            TransactionParametersResponse transParams;
            try
            {
                transParams = algodApiInstance.TransactionParams();
            }
            catch (ApiException e)
            {
                throw new Exception("Could not get params", e);
            }
            var amountsent = Utils.AlgosToMicroalgos(amount);
            var tx = Utils.GetPaymentTransaction(src.Address, new Address(DEST_ADDR), amountsent, "pay message", transParams);
            var signedTx = src.SignTransaction(tx);

            Console.WriteLine("Signed transaction with txid: " + signedTx.transactionID);

            // send the transaction to the network
            try
            {
                var id = Utils.SubmitTransaction(algodApiInstance, signedTx);
                Console.WriteLine("Successfully sent tx with id: " + id.TxId);
                Console.WriteLine(Utils.WaitTransactionToComplete(algodApiInstance, id.TxId));
            }
            catch (ApiException e)
            {
                // This is generally expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#rawTransaction: " + e.Message);
            }
        }
    }
}
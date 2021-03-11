using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using AlgorandPayments.Models;
using Xamarin.Forms;
using System.IO;

namespace AlgorandPayments.Views
{
    public class PaymentPage : ContentPage
    {
        private ListView _listView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mydbs.db3");
        public PaymentPage()
        {
            this.Title = "Transactions";
            var db = new SQLiteConnection(_dbPath);
            StackLayout stackLayout = new StackLayout();
            _listView = new ListView();
            _listView.ItemsSource = db.Table<Transfer>().OrderBy(n => n.Id).ToList();
            stackLayout.Children.Add(_listView);
            Content = stackLayout;
        }
    }
}
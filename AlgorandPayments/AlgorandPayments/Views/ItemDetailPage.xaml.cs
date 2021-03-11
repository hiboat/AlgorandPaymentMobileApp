using System.ComponentModel;
using Xamarin.Forms;
using AlgorandPayments.ViewModels;

namespace AlgorandPayments.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
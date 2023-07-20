using Proj.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Proj.Views
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
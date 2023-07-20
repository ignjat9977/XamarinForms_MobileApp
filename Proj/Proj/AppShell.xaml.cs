using Proj.ViewModels;
using Proj.Views;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace Proj
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(AlbumPage), typeof(AlbumPage));
            Routing.RegisterRoute(nameof(PproductPage), typeof(PproductPage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
            Routing.RegisterRoute(nameof(CategoryUpdatePage), typeof(CategoryUpdatePage));
            Routing.RegisterRoute(nameof(ProductUpdatePpage), typeof(ProductUpdatePpage));


            ServicePointManager.ServerCertificateValidationCallback += (x, y, z, w) => true;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Login");
        }
    }
}

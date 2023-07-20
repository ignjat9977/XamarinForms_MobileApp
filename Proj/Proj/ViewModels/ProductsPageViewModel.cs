using Proj.DataBase;
using Proj.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    public class ProductsPageViewModel : BaseViewModel
    {
        private ObservableCollection<ProductCat> _products;
        private ObservableCollection<Category> _categories;
        private string keyword;
        private List<int> catIds;
        private bool isCheck;

        public bool IsCheck
        {
            get => isCheck;
            set => SetProperty(ref isCheck, value);
        }
        public List<int> CatIds
        {
            get { return catIds; }
            set => SetProperty(ref catIds, value);
        }
        public string Keyword
        {
            get => keyword;
            set
            {
                SetProperty(ref keyword, value);
                LoadProducts();
            }
        }

        public ObservableCollection<ProductCat> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }
        public ProductsPageViewModel()
        {
            Db = new Db();
            IsCheck = false;
            CatIds = new List<int>();
            LoadProducts();
            var category = Db.Conn.Table<Category>().ToList();
            Categories = new ObservableCollection<Category>(category);
            AddNewCommand = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(AddProductPage));
            });
            CheckBoxChangedCommand = new Command<object>(CheckBoxx);
            DeleteCommand = new Command((object o) =>
            
            
            {
                var p = o as ProductCat;
                var product = Db.Conn.Find<Product>(x => x.Id == p.Id);

                Db.Conn.Delete(product);
                Products.Remove(p); 
            });
            EditCommand = new Command<object>(GoToUpdatePage);
        }
        private async void GoToUpdatePage(object o)
        {
            var product = o as ProductCat;
            Application.Current.Properties["Idd"] = product.Id;
            await Shell.Current.GoToAsync($"{nameof(ProductUpdatePpage)}?id={product.Id}");
            
        }
        private void LoadProducts()
        {
            
            var query = "SELECT p.Id, p.Name, p.Description, p.CategoryId, p.Price, p.Image, c.Name as CategoryName " +
                "FROM Products p INNER JOIN Categories c ON p.CategoryId = c.Id";

            if(!string.IsNullOrEmpty(Keyword) || CatIds.Any())
            {
                query += " WHERE";
            }
            if (!string.IsNullOrEmpty(Keyword))
            {
                query += $" c.Name Like '%{Keyword}%' OR p.Name like '%{Keyword}%'";
            }
            if (!string.IsNullOrEmpty(Keyword) && CatIds.Any())
            {
                query += " AND";
            }
            if (CatIds.Any())
            {
                query += $" p.CategoryId IN ";
                query += "(";
                for(int i=0; i<CatIds.Count; i++)
                {
                    if(i < catIds.Count - 1)
                    {
                        query += $"{CatIds[i]},";
                    }
                    else
                    {
                        query += $"{CatIds[i]}";
                    }
                }
                query += ")";
            }
            var product = Db.Conn.Query<ProductCat>(query);

            Products = new ObservableCollection<ProductCat>(product);

            
        }
        public void CheckBoxx(object o)
        {
            var cat = o as Category;
            if (CatIds.Contains(cat.Id))
            {
                CatIds.Remove(cat.Id);
                LoadProducts();
                return;
            }
            
            CatIds.Add(cat.Id);
            LoadProducts();
        }
        public ICommand AddNewCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CheckBoxChangedCommand { get; set; }
        private Db Db { get; }

        public class ProductCat
        {
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public decimal? Price { get; set; }

        }
    }
}

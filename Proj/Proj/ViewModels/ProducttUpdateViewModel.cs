using Proj.DataBase;
using Proj.Models;
using Proj.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class ProducttUpdateViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private string description;
        private decimal? price;
        private string image;

        private string nameError;
        private string priceError;
        private string imageError;

        private string successMessage;

        private bool formValid;

        private ObservableCollection<Category> categories;
        private Category selectedItem;

        public Category SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }
        public bool FormValid
        {
            get => formValid;
            set
            {
                SetProperty(ref formValid, value);

            }
        }
        public string SuccessMessage
        {
            get => successMessage;
            set => SetProperty(ref successMessage, value);
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                NameError = Validate("Name");
            }
        }
        public string NameError
        {
            get => nameError;
            set => SetProperty(ref nameError, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public decimal? Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
                PriceError = Validate("Price");
            }
        }
        public string PriceError
        {
            get => priceError;
            set => SetProperty(ref priceError, value);
        }
        public string Image
        {
            get => image;
            set
            {
                SetProperty(ref image, value);
                ImageError = Validate("Image");
            }
        }
        public string ImageError
        {
            get => imageError;
            set => SetProperty(ref imageError, value);
        }
        private string Validate(string property)
        {
            var validator = new UpdateProductPageValidator();
            var result = validator.Validate(this);

            FormValid = result.IsValid;

            if (!FormValid)
            {
                return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
            }
            return null;
        }
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public ProducttUpdateViewModel()
        {
            FormValid = false;
            Db = new Db();
            var categories = Db.Conn.Table<Category>().ToList();
            var y = Application.Current.Properties["Idd"] as ProductId;
            
            Categories = new ObservableCollection<Category>(categories);
            SelectedItem = Categories.FirstOrDefault();
            var productt = Db.Conn.Find<Product>(x => x.Id == Id);

            Name = productt.Name;
            Description = productt.Description;
            Image = productt.Image;
            Price = productt.Price;

            UpdateCommand = new Command(() =>
            {

                var product = Db.Conn.Find<Product>(x => x.Id == Id);

                product.Name = Name;
                product.Price = Price;
                product.Description = Description;
                product.Image = Image;
                product.CategoryId = SelectedItem.Id;

                Db.Conn.Update(product);

            });
        }
        private Db Db { get; set; }
        public ICommand UpdateCommand { get; }
    }
}

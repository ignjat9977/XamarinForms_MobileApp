using Proj.DataBase;
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
    public class AddProductPageVIewModel : BaseViewModel
    {
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
            var validator = new AddProductPageValidator();
            var result = validator.Validate(this);

            FormValid = result.IsValid;

            if (!FormValid)
            {
                return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
            }
            return null;
        }
        public AddProductPageVIewModel()
        {
            FormValid = false;
            Db = new Db();
            var categories = Db.Conn.Table<Category>().ToList();

            Categories = new ObservableCollection<Category>(categories);
            SelectedItem = Categories.FirstOrDefault();

            AddNewCommand = new Command(AddNewProduct);
        }
        private Db Db { get; set; }
        private void AddNewProduct()
        {
            var product = new Product
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                CategoryId = SelectedItem.Id,
                Image = this.Image
            };
            Db.Conn.Insert(product);
            this.SuccessMessage = "Product is added successfully!";
            this.Name = "";
            this.Description = "";
            this.Price = null;
            this.Image = "";
            SelectedItem = Categories.FirstOrDefault();
        }
        
        public ICommand AddNewCommand { get; }
    }
}

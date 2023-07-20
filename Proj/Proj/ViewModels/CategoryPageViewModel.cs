using Proj.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    public class CategoryPageViewModel : BaseViewModel
    {
        private string name;
        private string nameError;
        private string updateName;
        private string updateNameError;
        private bool formValid;
        private bool isFormValid;
        private ObservableCollection<Category> categories;
        private Category selectedCategory;
        private bool isCategorySelected = false;
        private bool isButtonActivated = false;
        public bool IsCategorySelected
        {
            get => isCategorySelected;
            set => SetProperty(ref isCategorySelected, value);
        }
        public bool IsButtonActivated
        {
            get => isButtonActivated;
            set => SetProperty(ref isButtonActivated, value);
        }
        public Category SelectedCategory
        {
            get => selectedCategory;
            set 
            {
                if (value != null)
                {
                    IsCategorySelected = true;
                }
                SetProperty(ref selectedCategory, value);
            }
        }

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                var trimmed = value.Trim();
                if (string.IsNullOrEmpty(value))
                {
                    NameError = "Name cant be empty!";
                    return;
                }
                if (string.IsNullOrEmpty(trimmed))
                {
                    NameError = "Name cant be empty!";
                    return;
                }
                if (trimmed.Length < 3)
                {
                    NameError = "Name must be longer than 3 character!";
                    return;
                }

                var category = DB.Conn.Find<Category>(x => x.Name == name);
                if (category != null)
                {
                    NameError = "That Name already exsist!";
                    return;
                }
                NameError = null;
            }
        }
        public string NameError
        {
            get => nameError;
            set
            {
                SetProperty(ref nameError, value);
                FormValid = value == null;
            }
        }
        public bool FormValid
        {
            get => formValid;
            set => SetProperty(ref formValid, value);
        }
        public string UpdateName
        {
            get => updateName;
            set
            {
                SetProperty(ref updateName, value);
                var trimmed = value.Trim();
                if (string.IsNullOrEmpty(value))
                {
                    UpdateNameError = "Name cant be empty!";
                    return;
                }
                if (string.IsNullOrEmpty(trimmed))
                {
                    UpdateNameError = "Name cant be empty!";
                    return;
                }
                if (trimmed.Length < 3)
                {
                    UpdateNameError = "Name must be longer than 3 character!";
                    return;
                }

                var category = DB.Conn.Find<Category>(x => x.Name == name);
                if (category != null)
                {
                    UpdateNameError = "That Name already exsist!";
                    return;
                }
                UpdateNameError = null;
            }
        }
        public string UpdateNameError
        {
            get => updateNameError;
            set
            {
                SetProperty(ref updateNameError, value);
                IsFormValid = value == null;

            }
        }
        public bool IsFormValid
        {
            get => isFormValid;
            set => SetProperty(ref isFormValid, value);
        }
        public CategoryPageViewModel()
        {
            FormValid = false;
            DB = new Db();
            var cat = DB.Conn.Table<Category>().ToList();
            Categories = new ObservableCollection<Category>(cat);
            AddNewCommand = new Command(AddNewCategory);
            DeleteCommand = new Command(() =>
            {
                var category = DB.Conn.Find<Category>(x => x.Id == selectedCategory.Id);

                DB.Conn.Delete(category);
                Categories.Remove(SelectedCategory);
                SelectedCategory = null;

            });
            UpdateCommand = new Command(() =>
            {
                IsButtonActivated = !IsButtonActivated;
            });
        }
        private void AddNewCategory()
        {
            var catt = new Category
            {
                Name = this.Name
            };

            DB.Conn.Insert(catt);
            Categories.Add(catt);
            this.Name = "";
        }
        private Db DB { get; set; }
        public ICommand AddNewCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
    }
}

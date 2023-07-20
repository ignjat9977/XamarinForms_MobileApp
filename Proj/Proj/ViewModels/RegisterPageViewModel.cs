using Proj.Validator;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private string firstName;
        private string lastName;
        private string password;
        private string email;
        private string username;

        private string firstNameError, lastNameError, passwordError, emailError, usernameError, registerError;

        private bool formValid;
        public string FirstName 
        {   
            get => firstName;
            set
            {
                SetProperty(ref firstName, value);
                FirstNameError = Validate("FirstName");
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                SetProperty(ref lastName, value);
                LastNameError = Validate("LastName");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                PasswordError = Validate("Password");
            }
        }
        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                EmailError = Validate("Email");
            }
        }
        public string Username
        {
            get => username;
            set
            {
                SetProperty(ref username, value);
                UsernameError = Validate("Username");
            }
        }
        public string FirstNameError
        {
            get => firstNameError;
            set => SetProperty(ref firstNameError, value);
        }
        public string LastNameError
        {
            get => lastNameError;
            set => SetProperty(ref lastNameError, value);
        }
        public string PasswordError
        {
            get => passwordError;
            set => SetProperty(ref passwordError, value);
        }
        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }
        public string UsernameError
        {
            get => usernameError;
            set => SetProperty(ref usernameError, value);
        }
        public string RegisterError
        {
            get => registerError;
            set => SetProperty(ref registerError, value);
        }
        public bool FormValid
        {
            get => formValid;
            set => SetProperty(ref formValid, value); 
        }
        private string Validate(string property)
        {
            var validatior = new RegisterPageValidation();
            var result = validatior.Validate(this);

            FormValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        public RegisterPageViewModel()
        {
            FormValid = false;

            RegisterUserCommand = new Command(RegisterUser);
        }
        private async void RegisterUser()
        {
         
            var registerobj = new
            {
                this.FirstName,
                this.LastName,
                this.Username,
                this.Password,
                this.Email
            };

            var client = new RestClient();

            var request = new RestRequest("https://10.0.2.2:5011/api/Register");
            request.Method = Method.POST;
            request.AddJsonBody(registerobj);

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                await Shell.Current.GoToAsync("Login");
            }
            else
            {
                this.RegisterError = "An error occurred during registration!";
            }
        }
        public ICommand RegisterUserCommand { get; }
    }
}

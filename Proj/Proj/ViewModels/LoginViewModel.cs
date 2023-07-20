using Proj.Models;
using Proj.Validator;
using Proj.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;

        private string emailError;
        private string passwordError;
        private string loginError;

        private bool formValidator;


        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);

                EmailError = Validation("Email");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                PasswordError = Validation("Password");
            }
        }
        public string EmailError
        {
            get => emailError;
            set => SetProperty(ref emailError, value);
        }
        public string PasswordError
        {
            get => passwordError;
            set => SetProperty(ref passwordError, value);
        }
        public string LoginError 
        {
            get => loginError;
            set => SetProperty(ref loginError, value);
        }
        public bool FormValidator
        {
            get=> formValidator;
            set=>SetProperty(ref formValidator, value);
        }
        public string Validation(string property)
        {
            var validation = new LoginPageValidation();
            var result = validation.Validate(this);

            FormValidator = result.IsValid;

            if (!FormValidator)
            {
                var error = result.Errors.FirstOrDefault(x => x.PropertyName == property);

                return error?.ErrorMessage;
            }
            return null;
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            this.Email = "ignjat9977@gmail.com";
            this.Password = "123123123";
        }

        private async void OnLoginClicked(object obj)
        {
            

            var client = new RestClient();

            var request = new RestRequest("https://10.0.2.2:5011/api/Token");

            request.Method = Method.POST;
            request.AddJsonBody(this);

            var response = client.Execute<LoginResponse>(request);

            if (response.IsSuccessful)
            {
                var loginData = response.Data;
                Application.Current.Properties["UserData"] = loginData;
                await Shell.Current.GoToAsync($"//{nameof(AlbumPage)}");
            }
            else
            {
                this.LoginError = "An error occured during logging in!";
            }

            
        }
    }
}

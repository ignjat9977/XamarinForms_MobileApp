using Proj.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proj.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        public ICollection<AlbumDto> Albums { get; set; }
        
        public AlbumViewModel()
        {
            var client = new RestClient();

            var request = new RestRequest("https://10.0.2.2:5011/api/Albums");

            request.Method = Method.GET;

            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;
            request.AddHeader("Authorization", "Bearer " + loginResponse.Token);

            var response = client.Execute<IEnumerable<AlbumDto>>(request);

            Albums = new ObservableCollection<AlbumDto>(response.Data);

            AddNewCommand = new Command(AddNewItem);
            EditCommand = new Command(EditItem);
            DeleteCommand = new Command(DeleteItem);
            
        }
        public void AddNewItem()
        {
            Albums.Add(new AlbumDto { Artist = "Ac/Dc", Name = "Nova Pesma", Image = "https://seeklogo.com/images/A/acdc-logo-9F158CEF86-seeklogo.com.png" });
        }
        public void EditItem(object item)
        {

        }
        public void DeleteItem(object item)
        {
            var album = item as AlbumDto;
            var client = new RestClient();

            
            var request = new RestRequest("https://10.0.2.2:5011/api/Albums/" + album.Id);
            request.Method = Method.DELETE;

            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;
            request.AddHeader("Authorization", "Bearer " + loginResponse.Token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Albums.Remove(album);
            }
            
            

            
        }
        public ICommand AddNewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
    }
}

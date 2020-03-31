using HotelLorteOpgaveWebservice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HotelLorteOpgave.Persistancy
{
    public class PersistanceService
    {
        private static string jsonFileNameEvents = "EventsAsJson.dat";

        public static async void FileCreationEvents()
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(jsonFileNameEvents);
            if (item == null)
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(jsonFileNameEvents);
            }
        }

        //public static async void SaveHotelAsJsonAsync(Hotel hotel) // FIX 
        //{
        //    string usersJsonString = JsonConvert.SerializeObject(events);
        //    SerializeEventsFileAsync(usersJsonString, jsonFileNameEvents);
        //}
        public static async Task<ObservableCollection<Hotel>> LoadHotelsFromJsonAsync()
        {
            //string eventsJsonString = await DeserializeEventsFileAsync(jsonFileNameEvents);


            const string serverUrl = "http://localhost:44337";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await client.GetAsync("api/Hotels");

                    var hotels = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return (ObservableCollection<Hotel>)hotels;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
  
        }
        public static async void SerializeEventsFileAsync(string eventString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, eventString);
        }
        public static async Task<string> DeserializeEventsFileAsync(string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(localFile);
        }

    }
}

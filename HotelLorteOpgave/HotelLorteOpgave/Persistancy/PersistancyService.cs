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
        public static async Task<List<Hotel>> LoadHotelsFromJsonAsync()
        {
            //string eventsJsonString = await DeserializeEventsFileAsync(jsonFileNameEvents);


            const string serverUrl = "http://localhost:51784";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                List<Hotel> hotelList = new List<Hotel>();

                try
                {
                    var response = await client.GetAsync("api/Hotels");

                    var hotels = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        foreach (var item in hotels)
                        {
                            hotelList.Add(item);
                        }

                        return hotelList;
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

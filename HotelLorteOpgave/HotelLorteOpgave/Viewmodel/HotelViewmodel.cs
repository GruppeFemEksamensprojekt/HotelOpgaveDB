using HotelLorteOpgave.Persistancy;
using HotelLorteOpgaveWebservice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLorteOpgave.Viewmodel
{

   public class HotelViewmodel
    {
        public HotelViewmodel()
        {
            Hotels = new ObservableCollection<Hotel>();
            //Hotels.Add(new Hotel() {
            //    Hotel_No = 1,
            //    Name = "Niels",
            //    Address = "Vej"
            //});
            LoadHotelsAsync();
        }
        public ObservableCollection<Hotel> Hotels { get; set; }

        public async void LoadHotelsAsync()
        {
            var HotelList = await PersistanceService.LoadHotelsFromJsonAsync();

                foreach (var hotel in HotelList)
                {
                    Hotels.Add(hotel);
                }                     
        }
    }
}

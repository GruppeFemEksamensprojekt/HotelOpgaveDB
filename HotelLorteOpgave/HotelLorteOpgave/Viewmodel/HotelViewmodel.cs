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
            //LoadHotelsAsync();
            Hotels.Add(new Hotel() { Hotel_No = 1, Address = "Roskilde 4000", Name = "Bo Svendsen" });
        }
        public ObservableCollection<Hotel> Hotels { get; set; }

        public async void LoadHotelsAsync()
        {
            Hotels = await PersistanceService.LoadHotelsFromJsonAsync();

                foreach (var hotel in Hotels)
                {
                    Hotels.Add(hotel);
                }                     
        }
    }
}

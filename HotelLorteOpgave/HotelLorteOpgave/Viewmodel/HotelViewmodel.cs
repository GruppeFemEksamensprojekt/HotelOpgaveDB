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
        private Hotel _selectedHotel;
        public HotelViewmodel()
        {
            Hotels = new ObservableCollection<Hotel>();
            LoadHotelsAsync();
        }
        public ObservableCollection<Hotel> Hotels { get; set; }
        public Hotel SelectedHotel { get { return _selectedHotel; } set { _selectedHotel = value; } }


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

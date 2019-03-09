using MyWebServices.DAL;
using MyWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MyWebServices
{
    /// <summary>
    /// Summary description for RestaurantService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RestaurantService : System.Web.Services.WebService
    {

        private RestaurantDAL _restaurantDAL;
        //ctor
        public RestaurantService()
        {
            _restaurantDAL = new RestaurantDAL();
        }

        [WebMethod]
        public List<Restaurant> GetAllRestaurant()
        {
            return _restaurantDAL.GetAll();
        }

        [WebMethod]
        public Restaurant GetById(int restaurantID)
        {
            return _restaurantDAL.GetByID(restaurantID);
        }

        [WebMethod]
        public List<Restaurant> GetByNama(string namaRestaurant)
        {
            return _restaurantDAL.GetByName(namaRestaurant);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Restaurant> GetAllRestaurantList()
        {
            Restaurant resto1 = new Restaurant
            {
                RestaurantID=1,
                NamaRestaurant="Gudeg Djuminten",
                Alamat = "JL Asem Gede",
                Tanggal = DateTime.Now
            };
            Restaurant resto2 = new Restaurant
            {
                RestaurantID = 2,
                NamaRestaurant = "Sate Klathak Pak Pong",
                Alamat = "JL Imogiri",
                Tanggal = DateTime.Now
            };

            List<Restaurant> listResto = new List<Restaurant>();
            listResto.Add(resto1);
            listResto.Add(resto2);

            return listResto;
        }
    }
}

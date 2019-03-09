using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
//menambahkan koneksi ke sqlserver
using System.Data.SqlClient;
using MyWebServices.Models;

namespace MyWebServices.DAL
{
    public class RestaurantDAL
    {
        private string GetConn()
        {
            return ConfigurationManager
                .ConnectionStrings["SQLServerConn"].ConnectionString;
        }

        public List<Restaurant> GetAll()
        {
            List<Restaurant> listRestaurant = new List<Restaurant>();
            using(SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from SoapDb order by NamaRestaurant";

                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql,conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Restaurant resto = new Restaurant();
                        resto.RestaurantID = Convert.ToInt32(dr["RestaurantID"]);
                        resto.NamaRestaurant = dr["NamaRestaurant"].ToString();
                        resto.Alamat = dr["Alamat"].ToString();
                        resto.Tanggal = Convert.ToDateTime(dr["Tanggal"]);
                        resto.Harga = Convert.ToDecimal(dr["Harga"]);

                        listRestaurant.Add(resto);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return listRestaurant;
        }
    }
}
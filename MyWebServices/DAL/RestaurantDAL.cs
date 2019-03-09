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
                string strSql = @"select * from Restaurants order by NamaRestaurant";

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


        public List<Restaurant> GetByName(string namaRestaurant)
        {
            List<Restaurant> listRestaurant = new List<Restaurant>();
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Restaurants 
                                  where NamaRestaurant = @NamaRestaurant 
                                  order by RestaurantID";

                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("NamaRestaurant", namaRestaurant);

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

        //method untuk mengambil 1 data/record
        public Restaurant GetByID(int restaurantID)
        {
            Restaurant resto = new Restaurant();
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Restaurants where RestaurantID=@RestaurantID";

                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("RestaurantID", restaurantID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        resto.RestaurantID = Convert.ToInt32(dr["RestaurantID"]);
                        resto.NamaRestaurant = dr["NamaRestaurant"].ToString();
                        resto.Alamat = dr["Alamat"].ToString();
                        resto.Tanggal = Convert.ToDateTime(dr["Tanggal"]);
                        resto.Harga = Convert.ToDecimal(dr["Harga"]);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return resto;
        }

        public string TambahRestaurant(Restaurant resto)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"insert into Restaurants(NamaRestaurant,Alamat,Tanggal,Harga) 
                                  values(@NamaRestaurant,@Alamat,@Tanggal,@Harga)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("NamaRestaurant", resto.NamaRestaurant);
                cmd.Parameters.AddWithValue("Alamat", resto.Alamat);
                cmd.Parameters.AddWithValue("Tanggal", resto.Tanggal);
                cmd.Parameters.AddWithValue("Harga", resto.Harga);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return "Data Restaurant berhasil ditambah";
                }
                catch (SqlException sqlEx)
                {
                    return "Data Restaurant Gagal Ditambahkan " + sqlEx.Message;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        } 
    }
}
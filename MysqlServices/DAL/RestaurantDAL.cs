using MysqlServices.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MysqlServices.DAL
{
    public class RestaurantDAL
    {
        private string GetConn()
        {
            return ConfigurationManager
                .ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        //data lebih dari satu record
        public List<Restaurant> GetAll()
        {
            List<Restaurant> listRestaurant = new List<Restaurant>();
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"select * from Restaurants order by NamaRestaurant";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
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
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"select * from Restaurants 
                                  where NamaRestaurant = @NamaRestaurant 
                                  order by RestaurantID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("NamaRestaurant", namaRestaurant);

                MySqlDataReader dr = cmd.ExecuteReader();
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
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"select * from Restaurants where RestaurantID=@RestaurantID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("RestaurantID", restaurantID);

                MySqlDataReader dr = cmd.ExecuteReader();
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
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"insert into Restaurants(NamaRestaurant,Alamat,Tanggal,Harga) 
                                  values(@NamaRestaurant,@Alamat,@Tanggal,@Harga)";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
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
                catch (MySqlException sqlEx)
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

        public string UpdateRestaurant(Restaurant resto)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"update Restaurants set NamaRestaurant=@NamaRestaurant,
                                  Alamat=@Alamat,Tanggal=@Tanggal,Harga=@Harga  
                                  where RestaurantID=@RestaurantID";

                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("NamaRestaurant", resto.NamaRestaurant);
                cmd.Parameters.AddWithValue("Alamat", resto.Alamat);
                cmd.Parameters.AddWithValue("Tanggal", resto.Tanggal);
                cmd.Parameters.AddWithValue("Harga", resto.Harga);
                cmd.Parameters.AddWithValue("RestaurantID", resto.RestaurantID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return "Data Restaurant berhasil di update";
                }
                catch (MySqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public string DeleteRestaurant(string restaurantID)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConn()))
            {
                string strSql = @"delete from Restaurants where RestaurantID=@RestaurantID";
                MySqlCommand cmd = new MySqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("RestaurantID", restaurantID);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return "Data Restaurant dengan ID " + restaurantID + " berhasil didelete";
                }
                catch (MySqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
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
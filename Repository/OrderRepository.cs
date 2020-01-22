using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace OrderManagement.Repository
{
    public class OrderRepository
    {
        private string connectionString = "data source=" + AppDomain.CurrentDomain.BaseDirectory + "\\DB\\mydb.db";
        public string SaveOrders(List<Order> orders)
        {
            try
            {
                foreach (var order in orders)
                {
                    SaveOrder(order);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }

        public void SaveOrder(Order order)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand())
                {
                    comm.Connection = connection;
                    comm.CommandText = "INSERT INTO Orders(FirstName, LastName, Address1, Address2, City, State, ZipCode, " +
                        "ItemID, Quantity, OrderStatusID, Reason) Values(@fName, @lName, @add1, @add2, @city, @state" +
                        ", @zip, @item, @quan, @orderStatusID, @reason)";
                    comm.Parameters.AddWithValue("@fName", order.FirstName);
                    comm.Parameters.AddWithValue("@lName", order.LastName);
                    comm.Parameters.AddWithValue("@add1", order.Address1);
                    comm.Parameters.AddWithValue("@add2", order.Address2);
                    comm.Parameters.AddWithValue("@city", order.City);
                    comm.Parameters.AddWithValue("@state", order.State);
                    comm.Parameters.AddWithValue("@zip", order.ZipCode);
                    comm.Parameters.AddWithValue("@item", order.ItemID);
                    comm.Parameters.AddWithValue("@quan", order.Quantity);
                    comm.Parameters.AddWithValue("@orderStatusID", order.OrderStatusID);
                    comm.Parameters.AddWithValue("@reason", order.Reason);

                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }

        public List<Order> LoadOrders()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                List<Order> orders = new List<Order>();
                using (SQLiteCommand comm = new SQLiteCommand())
                {
                    comm.Connection = connection;
                    comm.CommandText = "Select Id, FirstName, LastName, Address1, Address2, City, State, ZipCode, ItemID, Quantity, Reason, OrderStatusID from Orders;";

                    try
                    {
                        connection.Open();
                        //var returnedObj = comm.ExecuteScalar();

                        SQLiteDataReader reader = comm.ExecuteReader();
                        while (reader.Read())
                        {
                            orders.Add(new Order()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Address1 = reader["Address1"].ToString(),
                                Address2 = reader["Address2"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                ZipCode = reader["ZipCode"].ToString(),
                                ItemID = reader["ItemID"].ToString(),
                                Quantity = reader["Quantity"] == DBNull.Value ? (Nullable<int>)null : Convert.ToInt32(reader["Quantity"]),
                                Reason = reader["Reason"].ToString(),
                                OrderStatusID = Convert.ToInt32(reader["OrderStatusID"])
                            }) ;
                            //string x = reader["FirstName"].ToString();
                        }
                        reader.Close();

                        return orders;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public string UpdateOrders(int[] ids)
        {
            try
            {
                foreach (int i in ids)
                {
                    UpdateOrder(i);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }

        public string UpdateOrder(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                List<Order> orders = new List<Order>();
                using (SQLiteCommand comm = new SQLiteCommand())
                {
                    comm.Connection = connection;
                    comm.CommandText = "Update Orders set OrderStatusID = 3 where Id=@id and OrderStatusID = 1";
                    comm.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return "Success";
        }

        public string DeleteOrders(int[] ids)
        {
            try
            {
                foreach (int i in ids)
                {
                    DeleteOrder(i);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }

        public string DeleteOrder(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                List<Order> orders = new List<Order>();
                using (SQLiteCommand comm = new SQLiteCommand())
                {
                    comm.Connection = connection;
                    comm.CommandText = "Delete from Orders where Id=@id and OrderStatusID = 2";
                    comm.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return "Success";
        }
    }
}
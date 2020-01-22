using OrderManagement.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagement.Controllers
{
    public class UploadController : Controller
    {
        private OrderRepository orderRepo = new OrderRepository();


        // GET: Upload
        public ActionResult Index()
        {
            return View("Upload");
        }

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    if (!fileName.Contains(".csv"))
                    {
                        ViewBag.Message = "File Uploaded is not a .csv, please select a .csv file. File upload failed!!";
                        return View("Upload");
                    }

                    List<Order> orders = new List<Order>();
                    //Read from the file and save it.
                    using (var reader = new StreamReader(file.InputStream))
                    {
                        reader.ReadLine();  //skip first line
                        Order order = new Order();
                        int count = 1;
                        while (!reader.EndOfStream)
                        {
                            var row = reader.ReadLine();
                            orders.Add(ValidateRows(row));
                            count++;
                        }
                        orderRepo.SaveOrders(orders);
                    }
                    for (int i = 0; i < orders.Count; i++)
                    {
                        ViewBag.Message += "Row" + (i+1) + ": " + (orders[i].OrderStatusID == 1 ?  "Created": "Error: "+ orders[i].Reason) + "\n";
                    }
                }
                ViewBag.Message += "File Uploaded Successfully!!";
                return View("Upload");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "File upload failed!!";
                return View("Upload");
            }
        }

        

        private Order ValidateRows(string line)
        {
            var values = line.Split(',');
            string errorMsg = string.Empty;

            Repository.Order order = new Repository.Order();
            order.FirstName = values[0];
            order.LastName = values[1];
            order.Address1 = values[2];
            order.Address2 = values[3];
            order.City = values[4];
            order.State = values[5];
            order.ZipCode = values[6];
            order.ItemID = values[7];
            order.Quantity = values[8] != null && values[8] != string.Empty ? Convert.ToInt32(values[8]): (int?)null;

            return GetOrderStatusAndReason(order);
        }

        private Order GetOrderStatusAndReason(Order order)
        {
            bool isErrored = false; 

            if (order.FirstName == null || order.FirstName == string.Empty)
            {
                isErrored = true;
                order.Reason = "FirstName is null/empty; ";
            }
            if (order.LastName == null || order.LastName == string.Empty)
            {
                isErrored = true;
                order.Reason += "LastName is null/empty; ";
            }
            if (order.Address1 == null || order.Address1 == string.Empty)
            {
                isErrored = true;
                order.Reason += "Address1 is null/empty; ";
            }
            if (order.City == null || order.City == string.Empty)
            {
                isErrored = true;
                order.Reason += "City is null/empty; ";
            }
            if (order.State == null || order.State == string.Empty)
            {
                isErrored = true;
                order.Reason += "State is null/empty; ";
            }
            if (order.ZipCode == null || order.ZipCode == string.Empty)
            {
                isErrored = true;
                order.Reason += "ZipCode is null/empty; ";
            }
            if (order.ItemID == null || order.ItemID == string.Empty)
            {
                isErrored = true;
                order.Reason += "ItemID is null/empty; ";
            }
            if (order.Quantity == null)
            {
                isErrored = true;
                order.Reason += "Quantity is null/empty; ";
            }

            order.OrderStatusID = isErrored ? 2 : 1; // 2 is Errored and 1 is Created.
            return order;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderManagement.Repository;

namespace OrderManagement.Controllers
{
    public class OrdersController : Controller
    {
        private OrderRepository orderRepo = new OrderRepository();
        // GET: Orders
        public ActionResult Index()
        {
            return View("Orders");
        }

        public ActionResult LoadOrders()
        {
            List<Order> orders = orderRepo.LoadOrders();
            return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOrder(int id)
        {
            string returnVal = orderRepo.UpdateOrder(id);
            return View("Orders");
        }

        public ActionResult UpdateOrders(string ids)
        {
            int[] id;

            if (ids.Length <= 1)
            {
                id = new int[] {Convert.ToInt32(ids) };
            }
            else
            {
                id = ids.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            }

            string returnVal = orderRepo.UpdateOrders(id);
            return Json(new { data = returnVal }, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult DeleteOrder(int id)
        {
            string returnVal = orderRepo.DeleteOrder(id);
            return View("Orders");
        }

        public ActionResult DeleteOrders(string ids)
        {
            int[] id;

            if (ids.Length <= 1)
            {
                id = new int[] { Convert.ToInt32(ids) };
            }
            else
            {
                id = ids.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            }
            string returnVal = orderRepo.DeleteOrders(id);
            return Json(new { data = returnVal }, JsonRequestBehavior.AllowGet); ;
        }
    }
}
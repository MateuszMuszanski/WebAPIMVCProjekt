using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [RoutePrefix("api/Produkty")]
    public class ProduktyController : Controller
    {
        // GET: Produkty
        public ActionResult Index()
        {
            IEnumerable<mvcProduktyModel> prodList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Produkty").Result;
            prodList = response.Content.ReadAsAsync<IEnumerable<mvcProduktyModel>>().Result;
            return View(prodList);
        }
        /*
        [HttpGet]
        [Route("all")]
        public ActionResult GetAllProducts()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Produkty").Result;
            return Json(response.Content.ReadAsAsync<IEnumerable<mvcProduktyModel>>().Result.OrderByDescending(p => p.IdProduktu));
        }*/
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcProduktyModel());
            else
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.GetAsync("Produkty/" + id.ToString()).Result;
                return View(responseMessage.Content.ReadAsAsync<mvcProduktyModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcProduktyModel prod)
        {
            if (prod.IdProduktu == 0)
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.PostAsJsonAsync("Produkty", prod).Result;
                TempData["SuccessMessage"] = "Dodano pomyślnie";
            }
            else
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.PutAsJsonAsync("Produkty/" + prod.IdProduktu, prod).Result;
                TempData["SuccessMessage"] = "Zmieniono pomyślnie";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.DeleteAsync("Produkty/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Usunięto pomyślnie";

            return RedirectToAction("Index");
        }
    }
}
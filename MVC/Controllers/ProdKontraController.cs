using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProdKontraController : Controller
    {
        // GET: ProdKontra
        public ActionResult Index()
        {
            IEnumerable<mvcProdKontraModel> prodList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProdKontra").Result;
            prodList = response.Content.ReadAsAsync<IEnumerable<mvcProdKontraModel>>().Result;
            return View(prodList);
        }
        public ActionResult AddOrEdit(int nip = 0)
        {
            if (nip == 0)
                return View(new mvcProdKontraModel());
            else
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.GetAsync("ProdKontra/" + nip.ToString()).Result;
                return View(responseMessage.Content.ReadAsAsync<mvcProduktyModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcProdKontraModel prod)
        {
            if (prod.NIP == 0)
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.PostAsJsonAsync("ProdKontra", prod).Result;
                TempData["SuccessMessage"] = "Dodano pomyślnie";
            }
            else
            {
                HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.PutAsJsonAsync("ProdKontra/" + prod.NIP, prod).Result;
                TempData["SuccessMessage"] = "Zmieniono pomyślnie";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int nip)
        {
            HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.DeleteAsync("ProdKontra/" + nip.ToString()).Result;
            TempData["SuccessMessage"] = "Usunięto pomyślnie";

            return RedirectToAction("Index");
        }
    }
}
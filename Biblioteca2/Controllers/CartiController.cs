using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca2.DAL;
using Biblioteca2.DAL.Entities;
using Biblioteca2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca2.Controllers
{
    public class CartiController : Controller
    {
		private Repository _repo;

		public CartiController()
		{
			_repo = new Repository();
		}

        // GET: Carti
        public ActionResult Index()
        {
			List<CarteEntity> cartiDb = _repo.CitesteCartile();

			List<Carte> carti = new List<Carte>();

			foreach (CarteEntity carteEntity in cartiDb)
			{
				Carte carte = Map(carteEntity);
				carti.Add(carte);
			}

			return View(carti);
        }

		[HttpGet]
		public ActionResult Adauga()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Adauga(Carte model)
		{
			_repo.AdaugaCarte(Map(model));

			return RedirectToAction("Index");
		}

		private Carte Map(CarteEntity entity)
		{
			Carte model = new Carte
			{
				Id = entity.Id,
				DataPublicarii = entity.DataPublicarii,
				Editura = entity.Editura,
				NumeAutor = entity.NumeAutor,
				NumarPagini = entity.NumarPagini,
				NumeleCartii =  entity.NumeleCartii
			};

			return model;
		}

		private CarteEntity Map(Carte model)
		{
			CarteEntity entity = new CarteEntity
			{
				DataPublicarii = model.DataPublicarii,
				Editura = model.Editura,
				NumarPagini = model.NumarPagini,
				NumeAutor = model.NumeAutor,
				NumeleCartii = model.NumeleCartii
			};

			return entity;
		}

        //// GET: Carti/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Carti/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Carti/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Carti/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Carti/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Carti/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Carti/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
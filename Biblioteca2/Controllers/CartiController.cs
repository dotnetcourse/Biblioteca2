using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca2.DAL;
using Biblioteca2.DAL.Entities;
using Biblioteca2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Biblioteca2.Controllers
{
    public class CartiController : Controller
    {
		private Repository _repo;
		private IConfiguration _config;

		public CartiController(IConfiguration config)
		{
			string mysql = config.GetValue<string>("mysqlconn");
			string boo = config.GetValue<string>("mama:subSectiune:boo");
			_repo = new Repository();
			_config = config;
		}

		public ActionResult Foo()
		{
			List<CarteEntity> listaDb = _repo.CitesteCartile();

			List<Carte> listaCarti = new List<Carte>();

			foreach (CarteEntity entity in listaDb)
			{
				Carte model = Map(entity);
				listaCarti.Add(model);
			}

			return Ok(listaCarti.FirstOrDefault());
		}

        // GET: Carti
        public ActionResult Index(string search = null)
        {
			List<CarteEntity> listaDb = _repo.CitesteCartile(search);

			List<Carte> listaCarti = new List<Carte>();

			foreach (CarteEntity entity in listaDb)
			{
				Carte model = Map(entity);
				listaCarti.Add(model);
			}

			ViewBag.search = search;

			return View(listaCarti);
        }

		public ActionResult DescarcaCartile()
		{
			List<CarteEntity> listaDb = _repo.CitesteCartile();
			List<Carte> listaCarti = new List<Carte>();

			foreach (CarteEntity entity in listaDb)
			{
				Carte model = Map(entity);
				listaCarti.Add(model);
			}

			string serializat = JsonConvert.SerializeObject(listaCarti);
			byte[] bytes = Encoding.UTF8.GetBytes(serializat);
			return File(bytes, "application/json", "listaCarti.json");
			//FileStream myFile = System.IO.File.Open();
			//return File(myFile, "application/octet-stream", "myfile.someextention");
		}

		[HttpGet]
		public ActionResult Adauga()
		{
			var autori = new List<Autor> {
				new Autor
				{
					Id  = 1,
					NumeAutor = "Jack London"
				},
				new Autor
				{
					Id = 2,
					NumeAutor = "R. R. Martin"
				}
			};

			ViewBag.Mama = "mama";
			ViewBag.Autori = new SelectList(autori, nameof(Autor.NumeAutor), nameof(Autor.NumeAutor));

			return View();
		}

		[HttpPost]
		public ActionResult Adauga(Carte model)
		{
			if (!TryValidateModel(model))
			{
				return BadRequest();
			}

			_repo.AdaugaCarte(Map(model));

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			_repo.Delete(id);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			List<CarteEntity> listaDb = _repo.CitesteCartile();

			List<Carte> listaCarti = new List<Carte>();

			foreach (CarteEntity entity in listaDb)
			{
				Carte model = Map(entity);
				listaCarti.Add(model);
			}

			Carte carteEditata = listaCarti.Single(x => x.Id == id);

			return View(carteEditata);
		}

		[HttpGet]
		public ActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Upload(List<IFormFile> files)
		{
			string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploaded");

			foreach (IFormFile file in files)
			{
				string randomPart = Guid.NewGuid().ToString();
				string filename = randomPart + Path.GetExtension(file.FileName);

				using (FileStream newFile = System.IO.File.OpenWrite(Path.Combine(uploadPath, filename)))
				{
					file.CopyTo(newFile);
					newFile.Flush();
					
					//Close will be automatically executed when this block finishes
				}
			}

			return View("UploadSuccess");
		}

		public ActionResult Edit(Carte model)
		{
			_repo.Update(model.Id, Map(model));

			return RedirectToAction("Index");
		}

		private Carte Map(CarteEntity entity)
		{
			Carte model = new Carte
			{
				Id = entity.Id,
				DataPublicarii = entity.DataPublicarii,
				Editura = entity.Editura,
				NumarPagini = entity.NumarPagini,
				NumeAutor = entity.NumeAutor,
				NumeleCartii = entity.NumeleCartii
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
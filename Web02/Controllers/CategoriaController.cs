using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web02.Models;
using Web02.Repositories;

namespace Web02.Controllers
{
    public class CategoriaController : Controller
    {
        [HttpGet]
        public ActionResult Index(string busca = "")
        {

            CategoriaRepositorio repositorio = new CategoriaRepositorio();
            List<Categoria> categorias = repositorio.ObterTodos(busca);
            ViewBag.Categorias = categorias;

            return View();
        }
        
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(string nome)
        {
            Categoria categoria = new Categoria();
            categoria.Nome = nome;

            CategoriaRepositorio repositorio = new CategoriaRepositorio();
            int id = repositorio.Inserir(categoria);

            return Redirect("/categoria/");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CategoriaRepositorio repositorio = new CategoriaRepositorio();
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            CategoriaRepositorio repositorio = new CategoriaRepositorio();
            Categoria categoria = repositorio.ObterPeloId(id);

            ViewBag.Categoria = categoria;
            return View();
        }

        [HttpPost]
        public ActionResult Update(int id, string nome)
        {
            CategoriaRepositorio repositorio = new CategoriaRepositorio();
            Categoria categoria = repositorio.ObterPeloId(id);
            categoria.Nome = nome;

            repositorio.Alterar(categoria);
            return RedirectToAction("Editar", new { id = categoria.Id });
        }
    }

}
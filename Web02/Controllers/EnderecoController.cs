using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web02.Models;
using Web02.Repositories;

namespace Web02.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly EnderecoRepositorio repositorio;

        public EnderecoController()
        {
            repositorio = new EnderecoRepositorio();
        }

        [HttpGet]
        public ActionResult Index(string busca)
        {
            List<Endereco> enderecos = repositorio.ObterTodos(busca);
            ViewBag.Endereco = enderecos;

            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(Endereco endereco)
        {
            endereco.RegistroAtivo = true;
            int id = repositorio.Inserir(endereco);
            return Redirect("/endereco/");
        }

        [HttpGet]
        public ActionResult Delete (int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Endereco endereco = repositorio.ObterPeloId(id);
            ViewBag.Endereco = endereco;
            return View();

        }

        [HttpPost]
        public ActionResult Update(Endereco endereco)
        {
            Endereco enderecoUpdate = repositorio.ObterPeloId(endereco.Id);

            enderecoUpdate.Id = endereco.Id;
            enderecoUpdate.Estado = endereco.Estado;
            enderecoUpdate.Cidade = endereco.Cidade;
            enderecoUpdate.Bairro = endereco.Bairro;
            enderecoUpdate.Cep = endereco.Cep;
            enderecoUpdate.Numero = endereco.Numero;
            enderecoUpdate.Complemento = endereco.Complemento;
            enderecoUpdate.RegistroAtivo = endereco.RegistroAtivo;

            repositorio.Alterar(enderecoUpdate);
            return RedirectToAction("Editar", new { id = enderecoUpdate.Id });


        }
    }
}
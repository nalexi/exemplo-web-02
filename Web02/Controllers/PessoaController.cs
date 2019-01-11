using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web02.Models;
using Web02.Repositories;

namespace Web02.Controllers
{
    public class PessoaController : Controller
    {
        private readonly PessoaRepositorio repositorio;

        public PessoaController()
        {
            repositorio = new PessoaRepositorio();

        }

        [HttpGet]
        public ActionResult Index(string busca = "")
        {
            List<Pessoa> pessoas = repositorio.ObterTodos(busca);
            ViewBag.Pessoas = pessoas;
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store (Pessoa pessoa)
        {
            pessoa.RegistroAtivo = true;
            int id = repositorio.Inserir(pessoa);
            return Redirect("/pessoa/");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Pessoa pessoa = repositorio.ObterPeloId(id);
            ViewBag.Pessoa = pessoa;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Pessoa pessoa)
        {
            Pessoa pessoaUpdate = repositorio.ObterPeloId(pessoa.Id);

            pessoaUpdate.Nome = pessoa.Nome;
            pessoaUpdate.Cpf = pessoa.Cpf;
            pessoaUpdate.Rg = pessoa.Rg;
            pessoaUpdate.DataNascimento = pessoa.DataNascimento;
            pessoaUpdate.Sexo = pessoa.Sexo;
            pessoaUpdate.Idade = pessoa.Idade;
            
            repositorio.Alterar(pessoaUpdate);
            return RedirectToAction("Editar", new { id = pessoaUpdate.Id });
        }

    }
}
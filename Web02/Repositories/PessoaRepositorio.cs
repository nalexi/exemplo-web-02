using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web02.Database;
using Web02.Interfaces;
using Web02.Models;

namespace Web02.Repositories
{
    public class PessoaRepositorio : IRepositorio<Pessoa>
    {
        private SqlCommand comando;
        public void Alterar(Pessoa pessoa)
        {

            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE pessoas SET
                                    nome =@NOME,
                                    cpf = @CPF,
                                    rg =@RG,
                                    data_nascimento = @DATA_NASCIMENTO,
                                    sexo = @SEXO,
                                    idade = @IDADE
                                    WHERE id= @ID";
            comando.Parameters.AddWithValue("@NOME", pessoa.Nome);
            comando.Parameters.AddWithValue("@CPF", pessoa.Cpf);
            comando.Parameters.AddWithValue("@RG", pessoa.Rg);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", pessoa.DataNascimento);
            comando.Parameters.AddWithValue("@SEXO", pessoa.Sexo);
            comando.Parameters.AddWithValue("@IDADE", pessoa.Idade);
            comando.Parameters.AddWithValue("@ID", pessoa.Id);

            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {

            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE pessoas SET registro_ativo = 0 WHERE id =@ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Pessoa pessoa)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO pessoas
                                  (nome, cpf, rg, data_nascimento, sexo, idade, registro_ativo)
                                  OUTPUT INSERTED.ID
                                  VALUES (@NOME, @CPF, @RG, @DATA_NASCIMENTO, @SEXO, @IDADE, 1)";
            comando.Parameters.AddWithValue("@NOME", pessoa.Nome);
            comando.Parameters.AddWithValue("@CPF", pessoa.Cpf);
            comando.Parameters.AddWithValue("@RG", pessoa.Rg);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", pessoa.DataNascimento);
            comando.Parameters.AddWithValue("@SEXO", pessoa.Sexo);
            comando.Parameters.AddWithValue("@IDADE", pessoa.Idade);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());

            comando.Connection.Close();
            return id;
        }
        public Pessoa ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM pessoas WHERE id = @ID and registro_ativo =1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            Pessoa pessoa = null;
            if (table.Rows.Count == 1)
            {
                pessoa = new Pessoa();
                DataRow row = table.Rows[0];
                pessoa.Id = Convert.ToInt32(row["id"].ToString());
                pessoa.Nome = row["nome"].ToString();
                pessoa.Cpf = row["cpf"].ToString();
                pessoa.Rg = row["rg"].ToString();
                pessoa.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
                pessoa.Sexo = Convert.ToChar(row["sexo"].ToString());
                pessoa.Idade = Convert.ToInt16(row["idade"].ToString());
                
            }
            comando.Connection.Close();
            return pessoa != null ? pessoa : null; ;
        }

        public List<Pessoa> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM pessoas WHERE registro_ativo = 1 AND nome LIKE @BUSCA ORDER BY nome";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Pessoa> pessoas = new List<Pessoa>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Pessoa pessoa = new Pessoa();
                DataRow row = table.Rows[i];
                pessoa.Id = Convert.ToInt32(row["id"].ToString());
                pessoa.Nome = row["nome"].ToString();
                pessoa.Cpf = row["cpf"].ToString();
                pessoa.Rg = row["rg"].ToString();
                pessoa.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
                pessoa.Sexo = Convert.ToChar(row["sexo"].ToString());
                pessoa.Idade = Convert.ToInt16(row["idade"].ToString());
                pessoas.Add(pessoa);
            }

            /* foreach (DataRow row in table.Rows)
             {
                 Pessoa pessoa = new Pessoa();
                 pessoa.Id = Convert.ToInt32(row["id"].ToString());
                 pessoa.Nome = row["nome"].ToString();
                 pessoa.Cpf = row["cpf"].ToString();
                 pessoa.Rg = row["rg"].ToString();
                 pessoa.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
                 pessoa.Sexo = Convert.ToChar(row["sexo"].ToString());
                 pessoa.Idade = Convert.ToInt16(row["idade"].ToString());
                 pessoas.Add(pessoa);

             }
             */
            comando.Connection.Close();
            return pessoas;
        }
    }
}


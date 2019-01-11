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

    public class EnderecoRepositorio : IRepositorio<Endereco>
    {
        private SqlCommand comando;
        public void Alterar(Endereco endereco)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE enderecos SET
                                    estado =@ESTADO,
                                    cidade = @CIDADE,
                                    bairro =@BAIRRO,
                                    cep = @CEP,
                                    numero = @NUMERO,
                                    complemento = @COMPLEMENTO
                                    WHERE id= @ID";
            comando.Parameters.AddWithValue("@ESTADO", endereco.Estado);
            comando.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
            comando.Parameters.AddWithValue("@BAIRRO", endereco.Bairro);
            comando.Parameters.AddWithValue("@CEP", endereco.Cep);
            comando.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            comando.Parameters.AddWithValue("@ID", endereco.Id);

            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE enderecos SET registro_ativo = 0 WHERE id =@ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Endereco endereco)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO enderecos
                                  (cidade, estado, bairro, cep, numero, complemento, registro_ativo)
                                  OUTPUT INSERTED.ID
                                  VALUES (@CIDADE, @ESTADO, @BAIRRO, @CEP, @NUMERO, @COMPLEMENTO, 1)";
            comando.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
            comando.Parameters.AddWithValue("@BAIRRO", endereco.Bairro);
            comando.Parameters.AddWithValue("@ESTADO", endereco.Estado);
            comando.Parameters.AddWithValue("@CEP", endereco.Cep);
            comando.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());

            comando.Connection.Close();
            return id;

        }

        public Endereco ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM enderecos WHERE id = @ID AND registro_ativo = 1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            Endereco endereco = null;
            if (table.Rows.Count == 1)
            {
                endereco = new Endereco();
                DataRow row = table.Rows[0];
                endereco.Id = Convert.ToInt32(row["id"].ToString());
                endereco.Cidade = row["cidade"].ToString();
                endereco.Bairro = row["bairro"].ToString();
                endereco.Cep = row["cep"].ToString();
                endereco.Numero = Convert.ToInt16(row["numero"]);
                endereco.Complemento = row["complemento"].ToString();
                endereco.Estado = row["estado"].ToString();

            }
            comando.Connection.Close();
            return endereco != null ? endereco : null;
        }

        public List<Endereco> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM enderecos WHERE registro_ativo = 1 AND cidade LIKE @BUSCA ORDER BY cidade";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Endereco> enderecos = new List<Endereco>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Endereco endereco = new Endereco();
                DataRow row = table.Rows[i];
                endereco.Id = Convert.ToInt32(row["id"].ToString());
                endereco.Estado = row["estado"].ToString();
                endereco.Cidade = row["cidade"].ToString();
                endereco.Bairro = row["bairro"].ToString();
                endereco.Cep = row["cep"].ToString();
                endereco.Numero = Convert.ToInt16(row["numero"].ToString());
                endereco.Complemento = row["complemento"].ToString();
                enderecos.Add(endereco);

            }
            comando.Connection.Close();
            return enderecos;

        }
    }
}

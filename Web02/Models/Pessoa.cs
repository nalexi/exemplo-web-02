using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web02.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public short Idade { get; set; }
        public bool RegistroAtivo { get; set; }

        public override string ToString()
        {
            return Id + " " + Nome + " " + Cpf + " " + Rg + " " + DataNascimento + " " + Sexo + " " + Idade + " " + RegistroAtivo;
        }
    }
}

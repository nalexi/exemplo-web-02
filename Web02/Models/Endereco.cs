using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web02.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public short Numero { get; set; }
        public string Complemento { get; set; }
        public bool RegistroAtivo { get; set; }

        public override string ToString()
        {
            return Id + " - " + Estado + " - " + Cidade + " - " + Bairro + " - " + Cep + " - " + Numero + " - " + Complemento;
        }

    }
}

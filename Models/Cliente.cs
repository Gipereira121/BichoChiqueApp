using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BichoChiqueApp.Models
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int idCliente { get; set; }
        public string nomeCliente { get; set; }
        public string emailCliente { get; set; }
        public string senhaCliente { get; set; }
        public string telefoneCliente { get; set; }
        public string enderecoCliente { get; set; }
        public int numeroCliente { get; set; }
        public int cepCliente { get; set; }
        public string bairroCliente { get; set; }
        public int cpfCliente { get; set; }
    }
}

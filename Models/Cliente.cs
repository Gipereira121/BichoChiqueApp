using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BichoChiqueApp.Models
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int cliId { get; set; }

        [NotNull]
        public string cliNome { get; set; }

        public string? cliObs { get; set; }

        public string? cliCPF { get; set; }

        public string? cliEmail { get; set; }

        public DateTime cliDataCadastro { get; set; } = DateTime.Now;

    }
}

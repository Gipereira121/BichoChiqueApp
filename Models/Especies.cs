using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BichoChiqueApp.Models
{
    public class Especies
    {
        [PrimaryKey, AutoIncrement]
        public int espId { get; set; }

        [NotNull]
        public string espNome { get; set; }

        public string? espObs { get; set; }

    }
}

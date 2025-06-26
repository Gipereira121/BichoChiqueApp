using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BichoChiqueApp.Models
{
    public class Animal
    {
        [PrimaryKey, AutoIncrement]
        public int aniId { get; set; } 

        public string aniNome { get; set; }
        public string aniApe { get; set; } // Adicionada a propriedade ausente

        public DateTime aniDataNasc { get; set; }
        public string aniObs { get; set; }

        [Indexed]
        public int codEsp { get; set; }

        [Indexed]
        public int codCli { get; set; }
    }
}

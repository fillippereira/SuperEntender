using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Monitoria.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Cargos", Schema = "dbo")]
    public class Cargo
    {
        private MonitoriaContext db = new MonitoriaContext();

        [Key]
        public int IdCargo { get; set; }
        public string NomeCargo { get; set; }

      
    }
}
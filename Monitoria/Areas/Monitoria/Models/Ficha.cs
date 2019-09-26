using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Monitoria.Areas.Monitoria.Models
{
    public class Ficha
    {   
        [Key]
        public int IdFicha { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Produto { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Status { get; set; }
       /* [Required]
        public List<Bloco> Blocos { get; set; }*/


        public Ficha()
        {

        }

        public Ficha(Ficha ficha)
        {
            
        }
    }
}
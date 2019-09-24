using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Monitoria.Areas.Monitoria.Models
{
    public class Bloco
    {
        [Key]
        public int IdBloco { get; set; }
        [Required]
        public int Nome { get; set; }
        public List<Pergunta> Perguntas { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Monitoria.Areas.Monitoria.Models
{
    public class Resposta
    {
        [Key]
        public int IdResposta { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
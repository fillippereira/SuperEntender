using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Monitoria.Areas.Monitoria.Models
{
    public class Pergunta
    {
        [Key]
        public int IdPergunta { get; set; }
        [Required]
        public string Nome { get; set; }
        //public List<Resposta> Respostas { get; set; }
    }
}
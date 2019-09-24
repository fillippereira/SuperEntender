using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monitoria.Models
{
    public class Usuario
    {

        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "O {0} deve ter {2} caracteres.", MinimumLength = 11)]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        public string Cargo { get; set; }
        public bool LembrarMe { get; set; }
        public string IdSession { get; set; }

        public Usuario()
        {

        }

        public Usuario(RegisterModel model)
        {
           
        }

        
      

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Monitoria.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool LembrarMe { get; set; }
    }

    public class ForgotPaswordModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmaSenha { get; set; }

       
    }

    public class RegisterModel
    {

        [Required]
        [StringLength(11, ErrorMessage = "O {0} deve ter {2} caracteres.", MinimumLength = 1)]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O/A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmaSenha { get; set; }
        [Required]
        public string Cargo { get; set; }
       
        
    }
}
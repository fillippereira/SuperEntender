using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name ="CPF")]
        public string Cpf { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string Login { get; set; }

        [EmailAddress]
        [Display(Name ="E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        public int IdCargo { get; set; }

        [ForeignKey("IdCargo")]
        public virtual Cargo Cargo { get; set; }

        public string Tema { get; set; }
        public string UrlIcone { get; set; }

        public int Status { get; set; }

        public int PrimeiroAcesso { get; set; }
        public string IdSession { get; set; }

        public Usuario()
        {

        }

        public Usuario(RegisterViewModel model)
        {
           
        }


        public string NomeUsuario(string nome)
        {
            var NomeTratado = nome.Split(' ');
            string PrimeiroNome = NomeTratado[0].ToLower();
            string UltimoNome = NomeTratado[NomeTratado.Length - 1].ToLower();
            PrimeiroNome = PrimeiroNome.First().ToString().ToUpper() + PrimeiroNome.Substring(1);
            UltimoNome = UltimoNome.First().ToString().ToUpper() + UltimoNome.Substring(1);

            string NomeExibicao = PrimeiroNome + " " + UltimoNome;
            return NomeExibicao;
        }

    }
}
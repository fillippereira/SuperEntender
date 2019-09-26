using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Monitoria.Models
{
    public class MonitoriaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MonitoriaContext() : base("name=MonitoriaContext")
        {
        }

        public System.Data.Entity.DbSet<Monitoria.Models.Usuario> Usuario { get; set; }

        public System.Data.Entity.DbSet<Monitoria.Areas.Monitoria.Models.Ficha> Fichas { get; set; }

        public System.Data.Entity.DbSet<Monitoria.Areas.Monitoria.Models.Bloco> Blocos { get; set; }

        public System.Data.Entity.DbSet<Monitoria.Areas.Monitoria.Models.Pergunta> Perguntas { get; set; }

        public System.Data.Entity.DbSet<Monitoria.Areas.Monitoria.Models.Resposta> Respostas { get; set; }
    }
}

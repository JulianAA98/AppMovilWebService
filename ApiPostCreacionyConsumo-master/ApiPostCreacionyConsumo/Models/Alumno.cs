using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiPostCreacionyConsumo.Models
{
    [Table("CARACTERISTICAS")]
    public class Alumno
    {
        [Key]
        [Column("IDREGISTRO")]
        public int IdRegistro { get; set; }
        [Column("MATRICULA")]
        public String Matricula { get; set; }
        [Column("NOMBRES")]
        public String Nombres { get; set; }
        [Column("APELLIDOPATERNO")]
        public String ApellidoPaterno { get; set; }
        [Column("APELLIDOMATERNO")]
        public String ApellidoMaterno { get; set; }
        [Column("FECHA")]
        public String Fecha { get; set; }
        [Column("UNIVERSIDAD")]
        public String Universidad { get; set; }
        [Column("ETAPA")]
        public String Etapa { get; set; }
        [Column("TCARTA")]
        public String Tcarta { get; set; }
        [Column("REVISADO")]
        public Boolean Revisado { get; set; }

    }
    ///////

    public class AlumnoConsulta
    {
        public String Matricula { get; set; }
        public String Etapa { get; set; }
    }

   public class AlumnoResponse
    {
        public bool Resultado { get; set; }
    }

}
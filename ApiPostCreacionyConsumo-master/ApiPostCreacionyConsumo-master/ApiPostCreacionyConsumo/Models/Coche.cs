using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiPostCreacionyConsumo.Models
{
    [Table("CARACTERISTICAS")]
    public class Coche
    {
        [Key]
        [Column("IDCOCHE")]
        public int IdCoche { get; set; }
        [Column("MARCA")]
        public String Marca { get; set; }
        [Column("PROCEDENCIA")]
        public String Origen { get; set; }
        [Column("MODELO")]
        public String Modelo { get; set; }
        [Column("CV")]
        public int CV { get; set; }
        [Column("VELOCIDADPUNTA")]
        public int VelocidadMaxima { get; set; }
        [Column("ACELERACION")]
        public decimal Aceleracion { get; set; }
    }
}
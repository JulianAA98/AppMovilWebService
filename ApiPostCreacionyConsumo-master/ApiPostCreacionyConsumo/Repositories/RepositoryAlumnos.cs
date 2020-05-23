using ApiPostCreacionyConsumo.Data;
using ApiPostCreacionyConsumo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiPostCreacionyConsumo.Repositories
{
    public class RepositoryAlumnos
    {
        AlumnosContext bd = new AlumnosContext();

        [ActionName("Consultar")]
        [HttpPost]
        public List<AlumnoConsulta> consultar()
        {
            var VarConsulta = (from Caracteristicas in bd.CARACTERISTICAS
                              select new AlumnoConsulta()
                              {
                                  Matricula = Caracteristicas.Matricula,
                                  Etapa = Caracteristicas.Etapa,
                              }).ToList();
            return VarConsulta;
        }

    }
}
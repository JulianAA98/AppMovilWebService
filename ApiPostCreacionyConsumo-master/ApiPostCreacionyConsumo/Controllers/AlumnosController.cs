using ApiPostCreacionyConsumo.Data;
using ApiPostCreacionyConsumo.Models;
using ApiPostCreacionyConsumo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiPostCreacionyConsumo.Controllers
{
    public class AlumnosController : ApiController
    {  
        AlumnosContext bd = new AlumnosContext();

        [ActionName("Caracteristicas")]
        [HttpPost]
        public List<AlumnoConsulta> consulta()  
        {
            var consulta = (from datos in bd.CARACTERISTICAS
                              select new AlumnoConsulta()
                              {
                                  Etapa = datos.Etapa,
                                  Matricula = datos.Matricula,
                              }).ToList();
            return consulta;
        }

        [ActionName("Insertar")]
        [HttpPost]
        public JsonResult<AlumnoResponse> Insertar(Alumno alumno)
        {
            Alumno Consulta = bd.CARACTERISTICAS
                .Where(m => m.Matricula == alumno.Matricula)
                .Where(m => m.Etapa.ToLower() == alumno.Etapa.ToLower()).FirstOrDefault();
            if (Consulta != null)
            {
                Consulta.Revisado = true;
                bd.SaveChanges();
                return Json(new AlumnoResponse{ Resultado = true});
            }
            return Json(new AlumnoResponse{ Resultado = false });
        }
    }
}

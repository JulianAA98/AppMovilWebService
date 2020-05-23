using ApiPostCreacionyConsumo.Models;
using ApiPostCreacionyConsumo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiPostCreacionyConsumo.Controllers
{
    public class CochesController : ApiController
    {
        RepositoryCoches repo;
        public CochesController()
        {
            this.repo = new RepositoryCoches();
        }
        [HttpGet]
        [Route("api/Marcas")]
        public List<String> GetMarcas()
        {
            return this.repo.GetMarcas();
        }

        [HttpGet]
        [Route("api/BuscarCoches/{marca}")]
        public List<Coche> BuscarCochesPorMarca(String marca)
        {
            return this.repo.GetCochesPorMarca(marca);
        }

        [HttpGet]
        [Route("api/Coches")]
        public List<Coche> GetCoches()
        {
            return this.repo.GetCoches();
        }

    }
}

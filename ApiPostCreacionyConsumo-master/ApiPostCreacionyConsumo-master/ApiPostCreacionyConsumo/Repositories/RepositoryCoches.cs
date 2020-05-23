using ApiPostCreacionyConsumo.Data;
using ApiPostCreacionyConsumo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPostCreacionyConsumo.Repositories
{
    public class RepositoryCoches
    {
        CochesContext context;
        public RepositoryCoches()
        {
            this.context = new CochesContext();
        }

        public List<String> GetMarcas()
        {
            var consulta = from datos in context.Coches
                           select datos.Marca;
            return consulta.ToList();
        }

        public List<Coche> GetCochesPorMarca(String marca)
        {
            var consulta = from datos in context.Coches
                           where datos.Marca == marca
                           select datos;
            return consulta.ToList();
        }
        public List<Coche> GetCoches()
        {
            return this.context.Coches.ToList();
        }
    }
}
using NuevoProyectoRESTfulAPI.Models;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Repos
{
    public class ImplValorRepository : IValorRepository
    {
        public Valor GetValorById(int id)
        {
            return new Valor { 
                id = 1,
                nombre = "C#",
                plataforma = "Unix",
                version = "1.0.1"
            };
        }

        public IEnumerable<Valor> GetValores()
        {
            var valores = new List<Valor> {
                new Valor {
                id = 1,
                nombre = "C#",
                plataforma = "Unix",
                version = "1.0.1"
            },
                new Valor {
                id = 2,
                nombre = "Java",
                plataforma = "Unix",
                version = "1.0.0"
            },
                new Valor {
                id = 3,
                nombre = "C++",
                plataforma = "Unix",
                version = "4.5.1"
            }
            };
            return valores;
        }
    }
}

using NuevoProyectoRESTfulAPI.Models;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Repos
{
    public interface IValorRepository
    {
        IEnumerable<Valor> GetValores();
        Valor GetValorById(int id);
    }
}

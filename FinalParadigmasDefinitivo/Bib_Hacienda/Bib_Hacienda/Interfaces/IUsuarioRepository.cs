using Bib_Hacienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Interfaces
{
    /// <summary>
    /// Repositorio específico para persistencia de usuarios (ADR-03 — SRP/DIP).
    /// </summary>
    public interface IUsuarioRepository
    {
        List<Usuario> CargarUsuarios();
        string GuardarUsuarios(List<Usuario> usuarios);
    }
}

using Bib_Hacienda.Clases;
using Bib_Hacienda.Interfaces;

namespace p_mvcHacienda.Servicios.Repositorios
{
    /// <summary>
    /// Repositorio específico para persistencia de usuarios (ADR-03 — SRP/DIP).
    /// </summary>
    public class UsuarioRepositoryTxt : IUsuarioRepository
    {
        private readonly IAlmacenamiento<Usuario> _almacenamiento;

        public UsuarioRepositoryTxt(IAlmacenamiento<Usuario> almacenamiento)
        {
            _almacenamiento = almacenamiento;
        }

        public List<Usuario> CargarUsuarios()
        {
            try
            {
                var lineas = _almacenamiento.CargarLineas();
                var usuarios = new List<Usuario>();

                foreach (var linea in lineas)
                {
                    var partes = linea.Split('|');
                    if (partes.Length >= 2)
                    {
                        string nombre = partes[0];
                        string contrasena = partes[1];
                        usuarios.Add(new Usuario(nombre, contrasena));
                    }
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
                return new List<Usuario>();
            }
        }

        public string GuardarUsuarios(List<Usuario> usuarios)
        {
            try
            {
                var lineas = usuarios.Select(u => $"{u.Nombre}|{u.Contrasena}").ToList();
                _almacenamiento.GuardarLineas(lineas);
                return "Usuarios guardados exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar usuarios: {ex.Message}", ex);
            }
        }
    }
}

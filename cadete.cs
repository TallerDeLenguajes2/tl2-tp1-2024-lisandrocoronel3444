namespace cadete
{
    using pedidos;
    using System.Linq; // Para usar LINQ en el método GetPedidosEntregados

    public class Cadete
    {
        // Propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccionCadete { get; set; }
        public int telefono { get; set; }
        
        // Constructor
        public Cadete(int id, string nombre, string direccionCadete, int telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccionCadete = direccionCadete;
            this.telefono = telefono;
        }

        // Método que devuelve la información del cadete en lugar de imprimirla
        public string ObtenerInfoCadete()
        {
            return $"Nombre: {nombre}, ID: {id}";
        }
    }
}
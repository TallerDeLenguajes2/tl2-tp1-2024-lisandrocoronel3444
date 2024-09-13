namespace cadete;
using pedidos;
using System.Linq; // Para usar LINQ en el método GetPedidosEntregados

public class Cadete
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string direccionCadete { get; set; }
    public int telefono { get;  set; }
    

    public Cadete(int id, string nombre, string direccionCadete, int telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccionCadete = direccionCadete;
        this.telefono = telefono;
    }

    public void infoCadete(){
        Console.WriteLine($"Nombre:{nombre} ID: {id}");
    }
    
}
namespace cliente;

public class Cliente
{
    // Propiedades de acceso público para facilitar la serialización en APIs web
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public int Telefono { get; set; }

    // Constructor
    public Cliente(string nombre, string direccion, int telefono)
    {
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
    }

    // Constructor vacío para facilitar la deserialización automática en JSON si es necesario
    
}
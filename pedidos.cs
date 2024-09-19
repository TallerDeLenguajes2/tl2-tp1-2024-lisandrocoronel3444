namespace pedidos;
using cliente;
using cadete;

public enum EstadoPedido
{
    Pendiente = 0,
    EnProceso = 1,
    Entregado = 2
}

public class Pedido
{
    public int numeroPedido { get; set; }
    private string observaciones { get; set; } = "";
    private Cliente cliente;
    public EstadoPedido Estado { get; set; }
    public Cadete cadeteAsignado { get; set; }

    public Pedido(int numeroPedido, string observaciones, string nombre, string direccionCliente, int telefono)
    {
        cliente = new Cliente(nombre, direccionCliente, telefono);
        this.numeroPedido = numeroPedido;
        Estado = EstadoPedido.Pendiente;
        this.observaciones = observaciones;
    }

    public string ObtenerDireccionCliente()
    {
        return cliente.Direccion;
    }

    public string ObtenerDatosPedido()
    {
        return $"Número Pedido: {numeroPedido}, Nombre del cliente: {cliente.Nombre}, Teléfono del cliente: {cliente.Telefono}";
    }
}

public class ManejarPedidos
{
    public Pedido CrearPedido(int numeroPedido, string observaciones, string nombreCliente, string direccionCliente, int telefonoCliente)
    {
        // Crear un nuevo pedido
        return new Pedido(numeroPedido, observaciones, nombreCliente, direccionCliente, telefonoCliente);
    }

     public void CambiarEstadoPedido(Pedido pedidoSeleccionado, EstadoPedido nuevoEstado)
    {
        if (pedidoSeleccionado != null)
        {
            pedidoSeleccionado.Estado = nuevoEstado;
        }
    }
}
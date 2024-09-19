namespace pedidos;
using cliente;
using cadete;
public enum EstadoPedido
{
    Pendiente = 0,
    EnProceso = 1,
    Entregado = 2
}
public class Pedido{
    public int numeroPedido{get; set;}
    private string observaciones{get; set;} = "";

    private  Cliente cliente;
    public EstadoPedido Estado { get; set; }

    public Cadete cadeteAsignado{get; set;}
    
    
    
    

    public Pedido(int numeroPedido, string observaciones, string nombre, string direccionCliente, int telefono){
        cliente = new Cliente(nombre, direccionCliente ,telefono);
        this.numeroPedido = numeroPedido;
        Estado = EstadoPedido.Pendiente;
        this.observaciones = observaciones;
        
        
    }

    public void verDireccionCliente(){
        Console.WriteLine("La direccion del cliente es: " + cliente.Direccion);
        
    }
    public void verDatosPedido(){
        Console.WriteLine("Numero Pedido:" + numeroPedido + "Nombre del cliente: " + cliente.Nombre + "Telefono del cliente: " + cliente.Telefono);

    }
    


}
public class manejarPedidos
{
    public Pedido CrearPedido()
    {
        Console.WriteLine("Ingrese el número del pedido:");
        int numeroPedido;
        Int32.TryParse(Console.ReadLine(), out numeroPedido);

        Console.WriteLine("Ingrese las observaciones del pedido:");
        string observaciones = Console.ReadLine();

        Console.WriteLine("Ingrese el nombre del cliente:");
        string nombreCliente = Console.ReadLine();

        Console.WriteLine("Ingrese la dirección del cliente:");
        string direccionCliente = Console.ReadLine();

        Console.WriteLine("Ingrese el teléfono del cliente:");
        int telefonoCliente;
        Int32.TryParse(Console.ReadLine(), out telefonoCliente);

        // Crear un nuevo pedido
        return new Pedido(numeroPedido, observaciones, nombreCliente, direccionCliente, telefonoCliente);
    }

    public void CambiarEstadoPedido(List<Pedido> pedidosLista)
    {
        Console.WriteLine("Seleccione el número del pedido cuyo estado desea cambiar:");

        // Mostrar los pedidos disponibles
        foreach (var pedido in pedidosLista)
        {
            Console.WriteLine($"Número de pedido: {pedido.numeroPedido} - Estado: {pedido.Estado}");
        }

        // Leer el número de pedido del usuario
        int numeroPedido;
        if (!Int32.TryParse(Console.ReadLine(), out numeroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }

        // Buscar el pedido seleccionado
        Pedido pedidoSeleccionado = null;
        foreach (var pedido in pedidosLista)
        {
            if (pedido.numeroPedido == numeroPedido)
            {
                pedidoSeleccionado = pedido;
                break;
            }
        }

        if (pedidoSeleccionado == null)
        {
            Console.WriteLine("Pedido no encontrado.");
            return;
        }

        // Mostrar opciones para el nuevo estado
        Console.WriteLine("Seleccione el nuevo estado del pedido:");
        Console.WriteLine("1. Pendiente");
        Console.WriteLine("2. En Proceso");
        Console.WriteLine("3. Entregado");

        int nuevaOpcionEstado;
        if (!Int32.TryParse(Console.ReadLine(), out nuevaOpcionEstado) || nuevaOpcionEstado < 1 || nuevaOpcionEstado > 3)
        {
            Console.WriteLine("Opción de estado inválida.");
            return;
        }

        // Asignar el nuevo estado al pedido
        pedidoSeleccionado.Estado = (EstadoPedido)(nuevaOpcionEstado - 1);

        Console.WriteLine("Estado del pedido actualizado exitosamente.");
    }
}
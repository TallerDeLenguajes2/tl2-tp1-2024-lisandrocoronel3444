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
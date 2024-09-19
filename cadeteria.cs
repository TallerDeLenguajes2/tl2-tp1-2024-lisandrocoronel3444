namespace cadeteria;
using cadete;
using pedidos;

public class Cadeteria
{
    public string nombre { get; set; }
    public int telefono { get; set; }

    public List<Cadete> listaCadetes { get; set; } = new List<Cadete>();
    public List<Pedido> todosLosPedidos { get; set; } = new List<Pedido>();
    
    public Cadeteria(string nombre, int telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
    }

    public void JornalACobrar(int idCadete)
    {
        int cantEnvios = todosLosPedidos
            .Where(pedido => pedido.cadeteAsignado?.id == idCadete && pedido.Estado == EstadoPedido.Entregado)
            .Count();

        var cadete = listaCadetes.FirstOrDefault(c => c.id == idCadete);
        if (cadete != null)
        {
            Console.WriteLine($"El jornal a cobrar de {cadete.nombre} es de {500 * cantEnvios}");
        }
    }

    public void AsignarCadeteAPedido()
    {
        Console.WriteLine("Ingrese la ID del cadete:");
        InfoTodosCadetes();
        if (int.TryParse(Console.ReadLine(), out int cadeteID))
        {
            Console.WriteLine("Ingrese el número del pedido a asignar:");
            InfoTodosPedidos();
            if (int.TryParse(Console.ReadLine(), out int numPedido))
            {
                var cadete = listaCadetes.FirstOrDefault(c => c.id == cadeteID);
                var pedido = todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numPedido);
                if (cadete != null && pedido != null)
                {
                    pedido.cadeteAsignado = cadete;
                    pedido.Estado = EstadoPedido.Entregado;
                }
            }
        }
    }

    public void RemoverCadeteDePedido()
    {
        Console.WriteLine("Ingrese el número del pedido para remover el cadete:");
        InfoTodosPedidos();
        if (int.TryParse(Console.ReadLine(), out int numPedido))
        {
            var pedido = todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numPedido);
            if (pedido != null && pedido.cadeteAsignado != null)
            {
                Console.WriteLine($"Cadete {pedido.cadeteAsignado.nombre} removido del pedido {pedido.numeroPedido}.");
                pedido.cadeteAsignado = null;
                pedido.Estado = EstadoPedido.Pendiente;
            }
            else
            {
                Console.WriteLine("Este pedido no tiene un cadete asignado.");
            }
        }
        else
        {
            Console.WriteLine("Número de pedido inválido.");
        }
    }

    public void InfoTodosCadetes()
    {
        Console.WriteLine("-----Lista de Cadetes-----");
        foreach (var cadete in listaCadetes)
        {
            cadete.infoCadete();
        }
    }

    public void InfoTodosPedidos()
    {
        Console.WriteLine("-----Lista de Pedidos-----");
        foreach (var pedido in todosLosPedidos)
        {
            pedido.verDatosPedido();
        }
    }

    public void ReasignarPedido()
    {
        RemoverCadeteDePedido();
        AsignarCadeteAPedido();
    }
}
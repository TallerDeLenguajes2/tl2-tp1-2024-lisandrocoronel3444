namespace cadeteria;
using cadete;
using pedidos;
    using System.Linq;
    using System.Collections.Generic;

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
    public Cadeteria(){}

    public string JornalACobrar(int idCadete)
    {
        int cantEnvios = todosLosPedidos
                .Where(pedido => pedido.cadeteAsignado?.id == idCadete && pedido.Estado == EstadoPedido.Entregado)
                .Count();

            var cadete = listaCadetes.FirstOrDefault(c => c.id == idCadete);
            if (cadete != null)
            {
                return $"El jornal a cobrar de {cadete.nombre} es de {500 * cantEnvios}";
            }

            return "Cadete no encontrado";
    }

    public string AsignarCadeteAPedido(int cadeteID, int numPedido)
        {
            var cadete = listaCadetes.FirstOrDefault(c => c.id == cadeteID);
            var pedido = todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numPedido);
            if (cadete != null && pedido != null)
            {
                pedido.cadeteAsignado = cadete;
                pedido.Estado = EstadoPedido.Entregado;
                return $"Cadete {cadete.nombre} asignado al pedido {numPedido}";
            }

            return "Cadete o pedido no encontrado";
        }

    public string RemoverCadeteDePedido(int numPedido)
    {
         var pedido = todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numPedido);
            if (pedido != null && pedido.cadeteAsignado != null)
            {
                string nombreCadete = pedido.cadeteAsignado.nombre;
                pedido.cadeteAsignado = null;
                pedido.Estado = EstadoPedido.Pendiente;
                return $"Cadete {nombreCadete} removido del pedido {numPedido}.";
            }

            return "El pedido no tiene un cadete asignado o no existe.";
    }

    public List<string> InfoTodosCadetes()
        {
            return listaCadetes.Select(cadete => cadete.ObtenerInfoCadete()).ToList();
        }

   public List<string> InfoTodosPedidos()
        {
            return todosLosPedidos.Select(pedido => pedido.ObtenerDatosPedido()).ToList();
        }

     public string ReasignarPedido(int numPedido, int nuevoCadeteID)
        {
            string removerResultado = RemoverCadeteDePedido(numPedido);
            string asignarResultado = AsignarCadeteAPedido(nuevoCadeteID, numPedido);
            return $"{removerResultado} {asignarResultado}";
        }
}
using cadeteria;
using cadete;
using pedidos;
using cliente;
using System.Data.Common;
using controlDatos;


/*TP1 A)Una empresa de cadetería necesita implementar un sistema para asignar pedidos a sus
cadetes y poder luego saber cuántos pedidos despachó cada uno para poder así pagarles su
correspondiente jornal ($500 por cada pedido Entregado)
Tenga en cuenta que: Cada Pedido tiene un Cliente y cada Cadete puede tener uno o
más pedidos. Si se elimina un pedido entonces el cliente tiene que eliminarse también. Un pedido
puede reasignarse a otro Cadete. Es necesario generar informes sobre la actividad de la
cadetería.
2.a A partir del siguiente diseño de clases incompleto, responda las preguntas planteadas
a continuación en el archivo Readme.md:*/
/*TP1 2.b Implemente el sistema de cadetería solicitado utilizando como base el diseño de
clases sugerido, tenga en cuenta que:
A) Debe agregar los métodos faltantes a las clases en función de sus respuestas del
punto anterior.
B) Los datos de la cadetería y de sus cadetes deberán ser cargados
automáticamente a partir de 2 archivos csv, uno por cada entidad.
C) El sistema posee una interfaz de consola para gestión de pedidos para realizar las
siguientes operaciones:
a) dar de alta pedidos
b) asignarlos a cadetes
c) cambiarlos de estado
d) reasignar el pedido a otro cadete.
D) Mostrar un informe de pedidos al finalizar la jornada que incluya el monto ganado
y la cantidad de envíos de cada cadete y el total. Muestre también la cantidad de
envíos promedio por cadete




Refactorización del Sistema para una Cadeteria
El cliente presentó como nuevo requisito que los pedidos puedan no estar asignados a
algún cadete. Esto evidenció una falla en el diseño de clases del sistema, por lo que se decidió
realizar una refactorización del mismo.
Para poder cumplir con dicho requisito se propuso las siguientes modificaciones:
● Quitar el ListadoPedidos de la clase Cadete
● Agregar una referencia a Cadete dentro de la clase Pedido
● Agregar ListadoPedidos en la clase Cadeteria que contenga todo los pedidos que
se vayan generando.
● Agregar el método JornalACobrar en la clase Cadeteria que recibe como
parámetro el id del cadete y devuelve el monto a cobrar para dicho cadete
● Agregar el método AsignarCadeteAPedido en la clase Cadeteria que recibe como
parámetro el id del cadete y el id del Pedido
i) Implemente las modificaciones sugeridas más todas aquellas que crea necesarias
para cumplir con los requerimientos.
ii) Modifique la interfaz de usuario para cumplir con los nuevos requerimientos.
3) Se desea migrar además la carga de datos inicial a un archivo Json, sin perder la posibilidad
de seguir accediendo a los datos guardados en el archivo csv. Para ello se propone un nuevo
diseño de acceso a datos basado en los principios de herencia y polimorfismo.
Este nuevo diseño consta de una clase base llamada AccesoADatos y dos clases derivadas
llamadas AccesoCSV y AccesoJSON, donde se implementaran.
Refactorizar la clase AccesoADatos (que implementó en el práctico anterior) para que ésta
cumpla con lo solicitado.
Modifique la interfaz de usuario para que al inicio del sistema se pida que tipo de acceso usar
(CSV o JSON). y en función de esto instanciar el objeto a datos adecuado.*/

string direccionCadeteria;
string direccionCadete;
Cadeteria nuevaCadeteria = null;

while (true)
{
    Console.WriteLine("Seleccione el tipo de archivo para cargar los datos");
    Console.WriteLine("1.CSV 2.JSON");
    int.TryParse(Console.ReadLine(), out int tipoArchivo);
    if (tipoArchivo == 1)
    {
        IAccesoADatos accesoDatosCSV = new AccesoCSV();
        direccionCadeteria = "cadeteria.csv";
        direccionCadete = "cadete.csv";
        nuevaCadeteria = accesoDatosCSV.cargarCadeteria(direccionCadeteria);
        nuevaCadeteria.listaCadetes = accesoDatosCSV.cargarCadetes(direccionCadete);
        break;

    }
    else if (tipoArchivo == 2)
    {
        IAccesoADatos accesoDatosJSON = new AccesoJSON();
        direccionCadeteria = "cadeteria.json";
        direccionCadete = "cadetes.json";
        nuevaCadeteria = accesoDatosJSON.cargarCadeteria(direccionCadeteria);
        nuevaCadeteria.listaCadetes = accesoDatosJSON.cargarCadetes(direccionCadete);
        break;


    }
    else
    {
        Console.WriteLine("Opción no válida. Por favor, intente nuevamente.");
    }
}




while (true)
{
    Console.WriteLine("Sistema de Gestión de Pedidos");
    Console.WriteLine("1. Dar de alta pedidos");
    Console.WriteLine("2. Asignar pedidos a cadetes");
    Console.WriteLine("3. Cambiar estado de pedidos");
    Console.WriteLine("4. Reasignar pedidos a otro cadete");
    Console.WriteLine("5. Mostrar informe de la jornada");
    Console.WriteLine("6. Salir");
    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Pedido nuevoPedido = CrearPedido();
            nuevaCadeteria.todosLosPedidos.Add(nuevoPedido);
            Console.WriteLine("Pedido creado exitosamente.");
            break;
        case "2":
            nuevaCadeteria.AsignarCadeteAPedido();
            break;
        case "3":
            CambiarEstadoPedido(nuevaCadeteria.todosLosPedidos);
            break;
        case "4":
            nuevaCadeteria.ReasignarPedido();
            break;

        case "5":
            foreach (var cadetes in nuevaCadeteria.listaCadetes)
            {
                nuevaCadeteria.JornalACobrar(cadetes.id);
            }

            break;
        case "6":
            return; // Salir del programa
        default:
            Console.WriteLine("Opción inválida. Intente nuevamente.");
            break;
    }

}


static Pedido CrearPedido()
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

static void CambiarEstadoPedido(List<Pedido> pedidosLista)
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

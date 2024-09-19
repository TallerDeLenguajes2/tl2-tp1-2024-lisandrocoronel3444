using cadeteria;
using cadete;
using pedidos;
using cliente;
using controlDatos;

string direccionCadeteria;
string direccionCadete;
Cadeteria nuevaCadeteria;
ManejarPedidos pedidosManejo = new ManejarPedidos();

while (true)
{
    Console.WriteLine("Seleccione el tipo de archivo para cargar los datos");
    Console.WriteLine("1. CSV 2. JSON");
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
            // Solicitar datos para crear un nuevo pedido
            Console.WriteLine("Ingrese el número del pedido:");
            int numeroPedido = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese las observaciones del pedido:");
            string observaciones = Console.ReadLine();

            Console.WriteLine("Ingrese el nombre del cliente:");
            string nombreCliente = Console.ReadLine();

            Console.WriteLine("Ingrese la dirección del cliente:");
            string direccionCliente = Console.ReadLine();

            Console.WriteLine("Ingrese el teléfono del cliente:");
            int telefonoCliente = int.Parse(Console.ReadLine());

            Pedido nuevoPedido = pedidosManejo.CrearPedido(numeroPedido, observaciones, nombreCliente, direccionCliente, telefonoCliente);
            nuevaCadeteria.todosLosPedidos.Add(nuevoPedido);
            Console.WriteLine("Pedido creado exitosamente.");
            break;

        case "2":
            // Asignar cadete a un pedido
            Console.WriteLine("Ingrese el número del pedido al que desea asignar un cadete:");
            int numeroPedidoAsignar = int.Parse(Console.ReadLine());

            Pedido pedidoAsignar = nuevaCadeteria.todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numeroPedidoAsignar);
            if (pedidoAsignar != null)
            {
                Console.WriteLine("Seleccione el ID del cadete para asignar:");
                foreach (var cadete in nuevaCadeteria.listaCadetes)
                {
                    Console.WriteLine($"ID: {cadete.id}, Nombre: {cadete.nombre}");
                }

                int idCadete = int.Parse(Console.ReadLine());
                Cadete cadeteSeleccionado = nuevaCadeteria.listaCadetes.FirstOrDefault(c => c.id == idCadete);
                if (cadeteSeleccionado != null)
                {
                    pedidoAsignar.cadeteAsignado = cadeteSeleccionado;
                    Console.WriteLine("Cadete asignado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Cadete no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Pedido no encontrado.");
            }
            break;

        case "3":
            // Cambiar estado de un pedido
            Console.WriteLine("Ingrese el número del pedido cuyo estado desea cambiar:");
            int numeroPedidoEstado = int.Parse(Console.ReadLine());

            Pedido pedidoEstado = nuevaCadeteria.todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numeroPedidoEstado);
            if (pedidoEstado != null)
            {
                Console.WriteLine("Seleccione el nuevo estado del pedido:");
                Console.WriteLine("0. Pendiente");
                Console.WriteLine("1. En Proceso");
                Console.WriteLine("2. Entregado");
                int nuevoEstado = int.Parse(Console.ReadLine());

                pedidosManejo.CambiarEstadoPedido(pedidoEstado, (EstadoPedido)nuevoEstado);
                Console.WriteLine("Estado del pedido actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Pedido no encontrado.");
            }
            break;

        case "4":
            // Reasignar un pedido
            Console.WriteLine("Ingrese el número del pedido que desea reasignar:");
            int numeroPedidoReasignar = int.Parse(Console.ReadLine());

            Pedido pedidoReasignar = nuevaCadeteria.todosLosPedidos.FirstOrDefault(p => p.numeroPedido == numeroPedidoReasignar);
            if (pedidoReasignar != null)
            {
                Console.WriteLine("Seleccione el nuevo ID del cadete para reasignar:");
                foreach (var cadete in nuevaCadeteria.listaCadetes)
                {
                    Console.WriteLine($"ID: {cadete.id}, Nombre: {cadete.nombre}");
                }

                int nuevoIdCadete = int.Parse(Console.ReadLine());
                Cadete nuevoCadeteSeleccionado = nuevaCadeteria.listaCadetes.FirstOrDefault(c => c.id == nuevoIdCadete);
                if (nuevoCadeteSeleccionado != null)
                {
                    pedidoReasignar.cadeteAsignado = nuevoCadeteSeleccionado;
                    Console.WriteLine("Pedido reasignado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Cadete no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Pedido no encontrado.");
            }
            break;

        case "5":
            // Mostrar informe de la jornada
            foreach (var cadete in nuevaCadeteria.listaCadetes)
            {
                Console.WriteLine($"Cadete ID: {cadete.id}, Monto a cobrar: {nuevaCadeteria.JornalACobrar(cadete.id)}");
            }
            break;

        case "6":
            return; // Salir del programa

        default:
            Console.WriteLine("Opción inválida. Intente nuevamente.");
            break;
    }
}
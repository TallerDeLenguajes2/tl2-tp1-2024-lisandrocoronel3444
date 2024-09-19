using cadete;
using cadeteria;
using System.IO;
using System.Text.Json;
namespace controlDatos;
using pedidos;
public interface IAccesoADatos
    {
        Cadeteria cargarCadeteria(string archivoCadeteria);
        List<Cadete> cargarCadetes(string archivoCadete);
    }

    public class AccesoCSV : IAccesoADatos
    {
        public Cadeteria cargarCadeteria(string archivoCadeteria)
        {
            if (!File.Exists(archivoCadeteria))
            {
                throw new FileNotFoundException("El archivo CSV de Cadetería no existe.");
            }

            string[] lines = File.ReadAllLines(archivoCadeteria);
            if (lines.Length == 0)
            {
                throw new InvalidDataException("El archivo CSV de Cadetería está vacío.");
            }

            string[] valores = lines[0].Split(";");
            string nombreCadeteria = valores[0];

            if (!int.TryParse(valores[1], out int telefonoCadeteria))
            {
                throw new InvalidDataException("Formato inválido en el archivo de Cadetería.");
            }

            return new Cadeteria(nombreCadeteria, telefonoCadeteria);
        }

        public List<Cadete> cargarCadetes(string archivoCadete)
        {
            List<Cadete> cadetes = new List<Cadete>();

            if (!File.Exists(archivoCadete))
            {
                throw new FileNotFoundException("El archivo CSV de Cadetes no existe.");
            }

            string[] lineaCadetes = File.ReadAllLines(archivoCadete);
            foreach (var lineaDatos in lineaCadetes)
            {
                var valores = lineaDatos.Split(',');

                if (valores.Length < 4 || 
                    !int.TryParse(valores[0], out int id) || 
                    !int.TryParse(valores[3], out int numero))
                {
                    throw new InvalidDataException($"Formato inválido en la línea: {lineaDatos}");
                }

                Cadete cadete = new Cadete(id, valores[1], valores[2], numero);
                cadetes.Add(cadete);
            }

            return cadetes;
        }
    }

    public class AccesoJSON : IAccesoADatos
    {
        public Cadeteria cargarCadeteria(string archivoCadeteria)
        {
            if (!File.Exists(archivoCadeteria))
            {
                throw new FileNotFoundException("El archivo JSON de Cadetería no existe.");
            }

            var jsonString = File.ReadAllText(archivoCadeteria);
            var cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString);

            return cadeteria ?? new Cadeteria("sin nombre", 0); // Retorna una cadetería por defecto
        }

        public List<Cadete> cargarCadetes(string archivoCadete)
        {
            if (!File.Exists(archivoCadete))
            {
                throw new FileNotFoundException("El archivo JSON de Cadetes no existe.");
            }

            string jsonString = File.ReadAllText(archivoCadete);
            var cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonString);

            return cadetes ?? new List<Cadete>(); // Retorna una lista vacía si no se deserializa
        }
    }
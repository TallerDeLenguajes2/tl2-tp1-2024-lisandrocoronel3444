using cadete;
using cadeteria;
using System.IO;
using System.Text.Json;
namespace controlDatos;
using pedidos;
public interface IAccesoADatos
{
    public  Cadeteria cargarCadeteria(string archivoCadeteria);
    public List<Cadete> cargarCadetes(string archivoCadete);
}
public class AccesoCSV : IAccesoADatos
{

    public Cadeteria cargarCadeteria(string archivoCadeteria)
    {
        if (File.Exists(archivoCadeteria))
        {
            string[] lines = File.ReadAllLines(archivoCadeteria);
            if (lines.Length > 0)
            {
                string[] valores = lines[0].Split(";");
                string nombreCadeteria = valores[0];
                int telefonoCadeteria;
                Int32.TryParse(valores[1], out telefonoCadeteria);

                return new Cadeteria(nombreCadeteria, telefonoCadeteria);
            }
        }
        throw new FileNotFoundException("El archivo CSV de Cadetería no existe o está vacío.");
    }

    public List<Cadete> cargarCadetes(string archivoCadete)
    {
        List<Cadete> cadetes = new List<Cadete>();
        if (File.Exists(archivoCadete))
        {
            string[] lineaCadetes = File.ReadAllLines(archivoCadete);
            foreach (var lineaDatos in lineaCadetes)
            {
                var valores = lineaDatos.Split(',');

                int id;
                Int32.TryParse(valores[0], out id);
                int numero;
                Int32.TryParse(valores[3], out numero);

                Cadete cadete = new Cadete(id, valores[1], valores[2], numero);
                cadetes.Add(cadete);
            }
        }
        else
        {
            throw new FileNotFoundException("El archivo CSV de Cadetes no existe.");
        }
        return cadetes;
    }
}

public class AccesoJSON : IAccesoADatos
{
    

    public Cadeteria cargarCadeteria(string archivoCadeteria)
    {
        var jsonString = File.ReadAllText(archivoCadeteria);
        var cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString);

        // Asegúrate de que las listas estén inicializadas
        if (cadeteria == null)
        {
            return new Cadeteria("sin nombre", 0);
        }else{

        return cadeteria;
        }


    }

    public List<Cadete> cargarCadetes(string archivoCadete)
    {
    
            string jsonString = File.ReadAllText(archivoCadete);
            var cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonString);

            if (cadetes == null)
            {
                return new List<Cadete>();
            }else{

              return cadetes;
            }

        
       
    }
}
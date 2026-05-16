namespace CampusNetGraph.Model
{
    public class Vertex
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Rol { get; set; }

        public Vertex(string id, string nombre, string rol)
        {
            Id = id;
            Nombre = nombre;
            Rol = rol;
        }
    }
}
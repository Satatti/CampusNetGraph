using CampusNetGraph.Model;
using CampusNetGraph.View;
using System.Collections.Generic;

namespace CampusNetGraph.Controller
{
    public class GraphController
    {
        private Graph graph;

        private ConsoleView view;


        public GraphController()
        {
            graph = new Graph();

            view = new ConsoleView();
        }

        public void Start()
        {
            graph.AddVertex("U01", "Santiago", "Estudiante");
            graph.AddVertex("U02", "Laura", "Profesor");
            graph.AddVertex("U03", "Carlos", "Egresado");
            graph.AddVertex("U04", "Ana", "Estudiante");
            graph.AddVertex("U05", "Miguel", "Profesor");
            graph.AddVertex("U06", "Valeria", "Estudiante");
            graph.AddVertex("U07", "David", "Egresado");
            graph.AddVertex("U08", "Camila", "Profesor");
            graph.AddVertex("U09", "Juan", "Estudiante");
            graph.AddVertex("U10", "Sara", "Egresado");
            graph.AddVertex("U11", "Felipe", "Profesor");
            graph.AddVertex("U12", "Lucia", "Estudiante");
            graph.AddVertex("U13", "Andres", "Estudiante");
            graph.AddVertex("U14", "Paula", "Profesor");

            view.ShowMessage("Usuarios agregados al grafo.");

            //Construccion del grafo
            graph.AddEdge("U01", "U02");
            graph.AddEdge("U01", "U03");
            graph.AddEdge("U01", "U04");
            graph.AddEdge("U01", "U05");

            graph.AddEdge("U02", "U03");
            graph.AddEdge("U02", "U06");
            graph.AddEdge("U02", "U07");
            graph.AddEdge("U02", "U08");

            graph.AddEdge("U03", "U01");

            graph.AddEdge("U04", "U05");
            graph.AddEdge("U04", "U06");

            graph.AddEdge("U05", "U07");
            graph.AddEdge("U05", "U08");

            graph.AddEdge("U06", "U09");
            graph.AddEdge("U07", "U10");

            graph.AddEdge("U08", "U09");
            graph.AddEdge("U08", "U11");

            graph.AddEdge("U09", "U12");
            graph.AddEdge("U10", "U11");

            view.ShowMessage("\nRelaciones Agregadas.");

            view.ShowMessage("\nLISTA DE ADYACENCIA:");

            view.ShowGraph(graph);

            view.ShowMessage("\n=== BFS DESDE U01 ===");

            ShowBFS("U01");

            view.ShowMessage("\n=== BFS DESDE U04 ===");

            ShowBFS("U04");

            view.ShowMessage("\n=== BFS DESDE U08 ===");

            ShowBFS("U08");

            view.ShowMessage("\n=== DFS COMPLETO ===");

            List<string> dfs = graph.DFS();

            view.ShowMessage("Orden de descubrimiento:");
            foreach (string vertex in dfs)
            {
                view.ShowMessage(vertex);
            }

            view.ShowMessage("\n=== DETECCION DE CICLOS ===");
            bool hasCycle = graph.HasCycle();
            if (hasCycle)
            {
                view.ShowMessage("El grafo SI contiene ciclos.");
            }
            else
            {
                view.ShowMessage("El grafo NO contiene ciclos.");
            }
            view.ShowMessage("\n=== USUARIOS SIN SEGUIDORES ===");

            List<string> noFollowers = graph.GetUsersWithoutFollowers();

            foreach (string user in noFollowers)
            {
                view.ShowMessage(user);
            }

            view.ShowMessage("\n=== USUARIOS INFLUYENTES ===");

            List<string> influentialUsers = graph.GetMostInfluentialUsers();

            foreach (string user in influentialUsers)
            {
                view.ShowMessage(user);
            }

            view.ShowMessage("\n=== USUARIOS MAS ACTIVOS ===");

            List<string> activeUsers = graph.GetMostActiveUsers();

            foreach (string user in activeUsers)
            {
                view.ShowMessage(user);
            }

            view.ShowMessage("\n=== ALCANZABILIDAD ===");

            bool reachable = graph.CanReach("U01", "U12");

            if (reachable)
            {
                view.ShowMessage("U01 SI puede llegar a U12");
            }
            else
            {
                view.ShowMessage("U01 NO puede llegar a U12");
            }

            view.ShowMessage("\n=== ELIMINAR USUARIO ===");

            bool removed = graph.RemoveVertex("U12");

            if (removed)
            {
                view.ShowMessage("Usuario U12 eliminado correctamente.");
            }
            else
            {
                view.ShowMessage("Usuario no encontrado.");
            }

            view.ShowMessage("\nGrafo actualizado:");

            view.ShowGraph(graph);


            view.ShowMessage("\n=== ACTUALIZAR USUARIO ===");

            bool updated = graph.UpdateVertex(
                "U02",
                "Laura Actualizada",
                "Egresado");

            if (updated)
            {
                view.ShowMessage("Usuario actualizado correctamente.");

                var user = graph.Vertices["U02"];

                view.ShowMessage(
                    user.Id + " - "
                    + user.Nombre + " - "
                    + user.Rol
                );
            }
            else
            {
                view.ShowMessage("Usuario no encontrado.");
            }


            view.ShowMessage("\n=== ELIMINAR RELACION ===");

            bool edgeRemoved = graph.RemoveEdge("U01", "U03");

            if (edgeRemoved)
            {
                view.ShowMessage("Relacion eliminada correctamente.");
            }
            else
            {
                view.ShowMessage("Relacion no encontrada.");
            }

            view.ShowMessage("\nGrafo actualizado:");

            view.ShowGraph(graph);

        }


        private void ShowBFS(string startId)
        {
            List<string> recorrido = graph.BFS(startId);

            view.ShowMessage("Orden de visita:");

            foreach (string vertex in recorrido)
            {
                view.ShowMessage(vertex);
            }

            view.ShowMessage(
                "Cantidad de vertices alcanzados: "
                + recorrido.Count
            );
        }
    }
}
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//Estructura de datos para representar un grafo utilizando una lista de adyacencia

namespace CampusNetGraph.Model
{
    public class Graph
    {
        private Dictionary<string, Vertex> vertices;

        private Dictionary<string, List<string>> adjacencyList;

        public Graph()
        {
            vertices = new Dictionary<string, Vertex>();

            adjacencyList = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, Vertex> Vertices
        {
            get { return vertices; }
        }

        public Dictionary<string, List<string>> AdjacencyList
        {
            get { return adjacencyList; }
        }
    

    //CRUD de Usuarios, vertices
    public bool AddVertex(string id, string nombre, string rol)
        {
            if (vertices.ContainsKey(id))
            {
                return false;
            }

            Vertex vertex = new Vertex(id, nombre, rol);

            vertices.Add(id, vertex);

            adjacencyList.Add(id, new List<string>());

            return true;
        }

        // AddEdge, aristas
        public bool AddEdge(string from, string to)
        {
            if (!vertices.ContainsKey(from) || !vertices.ContainsKey(to))
            {
                return false;
            }
            if (adjacencyList[from].Contains(to))
            {
                return false;
            }
            adjacencyList[from].Add(to);
            return true;
        }

        //BFS, Agregar un método para realizar una búsqueda en anchura (BFS) en el grafo, dado un vértice de inicio, y devolver una lista de los vértices visitados en el orden de la búsqueda.
        public List<string> BFS(string startId)
        {
            if (!vertices.ContainsKey(startId))
            {
                return new List<string>();
            }
            Queue<string> queue = new Queue<string>();

            HashSet<string> visited = new HashSet<string>();

            List<string> result = new List<string>();

            queue.Enqueue(startId);

            visited.Add(startId);
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                result.Add(current);
                foreach (string neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);

                        queue.Enqueue(neighbor);
                    }
                }
            }
            return result;
        }

        //Metodo DFS
        public List<string> DFS()
        {
            HashSet<string> visited = new HashSet<string>();

            List<string> result = new List<string>();

            foreach (string vertex in vertices.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    DFSVisit(vertex, visited, result);
                }
            }

            return result;
        }

            private void DFSVisit(
                string current,
                HashSet<string> visited,
                List<string>result)
           {
            visited.Add(current);
            result.Add(current);

            foreach (string neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    DFSVisit(neighbor, visited, result);
                }
            }

           }

        //Metodo para detectar ciclos en el grafo
        public bool HasCycle()
        {
            HashSet<string> visited = new HashSet<string>();

            HashSet<string> recursionStack = new HashSet<string>();

            foreach (string vertex in vertices.Keys)
            {
                if (DetectCycle(vertex, visited, recursionStack))
                {
                    return true;
                }
            }

            return false;
        }
        private bool DetectCycle(
    string current,
    HashSet<string> visited,
    HashSet<string> recursionStack)
        {
            if (recursionStack.Contains(current))
            {
                return true;
            }

            if (visited.Contains(current))
            {
                return false;
            }

            visited.Add(current);

            recursionStack.Add(current);

            foreach (string neighbor in adjacencyList[current])
            {
                if (DetectCycle(neighbor, visited, recursionStack))
                {
                    return true;
                }
            }

            recursionStack.Remove(current);

            return false;
        }

        //Metodo para consulta de usuarios
        public List<string> GetUsersWithoutFollowers()
        {
            List<string> result = new List<string>();

            foreach (string vertex in vertices.Keys)
            {
                int inDegree = 0;

                foreach (var neighbors in adjacencyList.Values)
                {
                    if (neighbors.Contains(vertex))
                    {
                        inDegree++;
                    }
                }

                if (inDegree == 0)
                {
                    result.Add(vertex);
                }
            }

            return result;
        }

        // Metodos para usuarios influyentes
        public List<string> GetMostInfluentialUsers()
        {
            List<string> result = new List<string>();

            int maxInDegree = -1;

            foreach (string vertex in vertices.Keys)
            {
                int inDegree = 0;

                foreach (var neighbors in adjacencyList.Values)
                {
                    if (neighbors.Contains(vertex))
                    {
                        inDegree++;
                    }
                }

                if (inDegree > maxInDegree)
                {
                    maxInDegree = inDegree;

                    result.Clear();

                    result.Add(vertex);
                }
                else if (inDegree == maxInDegree)
                {
                    result.Add(vertex);
                }
            }

            return result;
        }

        // Metodo para usuarios mas activos
        public List<string> GetMostActiveUsers()
        {
            List<string> result = new List<string>();

            int maxOutDegree = -1;

            foreach (string vertex in vertices.Keys)
            {
                int outDegree = adjacencyList[vertex].Count;

                if (outDegree > maxOutDegree)
                {
                    maxOutDegree = outDegree;

                    result.Clear();

                    result.Add(vertex);
                }
                else if (outDegree == maxOutDegree)
                {
                    result.Add(vertex);
                }
            }

            return result;
        }

        //Metodo para alcanzabilidad de usuarios
        public bool CanReach(string start, string target)
        {
            List<string> recorrido = BFS(start);

            return recorrido.Contains(target);
        }

        //CRUD de Usuarios, eliminar usuarios
        public bool RemoveVertex(string id)
        {
            if (!vertices.ContainsKey(id))
            {
                return false;
            }

            vertices.Remove(id);

            adjacencyList.Remove(id);

            foreach (var neighbors in adjacencyList.Values)
            {
                neighbors.Remove(id);
            }

            return true;
        }

        //CRUD de Usuarios, actualizar usuarios
        public bool UpdateVertex(
          string id,
          string newName,
          string newRole)
        {
            if (!vertices.ContainsKey(id))
            {
                return false;
            }

            vertices[id].Nombre = newName;

            vertices[id].Rol = newRole;

            return true;
        }

        //Eliminar relaciones
        public bool RemoveEdge(string from, string to)
        {
            if (!adjacencyList.ContainsKey(from))
            {
                return false;
            }

            if (!adjacencyList[from].Contains(to))
            {
                return false;
            }

            adjacencyList[from].Remove(to);

            return true;
        }
    }
}


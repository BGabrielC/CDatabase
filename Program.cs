using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace TiendaConsolaApp
{
    class Program
    {
        //static string connectionString = "Server=localhost\\SQLEXPRESS;Database=TiendaDB;Trusted_Connection=True;"; // SQL Express

        static string connectionString = "Server=localhost,1433;Database=TiendaDB;User Id=sa;Password=Password123.;Encrypt=False;TrustServerCertificate=True;";


        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Clientes");
                Console.WriteLine("2. Categorías");
                Console.WriteLine("3. Productos");
                Console.WriteLine("4. Pedidos");
                Console.WriteLine("5. Detalles de Pedido");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");
                switch (Console.ReadLine())
                {
                    case "1": MenuClientes(); break;
                    case "2": MenuCategorias(); break;
                    case "3": MenuProductos(); break;
                    case "4": MenuPedidos(); break;
                    case "5": MenuDetalles(); break;
                    case "6": return;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        private class Cliente
        {
            public int ClienteID;
            public string Nombre;
            public string Email;
            public string Telefono;
        }

        private class Categoria
        {
            public int CategoriaID;
            public string Nombre;
            public string Descripcion;
        }

        private class Producto
        {
            public int ProductoID;
            public string Nombre;
            public decimal Precio;
            public int Stock;
            public int CategoriaID;
        }

        private class Pedido
        {
            public int PedidoID;
            public int ClienteID;
            public DateTime FechaPedido;
        }

        private class DetallePedido
        {
            public int DetalleID;
            public int PedidoID;
            public int ProductoID;
            public int Cantidad;
            public decimal PrecioUnitario;
        }

        static void Eliminar(string tabla, string idCol)
        {
            Console.Write("ID a eliminar: ");
            int id = int.Parse(Console.ReadLine());
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand($"DELETE FROM {tabla} WHERE {idCol} = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        static List<Cliente> Listar(string tabla)
        {
            Cliente cliente = new Cliente();
            List<Cliente> clientes = new List<Cliente>();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand($"SELECT * FROM {tabla}", connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cliente.ClienteID = reader.GetInt32(0);
                cliente.Nombre = reader.GetString(1);
                cliente.Email = reader.GetString(2);
                cliente.Telefono = reader.GetString(3);
                clientes.Add(cliente);
            }
            return clientes;
        }

        static void MenuClientes()
        {
            Console.WriteLine("\n1. Listar\n2. Insertar\n3. Actualizar\n4. Eliminar");
            switch (Console.ReadLine())
            {
                case "1": Listar("Clientes"); break;
                case "2": InsertarCliente(); break;
                case "3": ActualizarCliente(); break;
                case "4": Eliminar("Clientes", "ClienteID"); break;
            }
        }

        static void InsertarCliente()
        {
            Cliente c = new();
            Console.Write("Nombre: ");
            c.Nombre = Console.ReadLine();
            Console.Write("Email: ");
            c.Email = Console.ReadLine();
            Console.Write("Teléfono: ");
            c.Telefono = Console.ReadLine();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@n,@e,@t)", connection);
            command.Parameters.AddWithValue("@n", c.Nombre);
            command.Parameters.AddWithValue("@e", c.Email);
            command.Parameters.AddWithValue("@t", c.Telefono);
            command.ExecuteNonQuery();
        }

        static void ActualizarCliente()
        {
            Cliente c = new();
            Console.Write("ID: ");
            c.ClienteID = int.Parse(Console.ReadLine());
            Console.Write("Nuevo nombre: ");
            c.Nombre = Console.ReadLine();
            Console.Write("Nuevo email: ");
            c.Email = Console.ReadLine();
            Console.Write("Nuevo teléfono: ");
            c.Telefono = Console.ReadLine();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("UPDATE Clientes SET Nombre=@n, Email=@e, Telefono=@t WHERE ClienteID=@id", connection);
            sqlCommand.Parameters.AddWithValue("@id", c.ClienteID);
            sqlCommand.Parameters.AddWithValue("@n", c.Nombre);
            sqlCommand.Parameters.AddWithValue("@e", c.Email);
            sqlCommand.Parameters.AddWithValue("@t", c.Telefono);
            sqlCommand.ExecuteNonQuery();
        }

        static void MenuCategorias()
        {
            Console.WriteLine("\n1. Listar\n2. Insertar\n3. Actualizar\n4. Eliminar");
            switch (Console.ReadLine())
            {
                case "1": Listar("Categorias"); break;
                case "2": InsertarCategoria(); break;
                case "3": ActualizarCategoria(); break;
                case "4": Eliminar("Categorias", "CategoriaID"); break;
            }
        }

        static void InsertarCategoria()
        {
            Console.Write("Nombre: ");
            string n = Console.ReadLine();
            Console.Write("Descripción: ");
            string d = Console.ReadLine();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Categorias (Nombre, Descripcion) VALUES (@n,@d)", connection);
            command.Parameters.AddWithValue("@n", n);
            command.Parameters.AddWithValue("@d", d);
            command.ExecuteNonQuery();
        }

        static void ActualizarCategoria()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo nombre: ");
            string n = Console.ReadLine();
            Console.Write("Nueva descripción: ");
            string d = Console.ReadLine();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Categorias SET Nombre=@n, Descripcion=@d WHERE CategoriaID=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@n", n);
            command.Parameters.AddWithValue("@d", d);
            command.ExecuteNonQuery();
        }

        static void MenuProductos()
        {
            Console.WriteLine("\n1. Listar\n2. Insertar\n3. Actualizar\n4. Eliminar");
            switch (Console.ReadLine())
            {
                case "1": Listar("Productos"); break;
                case "2": InsertarProducto(); break;
                case "3": ActualizarProducto(); break;
                case "4": Eliminar("Productos", "ProductoID"); break;
            }
        }

        static void InsertarProducto()
        {
            Console.Write("Nombre: ");
            string n = Console.ReadLine();
            Console.Write("Precio: ");
            decimal p = decimal.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int s = int.Parse(Console.ReadLine());
            Console.Write("Categoría ID: ");
            int cat = int.Parse(Console.ReadLine());

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                "INSERT INTO Productos (Nombre, Precio, Stock, CategoriaID) VALUES (@n,@p,@s,@cat)", connection);
            command.Parameters.AddWithValue("@n", n);
            command.Parameters.AddWithValue("@p", p);
            command.Parameters.AddWithValue("@s", s);
            command.Parameters.AddWithValue("@cat", cat);
            command.ExecuteNonQuery();
        }

        static void ActualizarProducto()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo nombre: ");
            string n = Console.ReadLine();
            Console.Write("Nuevo precio: ");
            decimal p = decimal.Parse(Console.ReadLine());
            Console.Write("Nuevo stock: ");
            int s = int.Parse(Console.ReadLine());
            Console.Write("Nueva categoría: ");
            int cat = int.Parse(Console.ReadLine());

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                "UPDATE Productos SET Nombre=@n, Precio=@p, Stock=@s, CategoriaID=@cat WHERE ProductoID=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@n", n);
            command.Parameters.AddWithValue("@p", p);
            command.Parameters.AddWithValue("@s", s);
            command.Parameters.AddWithValue("@cat", cat);
            command.ExecuteNonQuery();
        }

        static void MenuPedidos()
        {
            Console.WriteLine("\n1. Listar\n2. Insertar\n3. Eliminar");
            switch (Console.ReadLine())
            {
                case "1": Listar("Pedidos"); break;
                case "2": InsertarPedido(); break;
                case "3": Eliminar("Pedidos", "PedidoID"); break;
            }
        }

        static void InsertarPedido()
        {
            Console.Write("Cliente ID: ");
            int cId = int.Parse(Console.ReadLine());
            Console.Write("Fecha (yyyy-mm-dd): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Pedidos (ClienteID, FechaPedido) VALUES (@cid,@f)", connection);
            command.Parameters.AddWithValue("@cid", cId);
            command.Parameters.AddWithValue("@f", fecha);
            command.ExecuteNonQuery();
        }

        static void MenuDetalles()
        {
            Console.WriteLine("\n1. Listar\n2. Insertar\n3. Eliminar");
            switch (Console.ReadLine())
            {
                case "1": Listar("DetallePedidos"); break;
                case "2": InsertarDetalle(); break;
                case "3": Eliminar("DetallePedidos", "DetalleID"); break;
            }
        }

        static void InsertarDetalle()
        {
            Console.Write("Pedido ID: ");
            int pid = int.Parse(Console.ReadLine());
            Console.Write("Producto ID: ");
            int pr = int.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            int cant = int.Parse(Console.ReadLine());
            Console.Write("Precio unitario: ");
            decimal pu = decimal.Parse(Console.ReadLine());

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                "INSERT INTO DetallePedidos (PedidoID, ProductoID, Cantidad, PrecioUnitario) VALUES (@p,@pr,@c,@pu)",
                connection);
            command.Parameters.AddWithValue("@p", pid);
            command.Parameters.AddWithValue("@pr", pr);
            command.Parameters.AddWithValue("@c", cant);
            command.Parameters.AddWithValue("@pu", pu);
            command.ExecuteNonQuery();
        }
    }
}
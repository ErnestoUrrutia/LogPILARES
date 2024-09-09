
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System;
namespace LogPILARES
{
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Net;
    using System.Text;

    public partial class Form1 : Form
    {
        private bool isFullScreen = false;
        public Form1()
        {
            InitializeComponent();
            SetFullScreen();
           
            Task.Run(() => conectar());
        }


        private void SetFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //   Process.Start("cmd.exe", "/C taskkill /f /im explorer.exe");
        
            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;
            textBox1.Top = (this.ClientSize.Height - textBox1.Height) / 2 +100;

            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            button1.Top = (this.ClientSize.Height - button1.Height) / 2 + 150;

            button2.Left = (this.ClientSize.Width -button2.Width)-10;
            button2.Top = (this.ClientSize.Height -button2.Height)-10;

            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Convertir el texto a mayúsculas
            textBox1.Text = textBox1.Text.ToUpper();

            // Colocar el cursor al final del texto
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.Text = textBox1.Text.ToUpper().Replace(" ", "");

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica si la tecla presionada es Enter
            {
                button1.PerformClick(); // Simula un clic en el botón
                e.SuppressKeyPress = true; // Evita el sonido "ding" de Windows
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Cancela el cierre del formulario
                e.Cancel = true;

                // Muestra un mensaje o realiza otra acción
                MessageBox.Show("El cierre del formulario está deshabilitado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        


        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=ernesto;database=Usuarios;port=3306;password=admin";

            // Crear una conexión
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    conn.Open();

                    Console.WriteLine("Conexión exitosa.");

                    // Obtener el folio desde el TextBox
                    string folio = textBox1.Text;

                    // Comando SQL con parámetro
                    string query = "SELECT nombre FROM Registros WHERE Folio = @Folio";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Folio", folio);

                    // Ejecutar el comando y leer el resultado
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Guardar el nombre en una variable
                        string nombre = reader["nombre"].ToString();

                        // Mostrar el nombre o usarlo en tu lógica
                        MessageBox.Show("Nombre encontrado: " + nombre);
                        esconderBloqueo();
                    }
                    else
                    {
                        MessageBox.Show("Folio no encontrado.");
                        
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

             // Esconde el formulario actual
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void esconderBloqueo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(esconderBloqueo));
            }
            else
            {
                this.Visible = false;
            }
        }
        public void mostrarBloqueo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(mostrarBloqueo));
            }
            else
            {
                this.Visible = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Crear una nueva instancia cada vez
            form2.Show(); // Mostrar el formulario);
            form2.TopMost = true;
        }
        public void conectar()
        {
            

           string serverIP = "127.0.0.1";  // Dirección IP del servidor
            int port = 12345;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
            Socket clientSocket = null;

            int retryCount = 5;  // Número de intentos de reconexión iniciales
            int delay = 2000;    // Tiempo de espera entre intentos en milisegundos

            while (true)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Intentar conectar al servidor con reintentos
                    while (true)
                    {
                        try
                        {
                            clientSocket.Connect(endPoint);
                            Console.WriteLine("Conectado al servidor.");
                            break;  // Si la conexión tiene éxito, salir del bucle de reintento
                        }
                        catch (SocketException ex)
                        {




                            Console.WriteLine("Reintentando en 2 segundos...");
                            Thread.Sleep(delay);
                        }
                    }

                    // Enviar mensaje al servidor
                    string clientMessage = "Hola, servidor!";
                    byte[] clientMessageBytes = Encoding.ASCII.GetBytes(clientMessage);
                    clientSocket.Send(clientMessageBytes);
                    Console.WriteLine("Mensaje enviado al servidor: " + clientMessage);

                    // Recibir múltiples mensajes del servidor
                    while (true)
                    {
                        byte[] buffer = new byte[1024];
                        int receivedBytes = clientSocket.Receive(buffer);
                        string serverMessage = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
                        if (serverMessage == "Ocultar")
                        {
                            esconderBloqueo();
                        }
                        if (serverMessage == "Mostrar")
                        {
                            mostrarBloqueo();
                        }
                        Console.WriteLine("Mensaje recibido del servidor: " + serverMessage);
                    }

                    // Simular una desconexión cerrando el socket (en un escenario real, esto ocurriría cuando el servidor cierra la conexión)
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    Console.WriteLine("Conexión cerrada por el servidor. Reintentando conexión...");

                    // Dormir antes de reintentar la conexión
                    Thread.Sleep(delay);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Error de socket durante la comunicación: " + ex.Message);
                    Console.WriteLine("Reintentando conexión...");
                    Thread.Sleep(delay);  // Esperar antes de reintentar la conexión
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inesperado: " + ex.Message);
                }
                finally
                {
                    if (clientSocket != null && clientSocket.Connected)
                    {
                        clientSocket.Close();
                    }
                }
            }
        }
    }

}

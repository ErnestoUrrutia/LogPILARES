
using MySql.Data.MySqlClient;
using System;
namespace LogPILARES
{
    using System.Diagnostics;

    public partial class Form1 : Form
    {
        private bool isFullScreen = false;
        public Form1()
        {
            InitializeComponent();
            SetFullScreen();
            this.Resize += new EventHandler(Form1_Resize);
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
        private void Form1_Resize(object sender, EventArgs e)
        {
            // Centrar el Label horizontalmente

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

        private void label1_Click(object sender, EventArgs e)
        {

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
                        this.Hide();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Crear una nueva instancia cada vez
            form2.Show(); // Mostrar el formulario);
            form2.TopMost = true;
        }
    }

}

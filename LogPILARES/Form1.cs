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
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            // Centrar el Label verticalmente
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;

            textBox1.Left = (this.ClientSize.Width - label1.Width) / 2;
            textBox1.Top = (this.ClientSize.Height - label1.Height) / 2 + 50;

            button1.Left = (this.ClientSize.Width - label1.Width) / 2;
            button1.Top = (this.ClientSize.Height - label1.Height) / 2 + 120;
            pictureBox1.Dock = DockStyle.Fill;

            // Mantiene la relación de aspecto de la imagen
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
            this.Hide(); // Esconde el formulario actual
            //Process.Start("cmd.exe", "/C start explorer.exe");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}

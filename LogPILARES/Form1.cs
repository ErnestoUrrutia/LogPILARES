namespace LogPILARES
{
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
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            // Centrar el Label verticalmente
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            // Centrar el Label horizontalmente
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            // Centrar el Label verticalmente
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }

}

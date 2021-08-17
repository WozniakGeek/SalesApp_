using COMPLETE_FLAT_UI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPLETE_FLAT_UI
{
    public partial class FormMantCliente : Form
    {
        public FormMantCliente()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMantCliente_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var Nombre = txtnombre.Text;
                var Apellido = txtapellido.Text;
                var Telefono = txttelefono.Text;
                var Idetificacion = txtdireccion.Text;
                using (var db = new PuntoDeVentaEntities())
                {
                    Cliente cliente = new Cliente()
                    {
                        Name = Nombre.ToString() +" "+ Apellido.ToString(),
                        Phone = int.Parse(Telefono.ToString()),
                        Identification = int.Parse(Idetificacion.ToString())
                    };
                    db.Cliente.Add(cliente);
                    db.SaveChanges();

                }
                this.Close();

            }
            catch (Exception ex)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtdocument_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

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
        Cliente cliente = null;
        public int? id;
        public bool delete;
        public FormMantCliente(int? id = null, bool delete= false)
        {
            InitializeComponent();
            this.id = id;
            this.delete = delete;
            if (id != null)
            {
                CargarData();
            }
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

        private void CargarData()
        {
            using (var db = new PuntoDeVentaEntities())
            {
                cliente = db.Cliente.Find(id);
                txtnombre.Text = cliente.Name;
                txtapellido.Text = cliente.LastName;
                txtdireccion.Text = cliente.Identification.ToString();
                txttelefono.Text = cliente.Phone.ToString();
            }
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
                var Idetificacion = Convert.ToInt32(txtdireccion.Text);
                using (var db = new PuntoDeVentaEntities())
                {
                    if (id != null)
                    {
                        var Update = db.Cliente.Where(x => x.Id == id).FirstOrDefault();                        
                        Update.Name= Nombre.ToString();
                        Update.LastName = Nombre.ToString();
                        Update.Phone = int.Parse(Telefono.ToString());
                        Update.Identification = int.Parse(Idetificacion.ToString());
                        db.Entry(Update).State = System.Data.Entity.EntityState.Modified;

                    }
                    else
                    {
                        cliente = new Cliente();

                        cliente.Name = Nombre.ToString();
                        cliente.LastName = Apellido.ToString();
                        cliente.Phone = int.Parse(Telefono.ToString());
                        cliente.Identification = int.Parse(Idetificacion.ToString());
                        cliente.Active = true;
                        db.Cliente.Add(cliente);
                    }                      

                    
                    db.SaveChanges();

                }
                this.Close();


            }
            catch (Exception ex)
            {

            }
        }

    }
}

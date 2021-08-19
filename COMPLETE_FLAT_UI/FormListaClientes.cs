using COMPLETE_FLAT_UI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPLETE_FLAT_UI
{
    public partial class FormListaClientes : Form
    {
        public FormListaClientes()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormListaClientes_Load(object sender, EventArgs e)
        {
            InsertarFilas();
        }
        private int? getIdCliente()
        {
            return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
        }


        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = getIdCliente();
                if (id != null)
                {
                    FormMantCliente frm = new FormMantCliente(id);
                    frm.ShowDialog();
                }
           


            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormMantCliente frm = new FormMantCliente();
            frm.ShowDialog();
        }

        private void InsertarFilas()
        {
            using (var db = new PuntoDeVentaEntities())
            {
                try
                {
                    var counter = 0;
                    var query = db.Cliente.Where(x => x.Active == true).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            dataGridView1.Rows.Insert(counter, item.Id.ToString(), item.Name, item.LastName, item.Identification.ToString(), item.Phone.ToString());
                            counter++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            //dataGridView1.Rows.Insert(0, "1", "Rafael", "Fernandez", "AV. Melgar", "56465");

        }
        private void Refresh()
        {
            using (var db = new PuntoDeVentaEntities())
            {
                var lst = from d in db.Cliente
                          select d;
                dataGridView1.DataSource = lst.ToList();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormMembresia frm = Owner as FormMembresia;
            //FormMembresia frm = new FormMembresia();

            frm.txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.txtnombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm.txtapellido.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
    }
}

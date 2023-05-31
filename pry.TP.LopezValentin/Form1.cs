using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry.TP.LopezValentin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsProfesion objProfesion = new clsProfesion();
            objProfesion.LlenarLstProfesion(lstProfesion);

            clsLocalidad objLocalidad = new clsLocalidad();
            objLocalidad.LlenarLstLocalidad(lstLocalidad);

            clsEncuestas objEncuestas = new clsEncuestas();

            dgvGrilla.Columns.Add("Grilla", "Grilla");

            DataTable tablaProfesiones = objProfesion.GetAll();
            DataTable tablaLocalidades = objLocalidad.GetAll();
            DataTable tablaEncuestas = objEncuestas.GetAll();

            foreach (DataRow dr in tablaProfesiones.Rows)
            {
                dgvGrilla.Columns.Add("Profesion", dr.ItemArray[1].ToString());
            }
            foreach (DataRow dr in tablaLocalidades.Rows)
            {
                dgvGrilla.Rows.Add(dr.ItemArray[1].ToString());
            }

            dgvGrilla.AllowUserToAddRows = false;
            dgvGrilla.AllowUserToResizeColumns = false;
            dgvGrilla.AllowUserToResizeRows = false;

            foreach (DataRow dr in tablaEncuestas.Rows)
            {
                string Localidad = objLocalidad.nombreLocalidad(Convert.ToInt32(dr.ItemArray[0]));
                string Profesion = objProfesion.nombreProfesion(Convert.ToInt32(dr.ItemArray[1]));

                foreach (DataGridViewTextBoxColumn dcGrilla in dgvGrilla.Columns)
                {
                    if (Profesion == dcGrilla.HeaderText)
                    {
                        int posicionColumna = dcGrilla.Index;

                        foreach (DataGridViewRow drGrilla in dgvGrilla.Rows)
                        {
                            if (Localidad == drGrilla.Cells[0].Value.ToString())
                            {
                                int posicionFila = drGrilla.Index;
                                dgvGrilla.Rows[posicionFila].Cells[posicionColumna].Value = dr["cantidad"];
                            }

                        }
                    }
                }

                dgvGrilla.AutoResizeColumns();
                dgvGrilla.AutoResizeRows();

            }

        }

        private void cmdRegistrarLoc_Click(object sender, EventArgs e)
        {
            clsLocalidad objLocalidad = new clsLocalidad();
            
            objLocalidad.NombreLoc = txtNombreLocalidad.Text;
            objLocalidad.IDLoc = Convert.ToInt32(txtIdLocalidad.Text);
            objLocalidad.RegistrarLocalidad();

            if (objLocalidad.IDLoc == 0)
            {
                MessageBox.Show("El ID ingresado ya existe.", "ERROR");
            }
            else
            {
                txtNombreLocalidad.Text = "";
                txtIdLocalidad.Text = "";
                txtIdLocalidad.Focus();
            }
        }

        private void cmdRegistrarPro_Click(object sender, EventArgs e)
        {
            clsProfesion objProfesion = new clsProfesion();

            objProfesion.NombreTrab = txtNombreProfesion.Text;
            objProfesion.IDTrab = Convert.ToInt32(txtIdProfesion.Text);
            objProfesion.RegistrarProfesion();

            if (objProfesion.IDTrab == 0)
            {
                MessageBox.Show("El ID ingresado ya existe.", "ERROR");
            }
            else
            {
                txtNombreProfesion.Text = "";
                txtIdProfesion.Text = "";
                txtIdProfesion.Focus();
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegistrarCantidad_Click(object sender, EventArgs e)
        {
            clsEncuestas objEncuestas = new clsEncuestas();

            bool valor = objEncuestas.Actualizar((int)lstLocalidad.SelectedValue, (int)lstProfesion.SelectedValue, Convert.ToInt32(txtCantidad.Text));
            if (valor == true)
            {
                txtCantidad.Text = "";
                lstLocalidad.SelectedIndex = -1;
                lstProfesion.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Ya fue registrado, intente otro", "ERORR");
            }
        }
    }
}

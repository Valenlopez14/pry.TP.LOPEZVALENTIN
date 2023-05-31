using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace pry.TP.LopezValentin
{



    internal class clsProfesion
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        private DataSet objTabla = new DataSet();

        private int IdTrabajo;
        private string NombreTrabajo;


        public Int32 IDTrab
        {
            get { return IdTrabajo; }
            set { IdTrabajo = value; }
        }

        public string NombreTrab
        {
            get { return NombreTrabajo; }
            set { NombreTrabajo = value; }
        }


        public clsProfesion()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Profesiones";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["profesion"];
            tabla.PrimaryKey = dc;
        }

        public void RegistrarProfesion()
        {
            DataRow BuscarFila = tabla.Rows.Find(IdTrabajo);
            if (BuscarFila == null)
            {
                DataRow Fila = tabla.NewRow();
                Fila["profesion"] = IdTrabajo;
                Fila["nombre"] = NombreTrabajo;
                //Agregamos los nuevos datos a la fila
                tabla.Rows.Add(Fila);
                //Actualizar la tabla y guardar los datos ingresados
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);
                //Actualiza la tabla
                adaptador.Update(tabla);
            }
            else
            {
                IdTrabajo = 0;
                NombreTrabajo = "";
            }
        }

        public void LlenarLstProfesion(ComboBox combo)
        {
            combo.DisplayMember = "nombre";
            combo.ValueMember = "profesion";
            combo.DataSource = tabla; 
        }
        public string nombreProfesion(int profesion)
        {
            DataRow filabuscar = tabla.Rows.Find(profesion);
            if (filabuscar != null)
            {
                NombreTrabajo = filabuscar[1].ToString();
            }
            else
            {
                NombreTrabajo = "";
            }
            return NombreTrabajo;
        }

        public DataTable GetAll()
        {
            return tabla; 
        }
    }
}

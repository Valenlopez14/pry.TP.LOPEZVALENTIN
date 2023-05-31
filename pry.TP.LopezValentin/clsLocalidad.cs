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
    internal class clsLocalidad
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        private DataSet objTabla = new DataSet();


        private int IdLocalidad;
        private string NombreLocalidad;



        public Int32 IDLoc
        {
            get { return IdLocalidad; }
            set { IdLocalidad = value; }
        }

        public string NombreLoc
        {
            get { return NombreLocalidad; }
            set { NombreLocalidad = value  ; }
        }




        public clsLocalidad()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Localidades";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["localidad"];
            tabla.PrimaryKey = dc;

        }

        public void RegistrarLocalidad()
        {

            
            DataRow BuscarFila = tabla.Rows.Find(IdLocalidad);
            if (BuscarFila == null)
            {
                DataRow Fila = tabla.NewRow();
                Fila["localidad"] = IdLocalidad;
                Fila["nombre"] = NombreLocalidad;
                //Agregamos los nuevos datos a la fila
                tabla.Rows.Add(Fila);
                //Actualizar la tabla y guardar los datos ingresados
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);
                //Actualiza la tabla
                adaptador.Update(tabla);
            }
            else
            {
                IdLocalidad = 0;
                NombreLocalidad = "";
            }

        }

        public void LlenarLstLocalidad(ComboBox combo)
        {
            combo.DisplayMember = "nombre";
            combo.ValueMember = "localidad";
            combo.DataSource = tabla;
        }

        public DataTable GetAll()
        {
            return tabla;
        }

        public string nombreLocalidad(int Localidad)
        {
            DataRow filabuscar = tabla.Rows.Find(Localidad);
            if (filabuscar != null)
            {
                NombreLocalidad = filabuscar[1].ToString();
            }
            else
            {
                NombreLocalidad = "";
            }
            return NombreLocalidad;
        }





    }
}

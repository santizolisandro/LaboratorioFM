using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;

namespace CapaControladorMantenimientosCliente
{
    public class ConexionMysql
    {
        private string NombreConexion;
        private OdbcConnection nuevaConexion;
        public ConexionMysql( string nombreConexion ) {
            this.NombreConexion = nombreConexion;
        }
        public OdbcConnection Conectar() {
            nuevaConexion = new OdbcConnection( "Dsn=" + NombreConexion );

            try {
                nuevaConexion.Open();
            } catch ( OdbcException ex ) {
                MessageBox.Show(ex.Message);
            }

            return nuevaConexion;
        }
        public void Desconectar() {
           try {
                nuevaConexion.Close();
            } catch (OdbcException ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

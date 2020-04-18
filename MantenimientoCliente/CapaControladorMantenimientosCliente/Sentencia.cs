using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;

namespace CapaControladorMantenimientosCliente
{
    public class Sentencia
    {

        private ConexionMysql NombreConexion;
        private string query;
        public Sentencia( string nombreConexion ) {
            this.NombreConexion = new ConexionMysql(nombreConexion);
        }
        public string Query { get => query; set => query = value; }
        private string ConcatenarCampos( String[] arrayCampos ) {
            string campos = "";
            foreach (string str in arrayCampos) { campos += str + ","; }
            if (campos.Length > 0) { campos = campos.Remove(campos.Length - 1); }

            return campos;
        }
        public List<string> ObtenerSugerencias( string tabla, string campo ) {
            List<string> myCollection = new List<string>();
            query = "SELECT " + campo + " FROM " + tabla;

            try
            {
                OdbcCommand command = new OdbcCommand(query, NombreConexion.Conectar());
                OdbcDataReader queryResultsReader = command.ExecuteReader();

                while (queryResultsReader.Read()) {
                    myCollection.Add(queryResultsReader.GetString(0));
                }

                NombreConexion.Desconectar();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return myCollection;
        }
        public List<string> ObtenerSugerencias( string tabla, String[] arrayCampos)
        {

            List<string> myCollection = new List<string>();

            query = "SELECT " + ConcatenarCampos(arrayCampos) + " FROM " + tabla;
            MessageBox.Show(query);


            /* FIX */

            try
            {

                OdbcCommand command = new OdbcCommand(query, NombreConexion.Conectar());
                OdbcDataReader queryResultsReader = command.ExecuteReader();

                while (queryResultsReader.Read())
                {

                    for (int i = 0; i < arrayCampos.Count() - 1; i++)
                    {

                        for (int j = 0; j < arrayCampos.Count() - 1; j++)
                        {
                            myCollection[i] += queryResultsReader.GetString(j) + "-";
                        }

                        myCollection[i].Substring(myCollection[i].Length - 1);
                    }
                }

                NombreConexion.Desconectar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "aqui");
            }

            return myCollection;
        }
        public List<string> ObtenerDatos( string tabla, String[] arrayCampos, string identificador, string resultado, bool isString )
        {

            List<string> resultados = new List<string>();

            if (!isString)
                query = "SELECT " + ConcatenarCampos(arrayCampos) + " FROM " + tabla +
                        " WHERE " + identificador + " = " + resultado;
            else
                query = "SELECT " + ConcatenarCampos(arrayCampos) + " FROM " + tabla +
                        " WHERE " + identificador + " = " + "'" + resultado + "'";

            try
            {
                OdbcCommand command = new OdbcCommand(query, NombreConexion.Conectar());
                OdbcDataReader queryResultsReader = command.ExecuteReader();

                while (queryResultsReader.Read())
                {
                    for (int i = 0; i < arrayCampos.Length; i++)
                        resultados.Add(queryResultsReader.GetString(i));
                }

                NombreConexion.Desconectar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return resultados;
        }

    }
}

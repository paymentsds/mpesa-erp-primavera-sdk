using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primavera.Paymentsds.Mpesa.Services
{
    public class BaseService : IDisposable
    {
        public BaseService() { }

        /// <summary>
        /// Método para resolução das assemblies.
        /// </summary>
        /// <param name="sender">Application</param>
        /// <param name="args">Resolving Assembly Name</param>
        /// <returns>Assembly</returns>
        public static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;

            System.Reflection.AssemblyName assemblyName;

            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(Environment.GetEnvironmentVariable("PERCURSOSGP100", EnvironmentVariableTarget.Machine), assemblyName.Name + ".dll");

            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }

        public DataTable GetSQLData(string querySql)
        {

            try
            {
                DataTable dt = new DataTable();

                string connectionString = PriEngine.Platform.BaseDados.DaConnectionStringNET(PriEngine.Platform.BaseDados.DaNomeBDdaEmpresa(PriEngine.Engine.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlDataAdapter da = new SqlDataAdapter(querySql, con);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetParameter(string Name)
        {
            try
            {
                string result = "";
                string query = string.Format("select * from TDU_Parametros where CDU_Parametro = '{0}' ", Name);
                DataTable dt = GetSQLData(query);

                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["CDU_Valor"].ToString();
                }
                else
                {
                    new Exception("O parametro {0} não se encontra configurado na tabela de TDU_Parametros");
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int ExecuteQuery(string querySQL)
        {
            try
            {
                DataTable dt = new DataTable();

                string connectionString = PriEngine.Platform.BaseDados.DaConnectionStringNET(PriEngine.Platform.BaseDados.DaNomeBDdaEmpresa(PriEngine.Engine.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = querySQL;
                con.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
               
        public void WriteLog(string folder, string name, string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(folder + "\\" + string.Format("log_{0}.log", name)))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), logMessage);
            }
            catch (Exception ex)
            {

            }
        }
        
        public void Dispose()
        {

        }
    }
}

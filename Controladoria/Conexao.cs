using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtocoloWeb
{
    public class Conexao
    {
        //Declarando váriavéis para conexão
        public SqlConnection cn = new SqlConnection();

        //Classe de conexão
        public bool Conectar()
        {
            try
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;
                cn.Open();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Banco de dados fora do ar. " + e.StackTrace);
            }
        }

        public void Desconectar()
        {
            try
            {
                if (cn.State != 0)
                {
                    cn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro no banco de dados." + e.StackTrace);
            }
        }
    }
}
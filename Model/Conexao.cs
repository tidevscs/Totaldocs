using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class Conexao
    {
        private string[] CN_STRING = new string[5];
        public Oracle.ManagedDataAccess.Client.OracleConnection conexao;
        public string ErroConexao;
        public Oracle.ManagedDataAccess.Client.OracleTransaction transacao;

        public Oracle.ManagedDataAccess.Client.OracleConnection AbreConexao(Int32 Banco)
        {
            //GLPI158273
            //CN_STRING[1] = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.38)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=PCORP01)));User Id=SAUDEPRO;Password=SAUDE#PLANOPRO;Pooling=False;";
            CN_STRING[1] = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.38)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=PCORP01)));User Id=TOTALDOCS;Password=Cq#7eb4L;Pooling=False;";

            CN_STRING[2] = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.118)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=HCORP01)));User Id=TOTALDOCS;Password=Cq#7eb4L;Pooling=False;";

            CN_STRING[3] = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.40)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=TCORP)));User Id=SAUDEPRO;Password=saude#planotreina;Pooling=False;";

            try
            {
                conexao = new OracleConnection(CN_STRING[Banco]);
                conexao.Open();

                return conexao;
            }
            catch (Exception ex)
            {
                ErroConexao = ErroConexao + ex.Message;
                conexao = null;

                StringBuilder builder = new StringBuilder();
                builder.Append(0).Append(" APITotaldocs - Conexao - AbreConexao - ");
                builder.Append(1).Append(" " + ex.Message);

                return conexao;
            }
        }


        public Oracle.ManagedDataAccess.Client.OracleTransaction IniciaTransacao()
        {
            transacao = conexao.BeginTransaction();
            return transacao;
        }

        public void FechaConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
                conexao.Dispose();
                conexao = null;
            }
        }
    }
}

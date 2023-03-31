using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APITotaldocs.Model.ModelDadosEstatisticos;

namespace APITotaldocs.Model
{
    public class ClassAtualizacaoEstatisticas
    {

       public string ExecutarConsultasEstatisticasTotaldocs()
       {
            string retorno = "";

            Conexao _Conexao = new Conexao();
            Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD = new OracleConnection();
            SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);

            //string queryString = "SELECT A.DATAHORAGERACAO, A.IDPRODUCAO, A.TIPOENVIO, A.TEMPLATE, A.CODTOTALDOCSTEMPLATE FROM SAUDEPRO.PLANO_V_TOTALDOCSLISTAMSGNABERTAS A ";
            string queryString = "SELECT A.DATASOLICITACAO AS DATAHORAGERACAO, A.PRODUCTIONID AS IDPRODUCAO, A.TIPOENVIO, A.CODIGOTEMPLATE AS TEMPLATE, A.TEMPLATEID CODTOTALDOCSTEMPLATE FROM SAUDEPRO.PLANO_V_TOTALDOCSLSTMSGNABERTASEXTRA A ";

            OracleCommand command = new OracleCommand(queryString, SecaoBD);
            OracleDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string IDPRODUCAO = (string)reader["IDPRODUCAO"].ToString();
                    string CODTOTALDOCSTEMPLATE = (string)reader["CODTOTALDOCSTEMPLATE"].ToString();

                    string TIPOENVIO = (string)reader["TIPOENVIO"].ToString();

                    string Totaldocs_eventType = "";
                    if (TIPOENVIO.Equals("1")) //email
                    {
                        Totaldocs_eventType = "EMAIL_04";
                    }
                    if (TIPOENVIO.Equals("2")) //whatsapp
                    {
                        Totaldocs_eventType = "WHATSAPP_04";
                    }
                    ProcessamentoAtualizacaoEstatisticas(IDPRODUCAO, CODTOTALDOCSTEMPLATE, Totaldocs_eventType);
                }
            }
            catch (Exception ex)
            {
                retorno = "ERRO: " + ex.Message;
            }
            finally
            {
                SecaoBD.Close();
                _Conexao.FechaConexao();
            }

            return retorno;
       }




        public string ProcessamentoAtualizacaoEstatisticas(string Totaldocs_productionID, string Totaldocs_templateId, string Totaldocs_eventType)
        {
            string retorno = "OK";

            var client = new RestClient("https://api.totaldocs.com/v1/");
            string parametros = "/rest/productionServices/searchProducaoDETByProductionID" + "?productionID=" + Totaldocs_productionID +"&templateID=" + Totaldocs_templateId + "&eventType=" + Totaldocs_eventType + "";

            Method method = Method.Post;            
            var request = new RestRequest(parametros, method);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("authToken", VariaveisGlobais.Totaldocs_authToken);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var response = client.Execute(request);
            var StatusCode = response.StatusCode;

            if (StatusCode == System.Net.HttpStatusCode.OK)
            {
                string dadosRetorno = response.Content;

                dadosRetorno = "{estatisticas:" + dadosRetorno + "}";

                EstatisticasRoot dadosTotaldocs = JsonConvert.DeserializeObject<EstatisticasRoot>(dadosRetorno);

                
                foreach (var itens in dadosTotaldocs.estatisticas)
                {
                    int aberto = itens.aberto;
                    int id = itens.id;                    
                    string caminhoEML = itens.caminhoEML;
                                        
                    DateTime dataEnviado = itens.dataRecebido;
                    DateTime dataEnvio = itens.dataRecebido;
                    DateTime dataRecebido = itens.dataRecebido;
                    DateTime? dataAberto = itens.dataAberto;

                    string _dataEnviado = dataEnviado.ToString("dd/MM/yyyy HH:mm:ss");
                    string _dataEnvio = dataEnvio.ToString("dd/MM/yyyy HH:mm:ss");
                    string _dataRecebido = dataRecebido.ToString("dd/MM/yyyy HH:mm:ss");

                    string _dataAberto = string.IsNullOrEmpty(dataAberto.ToString()) ?
                                "" :
                                dataAberto.ToString();

                    if (!string.IsNullOrEmpty(_dataAberto))
                    {
                        string hhh= "X";
                    }

                    // dataAberto.ToString("dd/MM/yyyy HH:mm:ss")
                    int producaoID = itens.producao.id;
                    int templateID = itens.template.id;

                    string clienteCelular = itens.cliente.celular;
                    string clienteEmail = itens.cliente.email;


                    GravarDadosEstatisticaTabela(Totaldocs_productionID, Totaldocs_templateId, Totaldocs_eventType, id.ToString(), 
                         caminhoEML, _dataRecebido, clienteCelular, clienteEmail, aberto.ToString(), _dataEnviado, _dataEnvio, _dataAberto);

                }

            } else
            {
                retorno = response.Content;
            }

            return retorno;
        }


        private string GravarDadosEstatisticaTabela(string Totaldocs_productionID, 
            string Totaldocs_templateId, string Totaldocs_eventType, string Totaldocs_id, 
            string Totaldocs_caminhoEML, string Totaldocs_dataRecebido, 
            string Totaldocs_clienteCelular, string Totaldocs_clienteEmail, string Totaldocs_aberto,
            string Totaldocs_dataEnviado, string Totaldocs_dataEnvio, string Totaldocs_dataAberto)
        {

            string retorno = "";
            try
            {
                Conexao _Conexao = new Conexao();
                Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD;

                SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);
                OracleCommand vDadosCF = new OracleCommand("SAUDEPRO.PLANO_PKG_TOTALDOCSESTATPROCESSOS.PLANO_P_DADOSESTATISICICASTOTALDOCS", SecaoBD);

                vDadosCF.CommandType = System.Data.CommandType.StoredProcedure;

                vDadosCF.Parameters.Add("PTOTALDOCS_PRODUCTIONID", OracleDbType.Varchar2).Value = Totaldocs_productionID;
                vDadosCF.Parameters.Add("PTOTALDOCS_TEMPLATEID", OracleDbType.Varchar2).Value = Totaldocs_templateId;
                vDadosCF.Parameters.Add("PTOTALDOCS_EVENTTYPE", OracleDbType.Varchar2).Value = Totaldocs_eventType;
                vDadosCF.Parameters.Add("PTOTALDOCS_ID", OracleDbType.Varchar2).Value = Totaldocs_id;
                vDadosCF.Parameters.Add("PTOTALDOCS_CAMINHOEML", OracleDbType.Varchar2).Value = Totaldocs_caminhoEML;
                vDadosCF.Parameters.Add("PTOTALDOCS_DATARECEBIDO", OracleDbType.Varchar2).Value = Totaldocs_dataRecebido;
                vDadosCF.Parameters.Add("PTOTALDOCS_CLIENTECELULAR", OracleDbType.Varchar2).Value = Totaldocs_clienteCelular;
                vDadosCF.Parameters.Add("PTOTALDOCS_CLIENTEEMAIL", OracleDbType.Varchar2).Value = Totaldocs_clienteEmail;
                vDadosCF.Parameters.Add("PTOTALDOCS_ABERTO", OracleDbType.Varchar2).Value = Totaldocs_aberto;
                vDadosCF.Parameters.Add("PTOTALDOCS_DATAENVIADO", OracleDbType.Varchar2).Value = Totaldocs_dataEnviado;
                vDadosCF.Parameters.Add("PTOTALDOCS_DATAENVIO", OracleDbType.Varchar2).Value = Totaldocs_dataEnvio;
                vDadosCF.Parameters.Add("PTOTALDOCS_DATAABERTO", OracleDbType.Varchar2).Value = Totaldocs_dataAberto;

                OracleParameter p1 = new OracleParameter("PERRO", OracleDbType.Varchar2, 500);
                p1.Direction = System.Data.ParameterDirection.Output;
                vDadosCF.Parameters.Add(p1);

                vDadosCF.ExecuteNonQuery();

                retorno = vDadosCF.Parameters["PERRO"].Value.ToString();

                SecaoBD.Close();
                _Conexao.FechaConexao();

            }
            catch (Exception ex)
            {

                StringBuilder builder = new StringBuilder();
                builder.Append(0).Append(" APITotaldocs - ClassAtualizacaoEstatisticas - GravarDadosEstatisticaTabela - ");
                builder.Append(1).Append(" " + ex.Message);

                ClassTratamentoErros _ClassTratamentoErros = new ClassTratamentoErros();
                _ClassTratamentoErros.DescricaoErro = builder.ToString();
            }
            return retorno;
        }


    }
}

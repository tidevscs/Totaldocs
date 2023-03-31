using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassTratamentoErros
    {
        private string _DescricaoErro;

        public string DescricaoErro
        {
            get
            {
                return _DescricaoErro;
            }
            set
            {
                _DescricaoErro = value;
                RegistrarErros();
            }
        }

        private void RegistrarErros()
        {
            try
            {
                Conexao _Conexao = new Conexao();
                Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD;

                SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);
                OracleCommand vDadosErros = new OracleCommand("SAUDEPRO.PLANO_PKG_TOTALDOCSMIDIAS.PLANO_P_REG_ERROS_PROCESSOS", SecaoBD);

                vDadosErros.CommandType = System.Data.CommandType.StoredProcedure;
                vDadosErros.Parameters.Add("EVENTO_DESCRICAO", OracleDbType.Varchar2).Value = DescricaoErro;

                vDadosErros.ExecuteNonQuery();

                SecaoBD.Close();
                _Conexao.FechaConexao();

            }
            catch (Exception)
            {
                //StringBuilder builder = new StringBuilder();
                //builder.Append(0).Append(" APIMensagensTotaldocs - ClassTratamentoErros - RegistrarErros - ");
                //builder.Append(1).Append(" " + ex.Message);

                //ClassTratamentoErros _ClassTratamentoErros = new ClassTratamentoErros();
                //_ClassTratamentoErros.DescricaoErro = builder.ToString();

            }
        }

        public string DescricaoErros(string Processo, string TipoPastaProcesso, string TipoMensagemEnvio, string TipoTemplateTotalDocs, string Erro)
        {
            string _retorno = "";
            string _processo = "";
            string _tipomsgEnvio = "";
            string _tipotemplatetotaldocs = "";


            // string Processo: 1 = E-MAILS, 2 = WHATSAPP
            if (Processo.Equals("1"))
            {
                _processo = "E-MAILS";
            }

            if (Processo.Equals("2"))
            {
                _processo = "WHATSAPP";
            }


            // string TipoMensagemEnvio: 1 = Somente Texto, 2 = Texto + Documento (PDF),3 =  Texto + Imagem (JPG)
            if (TipoMensagemEnvio.Equals("1"))
            {
                _tipomsgEnvio = "Somente Texto";
            }

            if (TipoMensagemEnvio.Equals("2"))
            {
                _tipomsgEnvio = "Texto + Documento (PDF),3 =  Texto + Imagem (JPG)";
            }

            if (TipoMensagemEnvio.Equals("3"))
            {
                _tipomsgEnvio = "Texto + Imagem (JPG)";
            }


            // string TipoTemplateTotalDocs: 1 = Plano PF, 2 = Plano PME, 3 = Plano PME (Empresário Individual) 
            if (TipoTemplateTotalDocs.Equals("1"))
            {
                _tipotemplatetotaldocs = "Plano PF";
            }
            if (TipoTemplateTotalDocs.Equals("2"))
            {
                _tipotemplatetotaldocs = "Plano PME";
            }
            if (TipoTemplateTotalDocs.Equals("3"))
            {
                _tipotemplatetotaldocs = "Plano PME (Empresário Individual)";
            }

            _retorno = "Processo: " + _processo + " - Tipo de Envio: " + _tipomsgEnvio + " - Tipo Notificação: " + _tipotemplatetotaldocs + " - " + Erro;

            return _retorno;
        }
    }

}

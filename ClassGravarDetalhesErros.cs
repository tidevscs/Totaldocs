using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassGravarDetalhesErros
    {
        public string GravarDetalheErro(string DescricaoDetalheErro)
        {
            string _retorno = "";

            try
            {
                Conexao _Conexao = new Conexao();
                Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD;

                SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);
                OracleCommand vDadosErros = new OracleCommand("SAUDEPRO.PLANO_PKG_TOTALDOCSMIDIAS.PLANO_P_REGDETERROS_PROCESSOS", SecaoBD);

                vDadosErros.CommandType = System.Data.CommandType.StoredProcedure;
                vDadosErros.Parameters.Add("PERROESCRICAO", OracleDbType.Varchar2).Value = DescricaoDetalheErro;

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
            return _retorno;
        }


        public string ConsultaControles(string ChaveReferencia)
        {
            string _retorno = "";

            try
            {
                Conexao _Conexao = new Conexao();
                Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD;

                SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);

                OracleCommand vDadosCF = new OracleCommand("SAUDEPRO.PLANO_PKG_TOTALDOCSMIDIAS.PLANO_P_CTRLS_PROCESSOS", SecaoBD);

                vDadosCF.CommandType = System.Data.CommandType.StoredProcedure;
                vDadosCF.Parameters.Add("PREFERENCIACTRL", OracleDbType.Varchar2).Value = ChaveReferencia;

                OracleParameter p1 = new OracleParameter("PLIGADESLIGA", OracleDbType.Varchar2, 50);
                p1.Direction = System.Data.ParameterDirection.Output;

                vDadosCF.Parameters.Add(p1);
                vDadosCF.ExecuteNonQuery();

                _retorno = vDadosCF.Parameters["PLIGADESLIGA"].Value.ToString();

                SecaoBD.Close();
                _Conexao.FechaConexao();

            }
            catch (Exception)
            {
                //StringBuilder builder = new StringBuilder();
                //builder.Append(0).Append(" APITotaldocs - ClassSolicitacaoCarteirinhaFisica - RegistroSolicitacaoCarteirinhaFisica - ");
                //builder.Append(1).Append(" " + ex.Message);

                //ClassTratamentoErros _ClassTratamentoErros = new ClassTratamentoErros();
                //_ClassTratamentoErros.DescricaoErro = builder.ToString();

            }

            return _retorno;
        }

    }

}

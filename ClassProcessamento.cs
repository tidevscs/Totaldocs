using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassProcessamento
    {
        public string ProcessarInformacoes(string DadosRequest)
        {
            string _retorno = "";

            //GRAVAR DADOS EM ARQUIVOS FISICOS
            ClassGravarDetalhesErros _ClassGravarDetalhesErros = new ClassGravarDetalhesErros();
            string ligarCtrl = _ClassGravarDetalhesErros.ConsultaControles("TOTALDOCS_ARMAZENARARQUIVOS_TOTALDOCSESTATISTICAS");

            if (ligarCtrl.Equals("S"))
            {
                try
                {
                    DateTime now = DateTime.Now;
                    //'DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK")'
                    string _dataHoraGeracaoArquivo = String.Format("{0:yyyyMMdd_HHmmssfffffff}", now);
                    string _arquivo = "TotaldocsEstatisticas_" + _dataHoraGeracaoArquivo + ".txt";

                    ClassFuncoesGenericas _ClassFuncoesGenericas = new ClassFuncoesGenericas();
                    string _diretorio = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ESTATISTICAS_TMP);

                    ClassTratarArquivos _ClassTratarArquivos = new ClassTratarArquivos();
                    _retorno = _ClassTratarArquivos.ArquivoTexto(_diretorio + "\\", _arquivo, DadosRequest);
                }
                catch (Exception)
                { }
            }

            try
            {
                //byte[] byteArray = Encoding.UTF8.GetBytes(DadosRequest);
                //MemoryStream bodystream = new MemoryStream(byteArray);
                //Root _root = JsonConvert.DeserializeObject<Root>(bodystream.ToString());

                Root _root = JsonConvert.DeserializeObject<Root>(DadosRequest);

                ClassAcompanhamamentos _ClassAcompanhamamentos = new ClassAcompanhamamentos();
                _retorno = _ClassAcompanhamamentos.ProcessaInformacoesEstatisticas(_root.envios);
            }
            catch (Exception ex)
            {
                _retorno = ex.Message.ToString();
            }

            return _retorno;
        }
    }
}

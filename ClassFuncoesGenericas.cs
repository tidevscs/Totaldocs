using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassFuncoesGenericas
    {
        public string DiretoriosArquivos(string ChaveRelatorio)
        {
            Conexao _Conexao = new Conexao();
            Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD = new OracleConnection();
            SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);

            string queryString = "SELECT CAMINHO FROM SAUDEPRO.PLANO_V_DIRETORIOSDESENV WHERE REFERENCIA = '" + ChaveRelatorio + "'";     // TESTES
            string sCaminho = "";

            OracleCommand command = new OracleCommand(queryString, SecaoBD);
            OracleDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        sCaminho = reader.GetValue(0).ToString();
                    }
                }
            }
            catch (Exception)
            {
                sCaminho = "";
            }
            finally
            {
                SecaoBD.Close();
                _Conexao.FechaConexao();
            }

            return sCaminho;
        }

        public string OracleDataHoraCorrente()
        {
            Conexao _Conexao = new Conexao();
            Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD = new OracleConnection();

            SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);

            string queryString = "SELECT TO_CHAR(SYSDATE, 'YYYYMMDDD_HH24MISS') FROM DUAL";
            string sData = "";

            OracleCommand command = new OracleCommand(queryString, SecaoBD);
            OracleDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        sData = reader.GetValue(0).ToString();
                    }
                }
            }
            catch (Exception)
            {
                sData = "";

            }
            finally
            {
                SecaoBD.Close();
                _Conexao.FechaConexao();
            }

            return sData;
        }

        public string VerificaExistenciaArquivo(string Diretorio, string Arquivo)
        {
            string retorno = "0";
            Boolean VerificaExistenciaArquivo = true;
            string ArquivoOld = "";

            while (VerificaExistenciaArquivo == true)
            {
                FileInfo file = new FileInfo(Diretorio + "\\" + Arquivo);
                if (file.Exists)
                {
                    int indiceArquivo = VerificaQtdeArquivosMesmoNome(Diretorio, Arquivo);
                    ArquivoOld = Arquivo;

                    string[] words = Arquivo.Split('_');
                    string arquivoAlt = "";
                    int i = 0;
                    foreach (var word in words)
                    {
                        string _novoParteNomeArquivo = words[i].ToString();

                        if (i == 0)
                        {
                            int valorIndiceArquivo = Convert.ToInt32(words[i].ToString());
                            valorIndiceArquivo++;
                            arquivoAlt += valorIndiceArquivo.ToString() + "_";
                        }
                        else
                        {
                            arquivoAlt += words[i].ToString();
                        }

                        i++;
                    }

                    Arquivo = arquivoAlt;

                    /*
                    Arquivo = NomeArquivosDuplicados(ArquivoOld, i.ToString());
                    if (Arquivo.Equals(ArquivoOld))
                    {
                        VerificaExistenciaArquivo = false;
                    }
                    */
                    retorno = Arquivo;
                }
                else
                {
                    retorno = Arquivo;
                    VerificaExistenciaArquivo = false;
                }
            }
            return retorno;
        }

        private string NomeArquivosDuplicados(string NomeArquivo, string Indice)
        {
            string[] words = NomeArquivo.Split('_');
            int i = 0;
            string _novoNomeArquivo = "";

            foreach (var word in words)
            {
                string _novoParteNomeArquivo = words[i].ToString();

                if (i == 0)
                {
                    _novoNomeArquivo = Indice + "_";
                }
                else
                {
                    _novoNomeArquivo += _novoParteNomeArquivo;
                }

                i++;
            }

            return _novoNomeArquivo;
        }

        private int VerificaQtdeArquivosMesmoNome(string Diretorio, string Arquivo)
        {
            int vcontador = 0;
            DirectoryInfo diretorio = new DirectoryInfo(Diretorio);
            FileInfo[] Arquivos = diretorio.GetFiles(Arquivo);

            foreach (FileInfo fileinfo in Arquivos)
            {
                string[] words = fileinfo.Name.Split('_');
                int i = 0;

                foreach (var word in words)
                {
                    if (i == 1)
                    {
                        string _partenome = words[i].ToString();
                        string _substr = Arquivo.Substring(2, (Arquivo.Length - 2));

                        if (_partenome.Equals(_substr))
                        {
                            vcontador++;
                        }
                    }

                    i++;
                }
            }
            return vcontador;
        }

        public string ConsultaHandleTabelaBenner(string NomeTabela)
        {
            Conexao _Conexao = new Conexao();
            Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD = new OracleConnection();

            SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);

            string queryString = "SELECT A.HANDLE FROM SAUDEPRO.Z_TABELAS A WHERE A.NOME = '" + NomeTabela + "'";
            string _handleTabela = "";

            OracleCommand command = new OracleCommand(queryString, SecaoBD);
            OracleDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        _handleTabela = reader.GetValue(0).ToString();
                    }
                }
            }
            catch (Exception)
            {
                _handleTabela = "";
            }
            finally
            {
                SecaoBD.Close();
                _Conexao.FechaConexao();
            }

            return _handleTabela;
        }

    }
}

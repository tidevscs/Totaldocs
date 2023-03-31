using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassTratarArquivos
    {
        public string ArquivoTexto(string DiretorioArquivo, string NomeArquivo, string LinhaDados)
        {
            string _retorno = "ok";
            string _arquivo = DiretorioArquivo + "\\" + NomeArquivo;
            FileInfo _File;

            try
            {
                _File = new FileInfo(_arquivo);
                FileStream fs = _File.Open(FileMode.Append, FileAccess.Write, FileShare.Read);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(LinhaDados);
                sw.Close();
            }
            catch (Exception ex)
            {
                _retorno = ex.Message.ToString();
            }
            finally
            {
            }

            return _retorno;
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using APITotaldocs.Model;
using Newtonsoft.Json;
using static APITotaldocs.Model.ModelDadosEstatisticos;

namespace APITotaldocs.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost, Route("/")]
        public string Index()
        {
            return "OK";
        }


        [HttpPost, Route("Home/uploadfilesestatisticas")]
        public async Task<string> Upload()
        {
            string request = "";
            string _retorno = "";

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var dadosrequest = reader.ReadToEndAsync();
                request = await dadosrequest;
            }

            if (!String.IsNullOrEmpty(request))
            {
                ClassProcessamento _ClassProcessamento = new ClassProcessamento();
                _ClassProcessamento.ProcessarInformacoes(request);
            }

            return _retorno;
        }


        [HttpPost, Route("Home/processamentoarquivosestatisticas")]
        public string ProcessamentoArquivosEstatisticas()
        {
            ClassAcompanhamamentos _ClassAcompanhamamentos = new ClassAcompanhamamentos();
            string retorno = _ClassAcompanhamamentos.DadosAcompanhamentos();

            return retorno;
        }


        [HttpPost, Route("Home/processamentoatualizacaoestatisticas")]
        public string ProcessamentoAtualizacaoEstatisticas()
        {
            ClassAtualizacaoEstatisticas _ClassAtualizacaoEstatisticas = new ClassAtualizacaoEstatisticas();
            string retorno = _ClassAtualizacaoEstatisticas.ExecutarConsultasEstatisticasTotaldocs();

            return retorno;
        }

        [HttpPost, Route("Home/About")]
        public string About()
        {
            return "Ambiente: " + VariaveisGlobais.WSDesenvolvimento + " - Versão: " + VariaveisGlobais.WSProductVersion + " - Data: " + VariaveisGlobais.WSProductVersionData;
        }


        //[HttpPost, Route("Home/processamenteoacertos")]
        //public string Processamenteo_Acertos()
        //{

        //    ClassAcompanhamamentos _ClassAcompanhamamentos = new ClassAcompanhamamentos();
        //    string retorno = _ClassAcompanhamamentos.Processamenteo_Acertos();

        //    return retorno;
        //}

    }
}

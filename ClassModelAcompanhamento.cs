using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassModelAcompanhamento
    {
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class UnidadedeNegocio
    {
        public string nome { get; set; }
    }

    public class ProjetoComunicacao
    {
        public string nome { get; set; }
        public UnidadedeNegocio unidadedeNegocio { get; set; }
    }

    public class Template
    {
        public string nome { get; set; }
        public string tipo { get; set; }
        public ProjetoComunicacao projetoComunicacao { get; set; }
    }

    public class Producao
    {
        public string id { get; set; }
        public string dataSolicitacao { get; set; }
        public string status { get; set; }
        public string inicioProcessamento { get; set; }
        public string fimProcessamento { get; set; }
    }

    public class CamposPersonalizado
    {
        public string nome { get; set; }
        public string valor { get; set; }
        public string tamanho { get; set; }
        public string tipo { get; set; }
    }

    public class Documento
    {
        public string nome { get; set; }
        public string tamanho { get; set; }
    }

    public class Envio
    {
        public string id { get; set; }
        public string idKit { get; set; }
        public string nomeCliente { get; set; }
        public string celularCliente { get; set; }
        public string emailCliente { get; set; }
        public string dataEnviado { get; set; }
        public string dataRecebido { get; set; }
        public string bounce { get; set; }
        public string dataAberto { get; set; }
        public string dataClicado { get; set; }
        public string dataOptout { get; set; }
        public string dataSpam { get; set; }
        public string dataRecebidoOperadora { get; set; }
        public string textoSMS { get; set; }
        public string respostaSMS { get; set; }
        public string dataRespostaSMS { get; set; }
        public string status { get; set; }
        public string carimbar { get; set; }
        public string carimboEnvio { get; set; }
        public string carimboEntrega { get; set; }
        public string carimboAbertura { get; set; }
        public Template template { get; set; }
        public Producao producao { get; set; }
        public List<CamposPersonalizado> camposPersonalizados { get; set; }
        public List<Documento> documentos { get; set; }
    }

    public class Root
    {
        public List<Envio> envios { get; set; }
    }


}

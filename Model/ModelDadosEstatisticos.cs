using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ModelDadosEstatisticos
    {
    }

    // EstatisticasRoot myDeserializedClass = JsonConvert.DeserializeObject<EstatisticasRoot>(myJsonResponse);

    public class EstatisticasRoot
    {
        public List<EstatisticasEstatistica> estatisticas { get; set; }
    }

    public class EstatisticasCarimbo
    {
    }

    public class EstatisticasCliente
    {
        public bool canalEmail { get; set; }
        public bool canalImpresso { get; set; }
        public bool canalSMS { get; set; }
        public bool canalWhatsApp { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string nome { get; set; }
        public int numero { get; set; }
    }

    public class EstatisticasEstatistica
    {
        public int aberto { get; set; }
        public bool aplicaSMIME { get; set; }
        public int bounce { get; set; }
        public string caminhoEML { get; set; }
        public bool carimbar { get; set; }
        public EstatisticasCarimbo carimbo { get; set; }
        public int clicado { get; set; }
        public EstatisticasCliente cliente { get; set; }
        public DateTime dataEnviado { get; set; }
        public DateTime dataEnvio { get; set; }
        public DateTime dataRecebido { get; set; }
        public bool enviado { get; set; }
        public bool guardarEML { get; set; }
        public string hashMensagemEnviada { get; set; }
        public int id { get; set; }
        public int idFase { get; set; }
        public int idKit { get; set; }
        public bool optout { get; set; }
        public EstatisticasProducao producao { get; set; }
        public int recebido { get; set; }
        public bool spam { get; set; }
        public int status { get; set; }
        public EstatisticasTemplate template { get; set; }
        public double totalAnexo { get; set; }
        public DateTime? dataAberto { get; set; }
        public string ipAberto { get; set; }
        public string navegadorAberto { get; set; }
    }

    public class EstatisticasProducao
    {
        public bool aprovado { get; set; }
        public string arquivo { get; set; }
        public int id { get; set; }
        public int qtdProducaoDet { get; set; }
        public int qtdProducaoDetEnviado { get; set; }
        public int qtdProducaoDetRecebido { get; set; }
        public string status { get; set; }
    }

    public class EstatisticasTemplate
    {
        public string type { get; set; }
        public string caminhoDisco { get; set; }
        public bool carimbar { get; set; }
        public int id { get; set; }
        public bool importado { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
        public int alturaPagina { get; set; }
        public bool aplicaSMIME { get; set; }
        public bool guardarEML { get; set; }
        public int larguraPagina { get; set; }
        public int tamFonte { get; set; }
    }

}

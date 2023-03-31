using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class VariaveisGlobais
    {
        public const string CODIGO_SISTEMA_TECNICO = "HMSCS0021";


        //Dados de acesso a API do Totaldocs
        public const string Totaldocs_authToken = "06c581bf835cd86afb9189b85df0e9d9";

        public const string Totaldocs_URL_Email_Anexos = "https://api.totaldocs.com/v1/rest/producaoServices/uploadAnexo";
        public const string Totaldocs_URL_WhatsApp_Anexos = "https://api.totaldocs.com/v1/rest/producaoServices/uploadAnexo";
        public const string Totaldocs_URL_Download_Carimbo_Tempo = "https://api.totaldocs.com/v1/rest/relatorioServices/downloadPdfCarimbo";
        public const string Totaldocs_URL_searchProducaoDETByProductionID = "https://api.totaldocs.com/v1/rest/productionServices/searchProducaoDETByProductionID";

        //Pasta para descompactar (temporario) arquivos ZIP
        public const string PASTA_REDE_TEMPORARIA_IONICZIP = "TOTALDOCS__IONICZIP_TMP";

        public const int RestClientTimeout = 92500000;

        //BENNER - Producao e Homologacao        /*
        ////producao - 29/06/2022 - 06/05/2021 - 15/12/2022 - 23/12/2022 - 05/01/2023 - 24/02/2023

        public const string WSProductVersionData = "24/02/2023";
        public const string WSProductVersion = "1.2.1.9";
        public const string WSDesenvolvimento = "PRODUCAO";
        public const string Sistema_Benner_Usuario = "ti.totaldocs";
        public const string Sistema_Benner_Senha = "ti!@#INTEGRA";
        public const string URLCookContainerBenner = "http://10.10.10.9:8073/AppServerAut/PoolService.asmx";
        public const string NomePoolBenner = "SAUDEPRO";
        public const Int32 InstanciaConexao = 1; // - Producao - Usuario SAUDEPRO
        

        //homologacao 
        // Somente o sistema Benner possui ambiente de Homologacao
        // O sistema Totaldocs (Sirius) NAO possui ambiente de Homologacao
        /*
        public const string WSProductVersionData = "24/02/2023";
        public const string WSProductVersion = "1.2.1.9";
        public const string WSDesenvolvimento = "HOMOLOGACAO";
        public const string Sistema_Benner_Usuario = "ti.totaldocs";
        public const string Sistema_Benner_Senha = "123400sc";
        public const string URLCookContainerBenner = "http://hmsc35:80/AppServerAUT/PoolService.asmx";
        public const string NomePoolBenner = "SAUDEHOM";
        public const Int32 InstanciaConexao = 2;     // - Homologacao - Usuario SAUDEPRO        
        */

        #region "Pastas de Rede"


        //Processo de envio de E-mails
        //Benner
        public const string PASTA_REDE_BENNER_EMAIL_TIPO01 = "TOTALDOCS__BENNER_EMAIL_TIPO01";
        public const string PASTA_REDE_BENNER_EMAIL_TIPO02 = "TOTALDOCS__BENNER_EMAIL_TIPO02";
        public const string PASTA_REDE_BENNER_EMAIL_TIPO03 = "TOTALDOCS__BENNER_EMAIL_TIPO03";

        //Processo
        public const string PASTA_REDE_EMAIL_PROCESSADOS = "TOTALDOCS__EMAIL_PROCESSADOS";
        public const string PASTA_REDE_EMAIL_ARQUIVOS_ENVIOS_TIPO01 = "TOTALDOCS__EMAIL_ARQUIVOS_ENVIOS_TIPO01";
        public const string PASTA_REDE_EMAIL_ARQUIVOS_ENVIOS_TIPO02 = "TOTALDOCS__EMAIL_ARQUIVOS_ENVIOS_TIPO02";
        public const string PASTA_REDE_EMAIL_ARQUIVOS_ENVIOS_TIPO03 = "TOTALDOCS__EMAIL_ARQUIVOS_ENVIOS_TIPO03";

        //Processo de envio de WhatsApp
        //Benner
        public const string PASTA_REDE_BENNER_WHATSAPP_TEXTO_TIPO01 = "TOTALDOCS__BENNER_WHATSAPP_TEXTO_TIPO01"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_BENNER_WHATSAPP_TEXTO_TIPO02 = "TOTALDOCS__BENNER_WHATSAPP_TEXTO_TIPO02"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_BENNER_WHATSAPP_TEXTO_TIPO03 = "TOTALDOCS__BENNER_WHATSAPP_TEXTO_TIPO03"; //IMPLEMENTADO, MAS NAO UTILIZADO

        public const string PASTA_REDE_BENNER_WHATSAPP_DOCUMENTO_TIPO01 = "TOTALDOCS__BENNER_WHATSAPP_DOCUMENTO_TIPO01";
        public const string PASTA_REDE_BENNER_WHATSAPP_DOCUMENTO_TIPO02 = "TOTALDOCS__BENNER_WHATSAPP_DOCUMENTO_TIPO02";
        public const string PASTA_REDE_BENNER_WHATSAPP_DOCUMENTO_TIPO03 = "TOTALDOCS__BENNER_WHATSAPP_DOCUMENTO_TIPO03";

        public const string PASTA_REDE_BENNER_WHATSAPP_IMAGEM_TIPO01 = "TOTALDOCS__BENNER_WHATSAPP_IMAGEM_TIPO01"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_BENNER_WHATSAPP_IMAGEM_TIPO02 = "TOTALDOCS__BENNER_WHATSAPP_IMAGEM_TIPO02"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_BENNER_WHATSAPP_IMAGEM_TIPO03 = "TOTALDOCS__BENNER_WHATSAPP_IMAGEM_TIPO03"; //IMPLEMENTADO, MAS NAO UTILIZADO

        //Processo
        public const string PASTA_REDE_WHATSAPP_TEXTO_PROCESSADOS = "TOTALDOCS__WHATSAPP_TEXTO_PROCESSADOS";
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO01 = "TOTALDOCS__WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO01"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO02 = "TOTALDOCS__WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO02"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO03 = "TOTALDOCS__WHATSAPP_ARQUIVOS_TEXTO_ENVIOS_TIPO03"; //IMPLEMENTADO, MAS NAO UTILIZADO

        public const string PASTA_REDE_WHATSAPP_DOCUMENTO_PROCESSADOS = "TOTALDOCS__WHATSAPP_DOCUMENTO_PROCESSADOS";
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO01 = "TOTALDOCS__WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO01";
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO02 = "TOTALDOCS__WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO02";
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO03 = "TOTALDOCS__WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO03";

        public const string PASTA_REDE_WHATSAPP_IMAGEM_PROCESSADOS = "TOTALDOCS__WHATSAPP_IMAGEM_PROCESSADOS";
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO01 = "TOTALDOCS__WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO01"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO02 = "TOTALDOCS__WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO02"; //IMPLEMENTADO, MAS NAO UTILIZADO
        public const string PASTA_REDE_WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO03 = "TOTALDOCS__WHATSAPP_ARQUIVOS_IMAGEM_ENVIOS_TIPO03"; //IMPLEMENTADO, MAS NAO UTILIZADO

        //Copia Arquivos Financeiro
        public const string PASTA_REDE_FIN_EMAIL_TIPO01 = "TOTALDOCS__FIN_EMAIL_ARQUIVOS_ENVIOS_TIPO01";
        public const string PASTA_REDE_FIN_EMAIL_TIPO02 = "TOTALDOCS__FIN_EMAIL_ARQUIVOS_ENVIOS_TIPO02";
        public const string PASTA_REDE_FIN_EMAIL_TIPO03 = "TOTALDOCS__FIN_EMAIL_ARQUIVOS_ENVIOS_TIPO03";

        public const string PASTA_REDE_FIN_WHATSAPP_TIPO01 = "TOTALDOCS__FIN_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO01";
        public const string PASTA_REDE_FIN_WHATSAPP_TIPO02 = "TOTALDOCS__FIN_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO02";
        public const string PASTA_REDE_FIN_WHATSAPP_TIPO03 = "TOTALDOCS__FIN_WHATSAPP_ARQUIVOS_DOCUMENTO_ENVIOS_TIPO03";


        //Processo de captura de retorno de dados do sistema Totaldocs
        public const string NOME_ARQUIVOS_CARIMBOTEMPO_LOGOTIPO = "LOGO_SC_SAUDE.PNG";
        public const string PASTA_REDE_ESTATISTICAS_TMP = "TOTALDOCS__ESTATISTICAS_ARQUIVOS_TMP";
        public const string PASTA_REDE_ARQUIVOS_ESTATISTICAS = "TOTALDOCS__ARQUIVOS_ESTATISTICAS";
        public const string PASTA_REDE_ARQUIVOS_CARIMBOTEMPO_LOGOTIPO = "TOTALDOCS__CARIMBOTEMPO_LOGOTIPO";
        public const string PASTA_REDE_ARQUIVOS_CARIMBOTEMPO = "TOTALDOCS__CARIMBOTEMPO";
        public const string PASTA_REDE_ARQUIVOS_CARIMBOTEMPO_TMP = "TOTALDOCS__CARIMBOTEMPO_TMP";


        #endregion

    }

}

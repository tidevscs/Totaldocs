using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APITotaldocs.Model
{
    public class ClassAcompanhamamentos
    {
        public string DadosAcompanhamentos()
        {
            string _retorno = "OK";
            try
            {
                ClassFuncoesGenericas _ClassFuncoesGenericas = new ClassFuncoesGenericas();
                string _diretorioArqEstatisticasTmp = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ESTATISTICAS_TMP);
                string _diretorioArqEstatisticas = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ARQUIVOS_ESTATISTICAS);

                DirectoryInfo info = new DirectoryInfo(_diretorioArqEstatisticasTmp);
                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                FileInfo[] files1 = files.OrderBy(p => p.Name).ToArray();

                //VERIFICA SE TEM 'S' NECESSIDADE DE GRAVAR OS ARQUIVOS OU 'N' APAGAR O ARQUIVO APOS PROCESSAMENTO DOS DADOS
                ClassGravarDetalhesErros _ClassGravarDetalhesErros = new ClassGravarDetalhesErros();
                string ligarCtrl = _ClassGravarDetalhesErros.ConsultaControles("TOTALDOCS_ARMAZENARARQUIVOS_TOTALDOCSESTATISTICAS");

                foreach (FileInfo file in files1)
                {
                    if (file.Extension.ToString().ToUpper().Equals(".TXT"))
                    {
                        string ret = "";
                        
                        try
                        {
                            ret = ProcessaArquivo(file.ToString());
                        }
                        catch(Exception ex)
                        { }

                        if (ligarCtrl.Equals("S"))
                        {
                            file.MoveTo(_diretorioArqEstatisticas + "\\" + file.Name.ToString());
                        }

                        if (ligarCtrl.Equals("N"))
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _retorno = "ERRO: " + ex.Message.ToString();
            }

            return _retorno;
        }

        private string ProcessaArquivo(string fileName)
        {
            string _retorno = "";
            ClassFuncoesGenericas _ClassFuncoesGenericas = new ClassFuncoesGenericas();
            //string _diretorio = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ESTATISTICAS_TMP);

            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                Root _root = Deserialize<Root>(file);
                _retorno = ProcessaInformacoesEstatisticas(_root.envios);
                file.Close();
            }

            return _retorno;
        }

        public string ProcessaInformacoesEstatisticas(List<Envio> lstDadosEstatisticas)
        {
            string _retorno = "";

            string _IDEnvios = "";
            string _IDProducao = "";
            string _handleAvisoEletronico = "";
            string _dataSolicitacaoProcessamentoLote = "";
            string _inicioProcessamentoLote = "";
            string _fimProcessamentoLote = "";
            string _flgCarimbar = "";
            string _flgCarimboEnvio = "";
            string _dscCarimboEnvioBenner = "";
            string _flgCarimboEntrega = "";
            string _dscCarimboEntregaBenner = "";
            string _flgCarimboAbertura = "";
            string _dscCarimboAberturaBenner = "";
            string _flgBounce = "";
            string _dscBounce = "";
            string _dscBounceBenner = "";
            string _statusEnviadoBenner = "";
            string _dataEnviado = "";
            string _statusProessamentoBenner = "";
            string _tipoTemplateBenner = "";

            //sms
            string _dataRecebidoOperadora = "";
            string _statusTelefoneSMS = "";
            string _scStatusTelefoneSMSBenner = "";
            string _tipoStatusEnviadoSMS = "";
            string _statusEnviadoSMSBenner = "";
            string _dataRespostaSMS = "";
            string _respostaDataHoraSMSMensagemDestinatario = "";
            string _respostaSMSMensagemDestinatario = "";

            //e-mail
            string _dataRecebidoEmail = "";
            string _statusRecebidoEmailBenner = "";
            string _dataSpamEmail = "";
            string _dscSpamEmailBenner = "";
            string _dataAberto = "";
            string _dscDataAbertoBenner = "";
            string _dataClicado = "";
            string _dscDataClicadoBenner = "";
            string _dataOptout = "";
            string _dscDataOptoutBenner = "";

            int vcont = 0;
            int vcontX = 0;

            //GLPI164959 e GLPI165942
            string _unidadedeNegocio = "";
            string _macroProcessoTotaldocs = "";
            string _processoTotaldocs = "";
            string _handleSegundaOpiniao = "";
            string _handleRN438 = "";

            foreach (Envio elementos in lstDadosEstatisticas)
            {

                _IDEnvios = "";
                _IDProducao = "";
                _handleAvisoEletronico = "";
                _dataSolicitacaoProcessamentoLote = "";
                _inicioProcessamentoLote = "";
                _fimProcessamentoLote = "";
                _flgCarimbar = "";
                _flgCarimboEnvio = "";
                _dscCarimboEnvioBenner = "";
                _flgCarimboEntrega = "";
                _dscCarimboEntregaBenner = "";
                _flgCarimboAbertura = "";
                _dscCarimboAberturaBenner = "";
                _flgBounce = "";
                _dscBounce = "";
                _dscBounceBenner = "";
                _statusEnviadoBenner = "";
                _dataEnviado = "";
                _statusProessamentoBenner = "";
                _tipoTemplateBenner = "";

                //sms
                _dataRecebidoOperadora = "";
                _statusTelefoneSMS = "";
                _scStatusTelefoneSMSBenner = "";
                _tipoStatusEnviadoSMS = "";
                _statusEnviadoSMSBenner = "";
                _dataRespostaSMS = "";
                _respostaDataHoraSMSMensagemDestinatario = "";
                _respostaSMSMensagemDestinatario = "";

                //e-mail
                _dataRecebidoEmail = "";
                _statusRecebidoEmailBenner = "";
                _dataSpamEmail = "";
                _dscSpamEmailBenner = "";
                _dataAberto = "";
                _dscDataAbertoBenner = "";
                _dataClicado = "";
                _dscDataClicadoBenner = "";
                _dataOptout = "";
                _dscDataOptoutBenner = "";

                //GLPI164959 e GLPI165942
                _unidadedeNegocio = "";
                _macroProcessoTotaldocs = "";
                _processoTotaldocs = "";
                _handleSegundaOpiniao = "";
                _handleRN438 = "";



                if (!String.IsNullOrEmpty(elementos.template.projetoComunicacao.unidadedeNegocio.nome.ToString()))
                {
                    _unidadedeNegocio = elementos.template.projetoComunicacao.unidadedeNegocio.nome.ToString();

                    if (_unidadedeNegocio.Equals("Comunicação")) //AVISO DE CANCELAMENTO
                    {
                        _macroProcessoTotaldocs = "1";
                    }
                    if (_unidadedeNegocio.Equals("SegundaOpiniao")) // SEGUNDA OPINIAO
                    {
                        _macroProcessoTotaldocs = "2";
                    }
                    if (_unidadedeNegocio.Equals("RN438")) // RN438
                    {
                        _macroProcessoTotaldocs = "3";
                    }
                }

                //processamento lote     
                if (!String.IsNullOrEmpty(elementos.producao.dataSolicitacao.ToString()))
                {
                    _IDEnvios = elementos.id.ToString();
                }

                if (!String.IsNullOrEmpty(elementos.producao.dataSolicitacao.ToString()))
                {
                    _IDProducao = elementos.producao.id.ToString();
                }

                if (!String.IsNullOrEmpty(elementos.producao.dataSolicitacao.ToString()))
                {
                    _dataSolicitacaoProcessamentoLote = elementos.producao.dataSolicitacao.ToString();
                }

                if (!String.IsNullOrEmpty(elementos.producao.inicioProcessamento.ToString()))
                {
                    _inicioProcessamentoLote = elementos.producao.inicioProcessamento.ToString();
                }

                if (!String.IsNullOrEmpty(elementos.producao.fimProcessamento.ToString()))
                {
                    _fimProcessamentoLote = elementos.producao.fimProcessamento.ToString();
                }

                //GLPI172695
                //dados beneficiario
                Template elementoTemplate = elementos.template;
                ProjetoComunicacao elementosProjCom = elementoTemplate.projetoComunicacao;
                UnidadedeNegocio elementoUnidNeg = elementosProjCom.unidadedeNegocio;
                _processoTotaldocs = elementoUnidNeg.nome.ToUpper();

                //GLPI172695
                //GLPI164959 e GLPI165942
                vcontX = 0;
                foreach (CamposPersonalizado elementosPersonalizados in elementos.camposPersonalizados)
                {
                    if (elementos.camposPersonalizados[vcontX].nome.Equals("CODIGO_INTERNO"))
                    {
                        if (_processoTotaldocs.Equals("AVISOCANCELAMENTO") || _processoTotaldocs.Equals("COMUNICAÇÃO"))
                        {
                            _handleAvisoEletronico = elementos.camposPersonalizados[vcontX].valor;
                        }
                        if (_processoTotaldocs.Equals("SEGUNDAOPINIAO"))
                        {
                            _handleSegundaOpiniao = elementos.camposPersonalizados[vcontX].valor;
                        }
                        if (_processoTotaldocs.Equals("RN438"))
                        {
                            _handleRN438 = elementos.camposPersonalizados[vcontX].valor;
                        }
                    }
                    vcontX++;
                }

                //registros                 
                if (!String.IsNullOrEmpty(elementos.carimbar))
                {
                    _flgCarimbar = elementos.carimbar.ToString();
                }

                if (!String.IsNullOrEmpty(elementos.carimboEnvio))
                {
                    _flgCarimboEnvio = elementos.carimboEnvio.ToString();

                    if (_flgCarimboEnvio.Equals("0"))
                    {
                        //_dscCarimboEnvio = "Não possui carimbo desta categoria";
                        _dscCarimboEnvioBenner = "1";
                    }
                    if (_flgCarimboEnvio.Equals("1"))
                    {
                        //_dscCarimboEnvio = "Aguardando ser carimbado";
                        _dscCarimboEnvioBenner = "2";
                    }
                    if (_flgCarimboEnvio.Equals("2"))
                    {
                        //_dscCarimboEnvio = "Carimbo solicitado";
                        _dscCarimboEnvioBenner = "3";
                    }
                    if (_flgCarimboEnvio.Equals("3"))
                    {
                        //_dscCarimboEnvio = "Carimbo concluído";
                        _dscCarimboEnvioBenner = "4";
                    }
                }

                if (!String.IsNullOrEmpty(elementos.carimboEntrega))
                {
                    _flgCarimboEntrega = elementos.carimboEntrega.ToString();

                    if (_flgCarimboEntrega.Equals("0"))
                    {
                        //_dscCarimboEntrega = "Não possui carimbo desta categoria";
                        _dscCarimboEntregaBenner = "5";
                    }
                    if (_flgCarimboEntrega.Equals("1"))
                    {
                        //_dscCarimboEntrega = "Aguardando ser carimbado";
                        _dscCarimboEntregaBenner = "6";
                    }
                    if (_flgCarimboEntrega.Equals("2"))
                    {
                        //_dscCarimboEntrega = "Carimbo solicitado";
                        _dscCarimboEntregaBenner = "7";
                    }
                    if (_flgCarimboEntrega.Equals("3"))
                    {
                        //_dscCarimboEntrega = "Carimbo concluído";
                        _dscCarimboEntregaBenner = "8";
                    }
                }

                if (!String.IsNullOrEmpty(elementos.carimboAbertura))
                {
                    _flgCarimboAbertura = elementos.carimboAbertura.ToString();

                    if (_flgCarimboAbertura.Equals("0"))
                    {
                        //_dscCarimboAbertura = "Não possui carimbo desta categoria";
                        _dscCarimboAberturaBenner = "9";
                    }
                    if (_flgCarimboAbertura.Equals("1"))
                    {
                        //_dscCarimboAbertura = "Aguardando ser carimbado";
                        _dscCarimboAberturaBenner = "10";
                    }
                    if (_flgCarimboAbertura.Equals("2"))
                    {
                        //_dscCarimboAbertura = "Carimbo solicitado";
                        _dscCarimboAberturaBenner = "11";
                    }
                    if (_flgCarimboAbertura.Equals("3"))
                    {
                        //_dscCarimboAbertura = "Carimbo concluído";
                        _dscCarimboAberturaBenner = "12";
                    }
                }

                //bounce
                if (!String.IsNullOrEmpty(elementos.bounce))
                {
                    _flgBounce = elementos.bounce.ToString();

                    if (_flgBounce.Equals("0"))
                    {
                        _dscBounce = "Não houve bounce";
                        _dscBounceBenner = "13";
                    }
                    if (_flgBounce.Equals("1"))
                    {
                        _dscBounce = ""; //*********************************

                        if (elementos.template.tipo.Equals("0")) // E-mail
                        {
                            _dscBounce = "Soft Bounce ";
                            _dscBounce += "(A caixa de e-mail do destinatário está cheia; ";
                            _dscBounce += "O servidor de e-mail do lead não está respondendo; ";
                            _dscBounce += "O e-mail encaminhado é muito grande e o servidor demora para recebê-lo; ";
                            _dscBounce += "A proteção antispam do servidor analisou o conteúdo do e-mail e rejeitou o recebimento; ";
                            _dscBounce += "A quantidade máxima de envios para o mesmo servidor em um período de tempo foi ultrapassada)";

                            _dscBounceBenner = "14";
                        }

                        if (elementos.template.tipo.Equals("2")) // SMS
                        {
                            _dscBounce = "Erro de entrega ";
                            _dscBounce += "(Erro de comunicação com a operadora; ";
                            _dscBounce += "A operadora rejeitou a mensagem; ";
                            _dscBounce += "O número de telefone de destino é inválido; ";
                            _dscBounce += "O texto da mensagem contém palavras que não são aceitas pela operadora; ";
                            _dscBounce += "A operadora aceitou a mensagem, mas não conseguiu entregá-la ao dispositivo.)";

                            _dscBounceBenner = "15";
                        }

                        //GLPI126128
                        if (elementos.template.tipo.Equals("4")) // Whatsapp
                        {
                            _dscBounce = "Erro de entrega ";
                            _dscBounce += "(Erro de comunicação com a operadora; ";
                            _dscBounce += "A operadora rejeitou a mensagem; ";
                            _dscBounce += "O número de telefone de destino é inválido; ";
                            _dscBounce += "O texto da mensagem contém palavras que não são aceitas pela operadora; ";
                            _dscBounce += "A operadora aceitou a mensagem, mas não conseguiu entregá-la ao dispositivo;";
                            _dscBounce += "WhatsApp não está instalado no aparelho)";

                            _dscBounceBenner = "51";
                        }

                    }
                    if (_flgBounce.Equals("2"))
                    {
                        _dscBounce = "Hard Bounce ";
                        _dscBounce += "(O endereço do e-mail do remetente não existe ou foi digitado incorretamente; ";
                        _dscBounce += "O servidor de e-mail do remetente bloqueou o recebimento dos seus e-mails)";

                        _dscBounceBenner = "16";
                    }
                }

                //data de envio
                _dataEnviado = elementos.dataEnviado.ToString();
                _statusEnviadoBenner = "17";
                if (!String.IsNullOrEmpty(_dataEnviado))
                {
                    //_statusEnviado = "OK";
                    _statusEnviadoBenner = "18";
                }

                //tipo
                if (elementos.template.tipo.Equals("0"))
                {
                    //_tipoTemplate = "E-mail";
                    _tipoTemplateBenner = "19";
                }
                else if (elementos.template.tipo.Equals("2"))
                {
                    //_tipoTemplate = "SMS";
                    _tipoTemplateBenner = "20";

                    _dataRecebidoOperadora = elementos.dataRecebidoOperadora.ToString();

                    if (!String.IsNullOrEmpty(_dataEnviado))
                    {
                        if (elementos.bounce.Equals("0"))
                        {
                            _tipoStatusEnviadoSMS = "A"; // AVISO
                            //_statusEnviadoSMS = "SMS entregue no aparelho do destinatário";
                            _statusEnviadoSMSBenner = "21";
                        }
                        if (elementos.bounce.Equals("1"))
                        {
                            _tipoStatusEnviadoSMS = "E"; // ERRO
                            //_statusEnviadoSMS = "Erro de entrega da mensagem";
                            _statusEnviadoSMSBenner = "22";
                        }
                    }

                    _statusTelefoneSMS = elementos.status.ToString();
                    if (!String.IsNullOrEmpty(_statusTelefoneSMS))
                    {
                        if (elementos.status.ToString().Equals("0"))
                        {
                            //_scStatusTelefoneSMS = "Cadastrado";
                            _scStatusTelefoneSMSBenner = "23";
                        }
                        if (elementos.status.ToString().Equals("1"))
                        {
                            //_scStatusTelefoneSMS = "Enviado";
                            _scStatusTelefoneSMSBenner = "24";
                        }
                        if (elementos.status.ToString().Equals("2"))
                        {
                            //_scStatusTelefoneSMS = "Inválido, foi cadastrado, mas não será enviado";
                            _scStatusTelefoneSMSBenner = "25";
                        }
                        if (elementos.status.ToString().Equals("3"))
                        {
                            //_scStatusTelefoneSMS = "Blacklist, foi cadastrado, mas não será enviado, pois já consta na lista de supressão";
                            _scStatusTelefoneSMSBenner = "26";
                        }
                    }

                    if (!String.IsNullOrEmpty(elementos.dataRespostaSMS.ToString()))
                    {
                        _dataRespostaSMS = elementos.dataRespostaSMS.ToString();
                        //_statusEnviadoSMS = "Destinatário respondeu a mensagem com algum texto";
                        _statusEnviadoSMSBenner = "27";
                        _respostaDataHoraSMSMensagemDestinatario = elementos.dataRespostaSMS.ToString();
                        _respostaSMSMensagemDestinatario = elementos.respostaSMS.ToString();
                    }
                }
                else if (elementos.template.tipo.Equals("4")) //GLPI126128
                {
                    //_tipoTemplate = "Whatsapp";
                    _tipoTemplateBenner = "42";

                    _dataRecebidoOperadora = elementos.dataRecebidoOperadora.ToString();

                    if (!String.IsNullOrEmpty(_dataEnviado))
                    {
                        if (elementos.bounce.Equals("0"))
                        {
                            _tipoStatusEnviadoSMS = "A"; // AVISO
                            //_statusEnviadoSMS = "WhatsApp entregue no aparelho do destinatário";
                            _statusEnviadoSMSBenner = "43";
                        }
                        if (elementos.bounce.Equals("1"))
                        {
                            _tipoStatusEnviadoSMS = "E"; //ERRO
                            //_statusEnviadoSMS = "Erro de entrega da mensagem.";
                            _statusEnviadoSMSBenner = "44";
                        }
                    }

                    _statusTelefoneSMS = elementos.status.ToString();
                    if (!String.IsNullOrEmpty(_statusTelefoneSMS))
                    {
                        if (elementos.status.ToString().Equals("0"))
                        {
                            //_scStatusTelefoneSMS = "Cadastrado";
                            _scStatusTelefoneSMSBenner = "45";
                        }
                        if (elementos.status.ToString().Equals("1"))
                        {
                            //_scStatusTelefoneSMS = "Enviado";
                            _scStatusTelefoneSMSBenner = "46";
                        }
                        if (elementos.status.ToString().Equals("2"))
                        {
                            //_scStatusTelefoneSMS = "Inválido, foi cadastrado, mas não será enviado";
                            _scStatusTelefoneSMSBenner = "47";
                        }
                        if (elementos.status.ToString().Equals("3"))
                        {
                            //_scStatusTelefoneSMS = "Blacklist, foi cadastrado, mas não será enviado, pois já consta na lista de supressão";
                            _scStatusTelefoneSMSBenner = "48";
                        }
                    }

                    if (!String.IsNullOrEmpty(elementos.dataRespostaSMS.ToString()))
                    {
                        _dataRespostaSMS = elementos.dataRespostaSMS.ToString();
                        //_statusEnviadoSMS = "O Destinatário respondeu a mensagem com algum texto. Consultar o campo respostaSMS para ver o que foi respondido";
                        _statusEnviadoSMSBenner = "49";
                        _respostaDataHoraSMSMensagemDestinatario = elementos.dataRespostaSMS.ToString();
                        _respostaSMSMensagemDestinatario = elementos.respostaSMS.ToString();
                    }


                }

                // status
                if (elementos.status.Equals("0"))
                {
                    //_statusProcessamento = "Fila de processamento";
                    _statusProessamentoBenner = "28";
                }
                else if (elementos.status.Equals("1"))
                {
                    //_statusProcessamento = "Cadastrando envios";
                    _statusProessamentoBenner = "29";
                }
                else if (elementos.status.Equals("2"))
                {
                    //_statusProcessamento = "Envios em andamento";
                    _statusProessamentoBenner = "30";
                }
                else if (elementos.status.Equals("3"))
                {
                    //_statusProcessamento = "Envio finalizado";
                    _statusProessamentoBenner = "31";
                }
                else if (elementos.status.Equals("4"))
                {
                    //_statusProcessamento = "Envio abortado por excesso de bounces ou spams";
                    _statusProessamentoBenner = "32";
                }
                else if (elementos.status.Equals("5"))
                {
                    //_statusProcessamento = "Envio cancelado";
                    _statusProessamentoBenner = "33";
                }

                //dataRecebido
                if (!String.IsNullOrEmpty(elementos.dataRecebido.ToString()))
                {
                    _dataRecebidoEmail = elementos.dataRecebido.ToString();

                    if (elementos.template.tipo.Equals("0"))
                    {
                        if (elementos.bounce.ToString().Equals("0"))
                        {
                            //_statusRecebidoEmail = "E-mail entregue no servidor de e-mail do destinatário";
                            _statusRecebidoEmailBenner = "34";
                        }
                    }
                    if (elementos.template.tipo.Equals("4")) //GLPI126128
                    {
                        if (elementos.bounce.ToString().Equals("0"))
                        {
                            //_statusRecebidoEmail = "Whatsapp entregue no servidor do destinatário";
                            _statusRecebidoEmailBenner = "52";
                        }
                    }

                    if (elementos.bounce.ToString().Equals("1"))
                    {
                        //_statusRecebidoEmail = "Soft bounce";
                        _statusRecebidoEmailBenner = "35";
                    }
                    if (elementos.bounce.ToString().Equals("2"))
                    {
                        //_statusRecebidoEmail = "Hard bounce";
                        _statusRecebidoEmailBenner = "36";
                    }
                }

                //Spam
                if (!String.IsNullOrEmpty(elementos.dataSpam.ToString()))
                {
                    _dataSpamEmail = elementos.dataSpam.ToString();
                    //_dscDataSpam = "Destinatário marcou a mensagem como spam em seu provedor";
                    _dscSpamEmailBenner = "37";
                }

                //dataAberto
                _dataAberto = "";
                if (!String.IsNullOrEmpty(elementos.dataAberto.ToString()))
                {
                    if (elementos.template.tipo.Equals("0"))
                    {
                        _dataAberto = elementos.dataAberto.ToString();
                        //_dscDataAberto = "E-mail aberto pelo destinatário";
                        _dscDataAbertoBenner = "38";
                    }
                    if (elementos.template.tipo.Equals("4")) // GLPI126128
                    {
                        _dataAberto = elementos.dataAberto.ToString();
                        //_dscDataAberto = "WhatsApp lido pelo destinatário";
                        _dscDataAbertoBenner = "50";
                    }
                }

                //dataClicado
                if (!String.IsNullOrEmpty(elementos.dataClicado.ToString()))
                {
                    _dataClicado = elementos.dataClicado.ToString();
                    //_dscDataClicado = "Algum link do e-mail clicado pelo destinatário";
                    _dscDataClicadoBenner = "39";
                }

                //dataOptout
                if (!String.IsNullOrEmpty(elementos.dataOptout.ToString()))
                {
                    _dataOptout = elementos.dataOptout.ToString();
                    //_dscDataOptout = "Destinatário solicitou descadastramento do envio de e-mails";
                    _dscDataOptoutBenner = "40";
                }

                vcont++;


                ClassDadosEstatisticasNormalizada _ClassDadosEstatisticasNormalizada = new ClassDadosEstatisticasNormalizada();

                //GLPI164959 e GLPI165942
                _ClassDadosEstatisticasNormalizada.pMacroProcesso = _macroProcessoTotaldocs;
                _ClassDadosEstatisticasNormalizada.pProcesso = _processoTotaldocs;
                _ClassDadosEstatisticasNormalizada.phandleSegundaOpiniao = _handleSegundaOpiniao;
                _ClassDadosEstatisticasNormalizada.phandleRN438 = _handleRN438;
                _ClassDadosEstatisticasNormalizada.phandleAvisoEletronico = _handleAvisoEletronico;

                _ClassDadosEstatisticasNormalizada.pidproducao = _IDProducao;
                _ClassDadosEstatisticasNormalizada.pidenvio = _IDEnvios;
                _ClassDadosEstatisticasNormalizada.pdatasolicitacaoprocessamento = _dataSolicitacaoProcessamentoLote;
                _ClassDadosEstatisticasNormalizada.pinicioprocessamento = _inicioProcessamentoLote;
                _ClassDadosEstatisticasNormalizada.pfimprocessamento = _fimProcessamentoLote;
                _ClassDadosEstatisticasNormalizada.pflagcarimbar = _flgCarimbar;
                _ClassDadosEstatisticasNormalizada.pflagcarimboenvio = _flgCarimboEnvio;
                _ClassDadosEstatisticasNormalizada.pcarimboenvio = _dscCarimboEnvioBenner;
                _ClassDadosEstatisticasNormalizada.pflagcarimboentrega = _flgCarimboEntrega;
                _ClassDadosEstatisticasNormalizada.pcarimboentrega = _dscCarimboEntregaBenner;
                _ClassDadosEstatisticasNormalizada.pflagcarimboabertura = _flgCarimboAbertura;
                _ClassDadosEstatisticasNormalizada.pcarimboabertura = _dscCarimboAberturaBenner;
                _ClassDadosEstatisticasNormalizada.pflagbounce = _flgBounce;
                _ClassDadosEstatisticasNormalizada.pbounce = _dscBounceBenner;
                _ClassDadosEstatisticasNormalizada.pstatusenviado = _statusEnviadoBenner;
                _ClassDadosEstatisticasNormalizada.pdataenviado = _dataEnviado;
                _ClassDadosEstatisticasNormalizada.pstatusprocessamento = _statusProessamentoBenner;
                _ClassDadosEstatisticasNormalizada.ptipotemplate = _tipoTemplateBenner;
                _ClassDadosEstatisticasNormalizada.pdatarecebidooperadora = _dataRecebidoOperadora;
                _ClassDadosEstatisticasNormalizada.pstatustelefonesms = _scStatusTelefoneSMSBenner;
                _ClassDadosEstatisticasNormalizada.pstatusenviadosms = _statusEnviadoSMSBenner;
                _ClassDadosEstatisticasNormalizada.ptipostatusenviadosms = _tipoStatusEnviadoSMS;
                _ClassDadosEstatisticasNormalizada.pdatarespostasms = _dataRespostaSMS;
                _ClassDadosEstatisticasNormalizada.prespdatahorasmsmsgdesto = _respostaDataHoraSMSMensagemDestinatario;
                _ClassDadosEstatisticasNormalizada.prespostahorasmsmsgdest = _respostaSMSMensagemDestinatario;
                _ClassDadosEstatisticasNormalizada.pdatarecebidoemail = _dataRecebidoEmail;
                _ClassDadosEstatisticasNormalizada.pstatusrecebidoemail = _statusRecebidoEmailBenner;
                _ClassDadosEstatisticasNormalizada.pdataspamemail = _dataSpamEmail;
                _ClassDadosEstatisticasNormalizada.pspamemail = _dscSpamEmailBenner;
                _ClassDadosEstatisticasNormalizada.pdataaberto = _dataAberto;
                _ClassDadosEstatisticasNormalizada.pdscdataaberto = _dscDataAbertoBenner;
                _ClassDadosEstatisticasNormalizada.pdataclicado = _dataClicado;
                _ClassDadosEstatisticasNormalizada.pdscdataclicado = _dscDataClicadoBenner;
                _ClassDadosEstatisticasNormalizada.pdataoptout = _dataOptout;
                _ClassDadosEstatisticasNormalizada.pdscdataoptout = _dscDataOptoutBenner;

                RegistroEstatisticas(_macroProcessoTotaldocs, _ClassDadosEstatisticasNormalizada);

            }
            
            return _retorno;
        }

        public void RegistroEstatisticas(string MacroProcesso, ClassDadosEstatisticasNormalizada DadosEstatisticas)
        {
            string retorno = "";
            try
            {
                Conexao _Conexao = new Conexao();
                Oracle.ManagedDataAccess.Client.OracleConnection SecaoBD;

                SecaoBD = _Conexao.AbreConexao(VariaveisGlobais.InstanciaConexao);
                OracleCommand vDadosCF = new OracleCommand("SAUDEPRO.PLANO_PKG_TOTALDOCSESTATPROCESSOS.PLANO_P_DADOSESTATISTICAS", SecaoBD);

                vDadosCF.CommandType = System.Data.CommandType.StoredProcedure;

                //GLPI164959 e GLPI165942
                vDadosCF.Parameters.Add("PMACROPROCESSO", OracleDbType.Varchar2).Value = DadosEstatisticas.pMacroProcesso.ToString();
                vDadosCF.Parameters.Add("PHANDLEAVISOELETRONICO", OracleDbType.Varchar2).Value = DadosEstatisticas.phandleAvisoEletronico.ToString();
                vDadosCF.Parameters.Add("PHANDLESEGUNDAOPINIAO", OracleDbType.Varchar2).Value = DadosEstatisticas.phandleSegundaOpiniao.ToString();
                vDadosCF.Parameters.Add("PHANDLERN438", OracleDbType.Varchar2).Value = DadosEstatisticas.phandleRN438.ToString();

                vDadosCF.Parameters.Add("PIDPRODUCAO", OracleDbType.Varchar2).Value = DadosEstatisticas.pidproducao.ToString();
                vDadosCF.Parameters.Add("PIDENVIO", OracleDbType.Varchar2).Value = DadosEstatisticas.pidenvio.ToString();
                vDadosCF.Parameters.Add("PDATASOLICITACAOPROCESSAMENTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdatasolicitacaoprocessamento.ToString();
                vDadosCF.Parameters.Add("PINICIOPROCESSAMENTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pinicioprocessamento.ToString();
                vDadosCF.Parameters.Add("PFIMPROCESSAMENTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pinicioprocessamento.ToString();
                vDadosCF.Parameters.Add("PFLAGCARIMBAR", OracleDbType.Varchar2).Value = DadosEstatisticas.pflagcarimbar.ToString();
                vDadosCF.Parameters.Add("PFLAGCARIMBOENVIO", OracleDbType.Varchar2).Value = DadosEstatisticas.pflagcarimboenvio.ToString();
                vDadosCF.Parameters.Add("PCARIMBOENVIO", OracleDbType.Varchar2).Value = DadosEstatisticas.pcarimboenvio.ToString();
                vDadosCF.Parameters.Add("PFLAGCARIMBOENTREGA", OracleDbType.Varchar2).Value = DadosEstatisticas.pflagcarimboentrega.ToString();
                vDadosCF.Parameters.Add("PCARIMBOENTREGA", OracleDbType.Varchar2).Value = DadosEstatisticas.pcarimboentrega.ToString();
                vDadosCF.Parameters.Add("PFLAGCARIMBOABERTURA", OracleDbType.Varchar2).Value = DadosEstatisticas.pflagcarimboabertura.ToString();
                vDadosCF.Parameters.Add("PCARIMBOABERTURA", OracleDbType.Varchar2).Value = DadosEstatisticas.pcarimboabertura.ToString();
                vDadosCF.Parameters.Add("PFLAGBOUNCE", OracleDbType.Varchar2).Value = DadosEstatisticas.pflagbounce.ToString();
                vDadosCF.Parameters.Add("PBOUNCE", OracleDbType.Varchar2).Value = DadosEstatisticas.pbounce.ToString();
                vDadosCF.Parameters.Add("PSTATUSENVIADO", OracleDbType.Varchar2).Value = DadosEstatisticas.pstatusenviado.ToString();
                vDadosCF.Parameters.Add("PDATAENVIADO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdataenviado.ToString();
                vDadosCF.Parameters.Add("PSTATUSPROCESSAMENTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pstatusenviado.ToString();
                vDadosCF.Parameters.Add("PTIPOTEMPLATE", OracleDbType.Varchar2).Value = DadosEstatisticas.ptipotemplate.ToString();
                vDadosCF.Parameters.Add("PDATARECEBIDOOPERADORA", OracleDbType.Varchar2).Value = DadosEstatisticas.pdatarecebidooperadora.ToString();
                vDadosCF.Parameters.Add("PSTATUSTELEFONESMS", OracleDbType.Varchar2).Value = DadosEstatisticas.pstatustelefonesms.ToString();
                vDadosCF.Parameters.Add("PSTATUSENVIADOSMS", OracleDbType.Varchar2).Value = DadosEstatisticas.pstatusenviadosms.ToString();
                vDadosCF.Parameters.Add("PTIPOSTATUSENVIADOSMS", OracleDbType.Varchar2).Value = DadosEstatisticas.ptipostatusenviadosms.ToString();
                vDadosCF.Parameters.Add("PDATARESPOSTASMS", OracleDbType.Varchar2).Value = DadosEstatisticas.pdatarespostasms.ToString();
                vDadosCF.Parameters.Add("PRESPDATAHORASMSMSGDESTO", OracleDbType.Varchar2).Value = DadosEstatisticas.prespdatahorasmsmsgdesto.ToString();
                vDadosCF.Parameters.Add("PRESPOSTAHORASMSMSGDEST", OracleDbType.Varchar2).Value = DadosEstatisticas.prespostahorasmsmsgdest.ToString();
                vDadosCF.Parameters.Add("PDATARECEBIDOEMAIL", OracleDbType.Varchar2).Value = DadosEstatisticas.pdatarecebidoemail.ToString();
                vDadosCF.Parameters.Add("PSTATUSRECEBIDOEMAIL", OracleDbType.Varchar2).Value = DadosEstatisticas.pstatusrecebidoemail.ToString();
                vDadosCF.Parameters.Add("PDATASPAMEMAIL", OracleDbType.Varchar2).Value = DadosEstatisticas.pdataspamemail.ToString();
                vDadosCF.Parameters.Add("PSPAMEMAIL", OracleDbType.Varchar2).Value = DadosEstatisticas.pspamemail.ToString();
                vDadosCF.Parameters.Add("PDATAABERTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdataaberto.ToString();
                vDadosCF.Parameters.Add("PDSCDATAABERTO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdscdataaberto.ToString();
                vDadosCF.Parameters.Add("PDATACLICADO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdataclicado.ToString();
                vDadosCF.Parameters.Add("PDSCDATACLICADO", OracleDbType.Varchar2).Value = DadosEstatisticas.pdscdataclicado.ToString();
                vDadosCF.Parameters.Add("PDATAOPTOUT", OracleDbType.Varchar2).Value = DadosEstatisticas.pdataoptout.ToString();
                vDadosCF.Parameters.Add("PDSCDATAOPTOUT", OracleDbType.Varchar2).Value = DadosEstatisticas.pdscdataoptout.ToString();

                OracleParameter p1 = new OracleParameter("RETCADASTRO", OracleDbType.Varchar2, 50);
                p1.Direction = System.Data.ParameterDirection.Output;
                vDadosCF.Parameters.Add(p1);

                vDadosCF.ExecuteNonQuery();

                retorno = vDadosCF.Parameters["RETCADASTRO"].Value.ToString();

                SecaoBD.Close();
                _Conexao.FechaConexao();

            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(0).Append(" APITotaldocs - ClassSolicitacaoCarteirinhaFisica - RegistroSolicitacaoCarteirinhaFisica - ");
                builder.Append(1).Append(" " + ex.Message);

                ClassTratamentoErros _ClassTratamentoErros = new ClassTratamentoErros();
                _ClassTratamentoErros.DescricaoErro = builder.ToString();
            }
        }

        public static void Serialize(object value, Stream s)
        {
            using (StreamWriter writer = new StreamWriter(s))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
                ser.Serialize(jsonWriter, value);
                jsonWriter.Flush();
            }
        }

        public static T Deserialize<T>(Stream s)
        {
            using (StreamReader reader = new StreamReader(s))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
                return ser.Deserialize<T>(jsonReader);
            }
        }


        public string Processamenteo_Acertos()
        {
            string _retorno = "OK";
            try
            {
                ClassFuncoesGenericas _ClassFuncoesGenericas = new ClassFuncoesGenericas();
                //string _diretorioArqEstatisticasTmp = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ESTATISTICAS_TMP);
                //string _diretorioArqEstatisticas = _ClassFuncoesGenericas.DiretoriosArquivos(VariaveisGlobais.PASTA_REDE_ARQUIVOS_ESTATISTICAS);

                string _diretorioArqEstatisticas = @"C:\WebApp\Arquivos\TOTALDOCS\ESTATISTICASTMP_03012022_1536";
                string _diretorioArqEstatisticasTmp = @"\\hmsc68\TOTALDOCS\PROCESSOEMAILS\ESTATISTICASTMP";

                DirectoryInfo info = new DirectoryInfo(_diretorioArqEstatisticas);
                DirectoryInfo infoDel = new DirectoryInfo(_diretorioArqEstatisticasTmp);

                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                FileInfo[] files1 = files.OrderBy(p => p.Name).ToArray();

                //VERIFICA SE TEM 'S' NECESSIDADE DE GRAVAR OS ARQUIVOS OU 'N' APAGAR O ARQUIVO APOS PROCESSAMENTO DOS DADOS
                //ClassGravarDetalhesErros _ClassGravarDetalhesErros = new ClassGravarDetalhesErros();
                //string ligarCtrl = _ClassGravarDetalhesErros.ConsultaControles("TOTALDOCS_ARMAZENARARQUIVOS_TOTALDOCSESTATISTICAS");

                foreach (FileInfo file in files1)
                {
                    if ((file.Extension.ToString().ToUpper().Equals(".TXT")))
                    {

                        FileInfo[] filesDel = infoDel.GetFiles().OrderBy(p => p.CreationTime).ToArray();

                        foreach (FileInfo fileDel in filesDel)
                        {
                            if ((fileDel.Name.ToUpper().Equals(file.Name.ToUpper())))
                            {
                                fileDel.Delete();
                            }
                        }


                                //string ret = ProcessaArquivo(file.ToString());

                                //if (ligarCtrl.Equals("S"))
                                //{
                                //    file.MoveTo(_diretorioArqEstatisticas + "\\" + file.Name.ToString());
                                //}

                                //if (ligarCtrl.Equals("N"))
                                //{
                                //    file.Delete();
                                //}
                            }
                }
            }
            catch (Exception ex)
            {
                _retorno = "ERRO: " + ex.Message.ToString();
            }

            return "OK";
        }

    }
}

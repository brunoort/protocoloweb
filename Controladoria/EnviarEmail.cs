using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Controladoria;

namespace ProtocoloWeb
{
    public class EnviarEmail
    {

        public void Enviar(string Operacao, string NumAP, string to, string cc, string from)
        {
            string smtp = Comum.BuscarChaveWebConfig("smtp");
            string msgTitle = "", msgOperacao = "", msgData = "";

            //Mensagens dos e-mails enviados
            if (Operacao == "Cadastro")
            {
                msgTitle = "Protocolo – Retorno";
                msgOperacao = "A AP nº " + NumAP + " foi recebida na data " + DateTime.Now.ToString("dd/MM/yyyy hh:mm") + " no Painel Controladoria e está sujeita a verificação";
            }
            else if (Operacao == "Finalizado")
            {
                msgTitle = "Protocolo – Finalizado";
                msgOperacao = "A AP nº " + NumAP + " foi concluída no Protocolo Web na data " + DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            }
            else if (Operacao == "Controladoria")
            {
                msgTitle = "Protocolo – Recebido e Aprovado pelo Painel Controladoria";
                msgOperacao = "A AP nº " + NumAP + " foi recebida e aprovada pelo Painel Controladoria na data " + DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            }

            StringBuilder strMsg = new StringBuilder();
            strMsg.Append("<html>");
            strMsg.Append("      <head><title>AP." + NumAP + " - " + msgTitle + "</title></head>");
            strMsg.Append("      <body>");
            strMsg.Append("          <table border='0' cellpadding='0' width='530'>");
            strMsg.Append("              <tr>");
            strMsg.Append("                  <td bgcolor='#000066'><strong><font color='#FFFFFF' face='Arial' size='2'>N." + NumAP + " - " + msgTitle + "</font></strong></td>");
            strMsg.Append("              </tr>");
            strMsg.Append("              <tr>");
            strMsg.Append("                  <td bgcolor='#CCCCCC'>");
            strMsg.Append("                      <table border='0' cellpadding='0' width='90%' height='90%' align='center'>");
            strMsg.Append("                          <tr>");
            strMsg.Append("                              <td bgcolor='#FFFFFF'><font face='Arial' size='2'>");
            strMsg.Append("                                  <br>");
            strMsg.Append("                                  " + msgOperacao + "<br>");
            strMsg.Append("                                  <br>");
            strMsg.Append("                                  </font></td>");
            strMsg.Append("                          </tr>");
            strMsg.Append("                      </table>");
            strMsg.Append("                  </td>");
            strMsg.Append("              </tr>");
            strMsg.Append("          </table>");
            strMsg.Append("      </body>");
            strMsg.Append("</html>");


            if (to != "")
            {
                MailMessage message = new MailMessage(from, to, msgTitle, strMsg.ToString());

                message.IsBodyHtml = true;
                if (cc != "")
                {
                    message.CC.Add(cc);
                }


                message.BodyEncoding = System.Text.Encoding.UTF8;

                SmtpClient client = new SmtpClient(smtp);
                client.Send(message);
            }
        }
    }
}
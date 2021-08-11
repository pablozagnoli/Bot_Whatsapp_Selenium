using OpenQA.Selenium.Chrome;
using System;
using System.Threading;


namespace Bot
{
    class Program
    {
        #region█VARIABLESGLOBAIS

        public static ChromeDriver vCrome = new ChromeDriver(@"C:\Program Files\Google\Chrome\Application\");
        public static string vUrl = "https://web.whatsapp.com/";
        public static string vMenssagemSend = String.Empty;
        public static string vMenssagemSearch = String.Empty;
        public static string vContato = String.Empty;
        public static int vSpanCount = 0;
        public static string vSpanMessageText = string.Empty;

        #endregion

        static void Main(string[] args)
        {
            Thread.Sleep(8000);

            Start_Config();
        }
        public static void Chat_Open()
        {
            try
            {
                #region█Variables

                bool vtryVer = true;

                #endregion

                vCrome.Navigate().GoToUrl(vUrl);
                vCrome.Manage().Window.Maximize();

                Thread.Sleep(8000);

                while (vtryVer)
                {
                    try
                    {
                        var vPesquisaEltry = vCrome.FindElementByClassName("selectable-text");
                        vtryVer = false;
                    }
                    catch (Exception)
                    {
                        vtryVer = true;
                    }
                }
                Chat_Monitora();
            }
            catch (Exception)
            {
            }

        }
        public static void Chat_Monitora()
        {
            try
            {
                while (true)
                {
                    var vMonitorChat = vCrome.FindElementsByClassName("_15smv");
                    var vMonitorChat2 = vCrome.FindElementsByClassName("_3Dr46");

                    for (int i = 0; i < vMonitorChat.Count; i++)
                    {
                        int vConvertDate = 0;
                        var vMonitorChatText = vMonitorChat[i].Text;
                        string v1 = vMonitorChatText.ToString();
                        try
                        {
                            vConvertDate = Int32.Parse(v1.Substring(0, 2)) + Int32.Parse(v1.Substring(3, 2));
                        }
                        catch (Exception)
                        {
                            vConvertDate = 0;
                        }
                        DateTime vDate = DateTime.Now;
                        int vHourSystem = vDate.Hour + vDate.Minute;

                        if (vConvertDate < vHourSystem && vConvertDate > 0 || vConvertDate == vHourSystem)
                        {
                            vMonitorChat[i].Click();

                            Thread.Sleep(1000);

                            var vSpan = vCrome.FindElementsByClassName("_3ExzF");

                            if (vSpan.Count > 0)
                            {
                                vSpanMessageText = vSpan[vSpan.Count - 1].Text;
                            }
                            else
                            {
                                vSpanMessageText = "abc";
                            }


                            if (vSpanMessageText == vMenssagemSearch)
                            {
                                vMonitorChat[i].Click();

                                Thread.Sleep(3000);

                                var vChatEl = vCrome.FindElementsByClassName("_2_1wd");
                                vChatEl[1].SendKeys(vMenssagemSend);

                                Thread.Sleep(3000);

                                var vButtonSendEl = vCrome.FindElementByClassName("_1E0Oz");
                                vButtonSendEl.Click();

                                Thread.Sleep(3000);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public static void Start_Config()
        {
            try
            {
                Console.WriteLine("Olá. Por gentileza Escreva a Baixo a Palavra chave que deseja Que seja enviada uma mensagem Quando a Receber.");
                vMenssagemSearch = Console.ReadLine();
                if (!String.IsNullOrEmpty(vMenssagemSearch))
                {
                    Console.WriteLine("Obrigado, Voce Tera uma resposta Automatica Sempre que alguem lhe enviar a palavra: " + vMenssagemSearch);
                }
                else
                {
                    Console.WriteLine("Por gentileza Escreva a Baixo a Palavra chave que deseja Que seja enviada uma mensagem Quando a Receber.");
                    vMenssagemSearch = Console.ReadLine();
                }

                Console.WriteLine("Olá. Por gentileza Escreva a Baixo a Frase que deseja Que seja enviada Sempre que receber a Palavra chave.");
                vMenssagemSend = Console.ReadLine();
                if (!String.IsNullOrEmpty(vMenssagemSend))
                {
                    Console.WriteLine("Obrigado, Voce Tera a resposta Automatica" + "'" + vMenssagemSend + "'" +
                    " Sempre que alguem lhe enviar a palavra: " + vMenssagemSearch);
                }
                else
                {
                    Console.WriteLine("Por gentileza Escreva a Baixo a Frase que deseja Que seja enviada Sempre que receber a Palavra chave.");
                    vMenssagemSend = Console.ReadLine();
                }

                Thread.Sleep(8000);

                Chat_Open();
            }
            catch (Exception)
            {

            }
        }
    }
}

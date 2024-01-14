// 
// Program.cs
// 
using System;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using Gtk;
using RollingAttribute;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace QCS
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                Application.Init();
                using var win = new MainWindow { Title = RC.RollingData.Title };
                
                MainWindow.ThisWindow = win;
                win.Show();
                Application.Run();
            }
            catch (Exception e)
            {
                MailDelegate mail = new MailDelegate(Mail);
                IAsyncResult ar = mail.BeginInvoke(e, null, null);
                ar.AsyncWaitHandle.WaitOne();
                MainWindow.ShowMessage(RC.RollingData.ErrorMessage, MessageType.Error, ButtonsType.Ok, out var _);
                mail.EndInvoke(ar);
            }
        }

        delegate void MailDelegate(Exception e);

        static void Mail(Exception e)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            using SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("*********@gmail.com", "**********")
            };
#pragma warning restore CS0618
            MailAddress from = new MailAddress("RollingCashier@rolling.ress", "Rolling Cashier", Encoding.UTF8);
            MailAddress to = new MailAddress("********@gmail.com");
            MailMessage message = new MailMessage(from, to)
            {
                Body = $@"To Sunwoo
                    
Hi, Sunwoo. I'm an anonymous user.
I like Rolling Cashier so much.. but I got some error :(

The Error was occured in {e.Source}.
Well...{e.Message}.
And Here is a stacktrace:
{e.StackTrace}

Please fix it as fast as you can.
I'm waiting.

Thanks.
From ananoymous",
                BodyEncoding = Encoding.UTF8,
                Subject = $"{RC.RollingData.ProgramName} {RC.RollingData.Version} Bug Report",
                SubjectEncoding = Encoding.UTF8
            };
            client.Send(message);
        }
    }
}

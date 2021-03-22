using System;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class Mail : Payload
        {
            public enum MailEncoding
            {
                MAILTO,
                MATMSG,
                SMTP
            }

            private readonly MailEncoding encoding;
            private readonly string mailReceiver, subject, message;

            /// <summary>
            /// Creates an empty email payload
            /// </summary>
            /// <param name="mailReceiver">Receiver's email address</param>
            /// <param name="encoding">Payload encoding type. Choose dependent on your QR Code scanner app.</param>
            public Mail(string mailReceiver, MailEncoding encoding = MailEncoding.MAILTO)
            {
                this.mailReceiver = mailReceiver;
                subject           = message = string.Empty;
                this.encoding     = encoding;
            }

            /// <summary>
            /// Creates an email payload with subject
            /// </summary>
            /// <param name="mailReceiver">Receiver's email address</param>
            /// <param name="subject">Subject line of the email</param>
            /// <param name="encoding">Payload encoding type. Choose dependent on your QR Code scanner app.</param>
            public Mail(string mailReceiver, string subject, MailEncoding encoding = MailEncoding.MAILTO)
            {
                this.mailReceiver = mailReceiver;
                this.subject      = subject;
                message           = string.Empty;
                this.encoding     = encoding;
            }

            /// <summary>
            /// Creates an email payload with subject and message/text
            /// </summary>
            /// <param name="mailReceiver">Receiver's email address</param>
            /// <param name="subject">Subject line of the email</param>
            /// <param name="message">Message content of the email</param>
            /// <param name="encoding">Payload encoding type. Choose dependent on your QR Code scanner app.</param>
            public Mail(string mailReceiver, string subject, string message, MailEncoding encoding = MailEncoding.MAILTO)
            {
                this.mailReceiver = mailReceiver;
                this.subject      = subject;
                this.message      = message;
                this.encoding     = encoding;
            }

            public override string ToString()
            {
                switch (encoding)
                {
                    case MailEncoding.MAILTO:
                        return
                            $"mailto:{mailReceiver}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(message)}";
                    case MailEncoding.MATMSG:
                        return
                            $"MATMSG:TO:{mailReceiver};SUB:{EscapeInput(subject)};BODY:{EscapeInput(message)};;";
                    case MailEncoding.SMTP:
                        return
                            $"SMTP:{mailReceiver}:{EscapeInput(subject, true)}:{EscapeInput(message, true)}";
                    default:
                        return mailReceiver;
                }
            }
        }
    }
}
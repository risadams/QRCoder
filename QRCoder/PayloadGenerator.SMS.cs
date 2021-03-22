using System;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class SMS : Payload
        {
            public enum SMSEncoding
            {
                SMS,
                SMSTO,
                SMS_iOS
            }

            private readonly SMSEncoding encoding;
            private readonly string number, subject;

            /// <summary>
            /// Creates a SMS payload without text
            /// </summary>
            /// <param name="number">Receiver phone number</param>
            /// <param name="encoding">Encoding type</param>
            public SMS(string number, SMSEncoding encoding = SMSEncoding.SMS)
            {
                this.number   = number;
                subject       = string.Empty;
                this.encoding = encoding;
            }

            /// <summary>
            /// Creates a SMS payload with text (subject)
            /// </summary>
            /// <param name="number">Receiver phone number</param>
            /// <param name="subject">Text of the SMS</param>
            /// <param name="encoding">Encoding type</param>
            public SMS(string number, string subject, SMSEncoding encoding = SMSEncoding.SMS)
            {
                this.number   = number;
                this.subject  = subject;
                this.encoding = encoding;
            }

            public override string ToString()
            {
                switch (encoding)
                {
                    case SMSEncoding.SMS:
                        return $"sms:{number}?body={Uri.EscapeDataString(subject)}";
                    case SMSEncoding.SMS_iOS:
                        return $"sms:{number};body={Uri.EscapeDataString(subject)}";
                    case SMSEncoding.SMSTO:
                        return $"SMSTO:{number}:{subject}";
                    default:
                        return "sms:";
                }
            }
        }
    }
}
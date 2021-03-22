using System;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class MMS : Payload
        {
            public enum MMSEncoding
            {
                MMS,
                MMSTO
            }

            private readonly MMSEncoding encoding;
            private readonly string number, subject;

            /// <summary>
            /// Creates a MMS payload without text
            /// </summary>
            /// <param name="number">Receiver phone number</param>
            /// <param name="encoding">Encoding type</param>
            public MMS(string number, MMSEncoding encoding = MMSEncoding.MMS)
            {
                this.number   = number;
                subject       = string.Empty;
                this.encoding = encoding;
            }

            /// <summary>
            /// Creates a MMS payload with text (subject)
            /// </summary>
            /// <param name="number">Receiver phone number</param>
            /// <param name="subject">Text of the MMS</param>
            /// <param name="encoding">Encoding type</param>
            public MMS(string number, string subject, MMSEncoding encoding = MMSEncoding.MMS)
            {
                this.number   = number;
                this.subject  = subject;
                this.encoding = encoding;
            }

            public override string ToString()
            {
                switch (encoding)
                {
                    case MMSEncoding.MMSTO:
                        return $"mmsto:{number}?subject={Uri.EscapeDataString(subject)}";
                    case MMSEncoding.MMS:
                        return $"mms:{number}?body={Uri.EscapeDataString(subject)}";
                    default:
                        return "mms:";
                }
            }
        }
    }
}
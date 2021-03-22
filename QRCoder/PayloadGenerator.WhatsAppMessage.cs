using System;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class WhatsAppMessage : Payload
        {
            private readonly string number, message;

            /// <summary>
            /// Let's you compose a WhatApp message and send it the receiver number.
            /// </summary>
            /// <param name="number">Receiver phone number</param>
            /// <param name="message">The message</param>
            public WhatsAppMessage(string number, string message)
            {
                this.number  = number;
                this.message = message;
            }

            /// <summary>
            /// Let's you compose a WhatApp message. When scanned the user is asked to choose a contact who will receive the message.
            /// </summary>
            /// <param name="message">The message</param>
            public WhatsAppMessage(string message)
            {
                number       = string.Empty;
                this.message = message;
            }

            public override string ToString() => $"whatsapp://send?phone={number}&text={Uri.EscapeDataString(message)}";
        }
    }
}
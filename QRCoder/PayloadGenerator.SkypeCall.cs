﻿namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class SkypeCall : Payload
        {
            private readonly string skypeUsername;

            /// <summary>
            /// Generates a Skype call payload
            /// </summary>
            /// <param name="skypeUsername">Skype username which will be called</param>
            public SkypeCall(string skypeUsername) => this.skypeUsername = skypeUsername;

            public override string ToString() => $"skype:{skypeUsername}?call";
        }
    }
}
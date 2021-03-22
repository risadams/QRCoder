﻿namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class PhoneNumber : Payload
        {
            private readonly string number;

            /// <summary>
            /// Generates a phone call payload
            /// </summary>
            /// <param name="number">Phonenumber of the receiver</param>
            public PhoneNumber(string number) => this.number = number;

            public override string ToString() => $"tel:{number}";
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class BitcoinLikeCryptoCurrencyAddress : Payload
        {
            public enum BitcoinLikeCryptoCurrencyType
            {
                Bitcoin,
                BitcoinCash,
                Litecoin
            }

            private readonly string address, label, message;
            private readonly double? amount;
            private readonly BitcoinLikeCryptoCurrencyType currencyType;

            /// <summary>
            /// Generates a Bitcoin like cryptocurrency payment payload. QR Codes with this payload can open a payment app.
            /// </summary>
            /// <param name="currencyName">Bitcoin like cryptocurrency address of the payment receiver</param>
            /// <param name="address">Bitcoin like cryptocurrency address of the payment receiver</param>
            /// <param name="amount">Amount of coins to transfer</param>
            /// <param name="label">Reference label</param>
            /// <param name="message">Referece text aka message</param>
            public BitcoinLikeCryptoCurrencyAddress(BitcoinLikeCryptoCurrencyType currencyType, string address, double? amount, string label = null, string message = null)
            {
                this.currencyType = currencyType;
                this.address      = address;

                if (!string.IsNullOrEmpty(label)) this.label = Uri.EscapeUriString(label);

                if (!string.IsNullOrEmpty(message)) this.message = Uri.EscapeUriString(message);

                this.amount = amount;
            }

            public override string ToString()
            {
                string query = null;

                var queryValues = new[]
                {
                    new KeyValuePair<string, string>(nameof(label), label),
                    new KeyValuePair<string, string>(nameof(message), message),
                    new KeyValuePair<string, string>(nameof(amount), amount.HasValue ? amount.Value.ToString("#.########", CultureInfo.InvariantCulture) : null)
                };

                if (queryValues.Any(keyPair => !string.IsNullOrEmpty(keyPair.Value)))
                    query = "?" + string.Join("&", queryValues
                                                   .Where(keyPair => !string.IsNullOrEmpty(keyPair.Value))
                                                   .Select(keyPair => $"{keyPair.Key}={keyPair.Value}")
                                                   .ToArray());

                return $"{Enum.GetName(typeof(BitcoinLikeCryptoCurrencyType), currencyType).ToLower()}:{address}{query}";
            }
        }
    }
}
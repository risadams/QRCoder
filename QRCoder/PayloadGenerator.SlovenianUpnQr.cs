using System;
using System.Text;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class SlovenianUpnQr : Payload
        {
            private readonly string _amount = "";
            private readonly string _code = "";
            private readonly string _deadLine = "";

            private readonly string _payerAddress = "";
            //Keep in mind, that the ECC level has to be set to "M", version to 15 and ECI to EciMode.Iso8859_2 when generating a SlovenianUpnQr!
            //SlovenianUpnQr specification: https://www.upn-qr.si/uploads/files/NavodilaZaProgramerjeUPNQR.pdf

            private readonly string _payerName = "";
            private readonly string _payerPlace = "";
            private readonly string _purpose = "";
            private readonly string _recipientAddress = "";
            private readonly string _recipientIban = "";
            private readonly string _recipientName = "";
            private readonly string _recipientPlace = "";
            private readonly string _recipientSiModel = "";
            private readonly string _recipientSiReference = "";

            public SlovenianUpnQr(string payerName, string payerAddress, string payerPlace, string recipientName, string recipientAddress, string recipientPlace, string recipientIban, string description, double amount, string recipientSiModel = "SI00", string recipientSiReference = "", string code = "OTHR") :
                this(payerName, payerAddress, payerPlace, recipientName, recipientAddress, recipientPlace, recipientIban, description, amount, null, recipientSiModel, recipientSiReference, code) { }

            public SlovenianUpnQr(string payerName, string payerAddress, string payerPlace, string recipientName, string recipientAddress, string recipientPlace, string recipientIban, string description, double amount, DateTime? deadline, string recipientSiModel = "SI99", string recipientSiReference = "", string code = "OTHR")
            {
                _payerName            = LimitLength(payerName.Trim(), 33);
                _payerAddress         = LimitLength(payerAddress.Trim(), 33);
                _payerPlace           = LimitLength(payerPlace.Trim(), 33);
                _amount               = FormatAmount(amount);
                _code                 = LimitLength(code.Trim().ToUpper(), 4);
                _purpose              = LimitLength(description.Trim(), 42);
                _deadLine             = deadline == null ? "" : deadline?.ToString("dd.MM.yyyy");
                _recipientIban        = LimitLength(recipientIban.Trim(), 34);
                _recipientName        = LimitLength(recipientName.Trim(), 33);
                _recipientAddress     = LimitLength(recipientAddress.Trim(), 33);
                _recipientPlace       = LimitLength(recipientPlace.Trim(), 33);
                _recipientSiModel     = LimitLength(recipientSiModel.Trim().ToUpper(), 4);
                _recipientSiReference = LimitLength(recipientSiReference.Trim(), 22);
            }

            public override int Version => 15;
            public override QRCodeGenerator.ECCLevel EccLevel => QRCodeGenerator.ECCLevel.M;
            public override QRCodeGenerator.EciMode EciMode => QRCodeGenerator.EciMode.Iso8859_2;

            private string LimitLength(string value, int maxLength) => value.Length <= maxLength ? value : value.Substring(0, maxLength);

            private string FormatAmount(double amount)
            {
                var _amt = (int) Math.Round(amount * 100.0);
                return string.Format("{0:00000000000}", _amt);
            }

            private int CalculateChecksum()
            {
                var _cs = 5 + _payerName.Length; //5 = UPNQR constant Length
                _cs += _payerAddress.Length;
                _cs += _payerPlace.Length;
                _cs += _amount.Length;
                _cs += _code.Length;
                _cs += _purpose.Length;
                _cs += _deadLine.Length;
                _cs += _recipientIban.Length;
                _cs += _recipientName.Length;
                _cs += _recipientAddress.Length;
                _cs += _recipientPlace.Length;
                _cs += _recipientSiModel.Length;
                _cs += _recipientSiReference.Length;
                _cs += 19;
                return _cs;
            }

            public override string ToString()
            {
                var _sb = new StringBuilder();
                _sb.Append("UPNQR");
                _sb.Append('\n').Append('\n').Append('\n').Append('\n').Append('\n');
                _sb.Append(_payerName).Append('\n');
                _sb.Append(_payerAddress).Append('\n');
                _sb.Append(_payerPlace).Append('\n');
                _sb.Append(_amount).Append('\n').Append('\n').Append('\n');
                _sb.Append(_code.ToUpper()).Append('\n');
                _sb.Append(_purpose).Append('\n');
                _sb.Append(_deadLine).Append('\n');
                _sb.Append(_recipientIban.ToUpper()).Append('\n');
                _sb.Append(_recipientSiModel).Append(_recipientSiReference).Append('\n');
                _sb.Append(_recipientName).Append('\n');
                _sb.Append(_recipientAddress).Append('\n');
                _sb.Append(_recipientPlace).Append('\n');
                _sb.AppendFormat("{0:000}", CalculateChecksum()).Append('\n');
                return _sb.ToString();
            }
        }
    }
}
namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class WiFi : Payload
        {
            public enum Authentication
            {
                WEP,
                WPA,
                nopass
            }

            private readonly bool isHiddenSsid;
            private readonly string ssid, password, authenticationMode;

            /// <summary>
            /// Generates a WiFi payload. Scanned by a QR Code scanner app, the device will connect to the WiFi.
            /// </summary>
            /// <param name="ssid">SSID of the WiFi network</param>
            /// <param name="password">Password of the WiFi network</param>
            /// <param name="authenticationMode">Authentification mode (WEP, WPA, WPA2)</param>
            /// <param name="isHiddenSSID">Set flag, if the WiFi network hides its SSID</param>
            public WiFi(string ssid, string password, Authentication authenticationMode, bool isHiddenSSID = false)
            {
                this.ssid               = EscapeInput(ssid);
                this.ssid               = isHexStyle(this.ssid) ? "\"" + this.ssid + "\"" : this.ssid;
                this.password           = EscapeInput(password);
                this.password           = isHexStyle(this.password) ? "\"" + this.password + "\"" : this.password;
                this.authenticationMode = authenticationMode.ToString();
                isHiddenSsid            = isHiddenSSID;
            }

            public override string ToString() => $"WIFI:T:{authenticationMode};S:{ssid};P:{password};{(isHiddenSsid ? "H:true" : string.Empty)};";
        }
    }
}
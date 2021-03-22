namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class Geolocation : Payload
        {
            public enum GeolocationEncoding
            {
                GEO,
                GoogleMaps
            }

            private readonly GeolocationEncoding encoding;
            private readonly string latitude, longitude;

            /// <summary>
            /// Generates a geo location payload. Supports raw location (GEO encoding) or Google Maps link (GoogleMaps encoding)
            /// </summary>
            /// <param name="latitude">Latitude with . as splitter</param>
            /// <param name="longitude">Longitude with . as splitter</param>
            /// <param name="encoding">Encoding type - GEO or GoogleMaps</param>
            public Geolocation(string latitude, string longitude, GeolocationEncoding encoding = GeolocationEncoding.GEO)
            {
                this.latitude  = latitude.Replace(",", ".");
                this.longitude = longitude.Replace(",", ".");
                this.encoding  = encoding;
            }

            public override string ToString()
            {
                switch (encoding)
                {
                    case GeolocationEncoding.GEO:
                        return $"geo:{latitude},{longitude}";
                    case GeolocationEncoding.GoogleMaps:
                        return $"http://maps.google.com/maps?q={latitude},{longitude}";
                    default:
                        return "geo:";
                }
            }
        }
    }
}
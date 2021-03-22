namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class Url : Payload
        {
            private readonly string url;

            /// <summary>
            /// Generates a link. If not given, http/https protocol will be added.
            /// </summary>
            /// <param name="url">Link url target</param>
            public Url(string url) => this.url = url;

            public override string ToString() => !url.StartsWith("http") ? "http://" + url : url;
        }
    }
}
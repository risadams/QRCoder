﻿namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class Bookmark : Payload
        {
            private readonly string url, title;

            /// <summary>
            /// Generates a bookmark payload. Scanned by an QR Code reader, this one creates a browser bookmark.
            /// </summary>
            /// <param name="url">Url of the bookmark</param>
            /// <param name="title">Title of the bookmark</param>
            public Bookmark(string url, string title)
            {
                this.url   = EscapeInput(url);
                this.title = EscapeInput(title);
            }

            public override string ToString() => $"MEBKM:TITLE:{title};URL:{url};;";
        }
    }
}
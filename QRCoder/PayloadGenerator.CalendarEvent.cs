using System;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class CalendarEvent : Payload
        {
            public enum EventEncoding
            {
                iCalComplete,
                Universal
            }

            private readonly EventEncoding encoding;
            private readonly string subject, description, location, start, end;

            /// <summary>
            /// Generates a calender entry/event payload.
            /// </summary>
            /// <param name="subject">Subject/title of the calender event</param>
            /// <param name="description">Description of the event</param>
            /// <param name="location">Location (lat:long or address) of the event</param>
            /// <param name="start">Start time of the event</param>
            /// <param name="end">End time of the event</param>
            /// <param name="allDayEvent">Is it a full day event?</param>
            /// <param name="encoding">Type of encoding (universal or iCal)</param>
            public CalendarEvent(string subject, string description, string location, DateTime start, DateTime end, bool allDayEvent, EventEncoding encoding = EventEncoding.Universal)
            {
                this.subject     = subject;
                this.description = description;
                this.location    = location;
                this.encoding    = encoding;
                var dtFormat = allDayEvent ? "yyyyMMdd" : "yyyyMMddTHHmmss";
                this.start = start.ToString(dtFormat);
                this.end   = end.ToString(dtFormat);
            }

            public override string ToString()
            {
                var vEvent = $"BEGIN:VEVENT{Environment.NewLine}";
                vEvent += $"SUMMARY:{subject}{Environment.NewLine}";
                vEvent += !string.IsNullOrEmpty(description) ? $"DESCRIPTION:{description}{Environment.NewLine}" : "";
                vEvent += !string.IsNullOrEmpty(location) ? $"LOCATION:{location}{Environment.NewLine}" : "";
                vEvent += $"DTSTART:{start}{Environment.NewLine}";
                vEvent += $"DTEND:{end}{Environment.NewLine}";
                vEvent += "END:VEVENT";

                if (encoding == EventEncoding.iCalComplete)
                    vEvent = $@"BEGIN:VCALENDAR{Environment.NewLine}VERSION:2.0{Environment.NewLine}{vEvent}{Environment.NewLine}END:VCALENDAR";

                return vEvent;
            }
        }
    }
}
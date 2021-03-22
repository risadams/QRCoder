using System;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        public class IcsEvent : Payload
        {
            public enum EventEncoding
            {
                iCalComplete,
                Universal
            }

            private readonly EventEncoding encoding;
            private readonly CalendarEvent internalEvent;

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
            public IcsEvent(string subject, string description, string location, DateTime start, DateTime end, bool allDayEvent, EventEncoding encoding = EventEncoding.Universal)
            {
                internalEvent             = new CalendarEvent();
                internalEvent.Summary     = subject;
                internalEvent.Description = description;
                internalEvent.Location    = location;
                this.encoding             = encoding;
                internalEvent.DtStart     = new CalDateTime(start);
                internalEvent.DtEnd       = new CalDateTime(end);
            }

            public IcsEvent(CalendarEvent calendarEvent, EventEncoding encoding = EventEncoding.Universal)
            {
                var cal = new Calendar();
                cal.Events.Add(new CalendarEvent());
            }

            public override string ToString()
            {
                var cal = new Calendar();
                cal.Events.Add(new CalendarEvent());
                var serializer         = new CalendarSerializer();
                var serializedCalendar = serializer.SerializeToString(cal);
                var vEvent             = serializedCalendar;

                return vEvent;
            }
        }
    }
}
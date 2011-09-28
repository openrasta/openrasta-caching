using System;

namespace OpenRasta.Caching
{
    public static class ServerClock
    {
        public static Func<DateTimeOffset> UtcNowDefinition = ()=> DateTimeOffset.UtcNow;
        public static DateTimeOffset UtcNow()
        {
            return UtcNowDefinition();
        }

    }
}
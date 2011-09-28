using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests
{
    public class extensions
    {
        [Test]
        public void before_date()
        {
            var now = DateTimeOffset.Now;
            now.Before(now + 2.Hours()).ShouldBeTrue();
        }
        [Test]
        public void after_date()
        {
            var now = DateTimeOffset.Now;
            now.After(now - 2.Hours()).ShouldBeTrue();
        }
    }
}
using Xunit;

namespace ISOOU.Services.Data.Tests.Common
{
    public static class AssertExtensions
    {
        public static void EqualWithMessage(object first, object second, string message)
        {
            Assert.True(first == second, message);
        }
    }
}

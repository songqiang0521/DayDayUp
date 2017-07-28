using System.Text;
using Xunit;

namespace Basics.Tests.Math.Test
{
    public class CRC32Test
    {
        [Fact]
        public void Managed_VS_Native()
        {
            var bytes = Encoding.UTF8.GetBytes("Basics.Tests.Math.Test.Managed_VS_Native");

            uint crcNative=Basics.Math.CRC32.GetCrc32(0, bytes, 0, bytes.Length);

            uint crcManaged = Basics.Math.CRC32.ManagedCrc32(0, bytes, 0, bytes.Length);

            Assert.Equal(crcManaged,crcNative);
        }

    }
}

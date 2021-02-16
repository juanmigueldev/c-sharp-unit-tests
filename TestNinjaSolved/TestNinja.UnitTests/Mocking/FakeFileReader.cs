using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}

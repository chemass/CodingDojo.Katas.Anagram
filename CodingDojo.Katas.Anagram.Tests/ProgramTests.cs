namespace CodingDojo.Katas.Anagram.Tests
{
    [TestClass]
    public sealed class ProgramTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> anagrams = Program.GenerateTwoWordAnagrams("documenting").ToList();

        }
    }
}

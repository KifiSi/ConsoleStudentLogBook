using ChallengeAppConsole;

namespace ChallengeAppConsoleTests
{
    public class KidTests
    {
        [Fact]
        public void Test1()
        {
            // arrange - przygotowanie
            var kid = new KidInMemory("Tomek");
            kid.AddGrades("3+", kid.Name);
            kid.AddGrades("4.7", kid.Name);
            kid.AddGrades("5", kid.Name);

            // act - test
            var result = kid.GetStatistics(kid.Name);

            // assert - sprawdzenie
            Assert.Equal(3.5, result.Low);
            Assert.Equal(5, result.High);
            Assert.Equal(4.4, result.Average, 1);
        }
    }
}
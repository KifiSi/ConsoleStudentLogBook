using ChallengeAppConsole;

namespace ChallengeAppConsoleTests
{
    public class KidTests
    {
        [Fact]
        public void CanAddGradesAndCheckStatistics()
        {
            // arrange - przygotowanie
            var kid = new KidInMemory("Tomek");
            kid.AddGrade("3+");
            kid.AddGrade("4.7");
            kid.AddGrade("5");

            // act - test
            var result = kid.GetStatistics();

            // assert - sprawdzenie
            Assert.Equal(3.5, result.Low);
            Assert.Equal(5, result.High);
            Assert.Equal(4.4, result.Average, 1);
        }
    }
}
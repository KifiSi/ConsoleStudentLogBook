using System;
using ChallengeAppConsole;

namespace ChallengeAppConsoleTests
{
    public class TypeTests
    {
        [Fact]
        public void GetKidReturnsDifrrentObjetcs()
        {
            // arrange - przygotowanie
            var kid1 = GetKid("Tomek");
            var kid2 = GetKid("Ania");

            // act - test


            // assert - sprawdzenie
            Assert.Equal("Tomek", kid1.Name);
            Assert.Equal("Ania", kid2.Name);
            Assert.NotSame(kid1, kid2);
            Assert.False(Object.ReferenceEquals(kid1, kid2));
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var kid = GetKid("Ola");
            kid.ChangeName("Paulina");
            Assert.Equal("Paulina", kid.Name);
        }

        private KidInMemory GetKid(string name)
        {
            return new KidInMemory(name);
        }

    }
}
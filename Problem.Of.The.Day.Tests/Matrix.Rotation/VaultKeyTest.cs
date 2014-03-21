using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem.Of.The.Day.Matrix.Rotation;
using System.Collections.Generic;

namespace Problem.Of.The.Day.Tests.Matrix.Rotation
{
    [TestClass]
    public class VaultKeyTest
    {
        private static IEnumerable<DayOfWeek> WeekDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        private static IEnumerable<DayOfWeek> WeekendDays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsForNullInput()
        {
            var key = new VaultKey();
            key.GetCode(null, DayOfWeek.Monday);

            var action = new Action<DayOfWeek>(day => key.GetCode(new int[][] { null }, day));
            
            AssertFor(action, WeekDays);
            AssertFor(action, WeekendDays);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsForEmptyInput()
        {
            var key = new VaultKey();
            
            var action = new Action<DayOfWeek>(day => key.GetCode(new int[0][], day));

            AssertFor(action, WeekDays);
            AssertFor(action, WeekendDays);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsForInvalidInput()
        {
            var input = new int[][] { new[] { 1, 2 }, new[] { 2 } };
            var key = new VaultKey();

            var action = new Action<DayOfWeek>(day => key.GetCode(input, day));

            AssertFor(action, WeekDays);
            AssertFor(action, WeekendDays);
        
        }

        [TestMethod]
        public void ReturnsForSingleValue()
        {
            var input = new int[][] { new[] { 1 } };
            var key = new VaultKey();

            var action = new Action<DayOfWeek>(day =>
                {
                    var result = key.GetCode(input, day);
                    AreEquivalent(result, input);
                });

            AssertFor(action, WeekDays);
            AssertFor(action, WeekendDays);
        }

        [TestMethod]
        public void ReturnsForWeekDays()
        {
            var input = new int[][] { new[] { 1, 2 }
                                    , new[] { 3, 4 } };
            var key = new VaultKey();

            var action = new Action<DayOfWeek>(day =>
            {
                var result = key.GetCode(input, day);
                Assert.IsTrue(AreEquivalent(result, new int[][]{ new[] { 3, 1 }
                                                               , new[] { 4, 2 }}));
                
            });

            AssertFor(action, WeekDays);
        }

        [TestMethod]
        public void ReturnsForWeekendDays()
        {
            var input = new int[][] { new[] { 1, 2 }
                                    , new[] { 3, 4 } };
            var key = new VaultKey();

            var action = new Action<DayOfWeek>(day =>
            {
                var result = key.GetCode(input, day);
                Assert.IsTrue(AreEquivalent(result, new int[][]{ new[] { 2, 4 }
                                                               , new[] { 1, 3 }}));
            });

            AssertFor(action, WeekendDays);
        }

        [TestMethod]
        public void DoesNotModifyInput()
        {
            var input = new int[][] { new[] { 1, 2 }
                                    , new[] { 3, 4 } };
            var key = new VaultKey();

            var action = new Action<DayOfWeek>(day =>
            {
                var result = key.GetCode(input, day);
                Assert.IsFalse(AreEquivalent(input, result));
            });

            AssertFor(action, WeekDays);
            AssertFor(action, WeekendDays);
        }
        

        private static void AssertFor(Action<DayOfWeek> action, IEnumerable<DayOfWeek> days)
        {
            foreach (var day in days)
            {
                action(day);
            }
        }

        private static bool AreEquivalent(int[][] first, int[][] second)
        {
            int n = first.Length;

            for (int row = 0; row < n; row++)
            {
                if (n != second[row].Length)
                    return false;

                for (int column = 0; column < n; column++)
                {
                    if (first[row][column] != second[row][column])
                        return false;
                }
            }

            return true;
        }
    }
}


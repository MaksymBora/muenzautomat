using FluentAssertions;

using muenzautomat;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    public class CalculatorTests
    {
        #region Test data

        public static IEnumerable<object[]> Money => new List<object[]>
        {
            new object[] { 3, 15, 
                new Dictionary<Coin, int> {
                    { Coin.Euro2, 1 },
                    { Coin.Euro1, 1 },
                    { Coin.Cent10, 1 },
                    { Coin.Cent5, 1 }
                }
            },
            new object[] { 15, 37,
                new Dictionary<Coin, int> {
                    { Coin.Euro2, 7 },
                    { Coin.Euro1, 1 },
                    { Coin.Cent20, 1 },
                    { Coin.Cent10, 1 },
                    { Coin.Cent5, 1 },
                    { Coin.Cent1, 2 }
                }
            },
            new object[] { 99, 99,
                new Dictionary<Coin, int> {
                    { Coin.Euro2, 49 },
                    { Coin.Euro1, 1 },
                    { Coin.Cent20, 4 },
                    { Coin.Cent10, 1 },
                    { Coin.Cent5, 1 },
                    { Coin.Cent1, 4 },
                }
            }
        };

        private IChangeCalculator GetInstance() => 
            ChangeCalculator.GetInsance();

        #endregion

        #region Tests

        [Theory]
        [MemberData(nameof(Money))]
        public void Get_coins_from_normal_money(int euros, int cent, Dictionary<Coin, int> expected)
        {
            // Arrange
            IChangeCalculator calculator = GetInstance();

            // Act
            var result = calculator.GetChange(euros, cent);

            // Assert
            result
                .Should()
                .IntersectWith(expected);
        }

        [Fact]
        public void Get_coins_from_zero_money()
        {
            // Arrange
            IChangeCalculator calculator = GetInstance();

            // Act
            var result = calculator.GetChange(0, 0);

            // Assert
            result.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(-1, 20)]
        [InlineData(2, -30)]
        [InlineData(-3, -40)]
        public void Get_coins_if_invalid_money_passed(int euros, int cent)
        {
            // Arrange
            IChangeCalculator calculator = GetInstance();

            // Act
            var act = () => calculator.GetChange(euros, cent);

            // Assert
            act.Should().Throw<Exception>();
        }

        #endregion
    }
}

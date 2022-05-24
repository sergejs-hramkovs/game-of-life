using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Models;

namespace GameOfLife.Tests
{
    [TestClass]
    public class GameFieldModelTest
    {
        [TestMethod]
        public void TestGameFieldCreation()
        {
            GameFieldModel gameField = new(10, 8);
            bool checkDeadCellError = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameField.GameField[i, j] != StringConstants.DeadCellSymbol)
                    {
                        checkDeadCellError = true;
                    }
                }
            }

            Assert.AreEqual(10, gameField.Length);
            Assert.AreEqual(8, gameField.Width);
            Assert.AreEqual(10 * 8, gameField.Area);
            Assert.AreEqual(false, checkDeadCellError);
            Assert.AreEqual(10 * 8, gameField.DeadCellsNumber);
        }
    }
}
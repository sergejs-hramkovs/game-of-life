using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Models;
using GameOfLife.Interfaces;
using System;

namespace GameOfLife.Tests
{
    [TestClass]
    public class InputControllerTest
    {
        [TestMethod]
        [DataRow(ConsoleKey.D1, 3, 3)]
        [DataRow(ConsoleKey.D2, 5, 5)]
        [DataRow(ConsoleKey.D3, 10, 10)]
        [DataRow(ConsoleKey.D4, 20, 20)]
        [DataRow(ConsoleKey.D5, 75, 40)]
        public void TestCheckInputMainMenu_PremadeInput(ConsoleKey keyPressed, int length, int width)
        {
            InputController inputController = new();
            GameFieldModel gameField;

            gameField = inputController.CheckInputMainMenu(keyPressed);

            Assert.AreEqual(gameField.Length, length);
            Assert.AreEqual(gameField.Width, width);
        }

        [TestMethod]
        [DataRow(ConsoleKey.D1, 40, 30)]
        [DataRow(ConsoleKey.D2, 37, 40)]
        public void TestCheckGliderGunMenu_D1D2Input(ConsoleKey keyPressed, int length, int width)
        {
            Engine engine = new();
            InputController inputController = new();
            inputController.Injection(engine);
            GameFieldModel gameField;

            gameField = inputController.CheckInputGliderGunMenu(keyPressed);

            Assert.AreEqual(gameField.Length, length);
            Assert.AreEqual(gameField.Width, width);
        }

        [TestMethod]
        [DataRow(ConsoleKey.LeftArrow, 1000, 900)]
        [DataRow(ConsoleKey.LeftArrow, 100, 90)]
        [DataRow(ConsoleKey.LeftArrow, 10, 10)]
        [DataRow(ConsoleKey.RightArrow, 700, 800)]
        [DataRow(ConsoleKey.RightArrow, 900, 1000)]
        [DataRow(ConsoleKey.RightArrow, 2000, 2000)]
        public void TestChangeDelay(ConsoleKey keyPressed, int initialDelay, int newDelay)
        {
            Engine engine = new();
            InputController inputController = new();
            inputController.Injection(engine);
            engine.Delay = initialDelay;

            inputController.ChangeDelay(keyPressed);

            Assert.AreEqual(newDelay, engine.Delay);
        }
    }
}
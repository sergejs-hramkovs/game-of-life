﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameOfLife.Tests
{
    [TestClass]
    public class InputControllerTest
    {
        [TestMethod]
        [DataRow(ConsoleKey.LeftArrow, 1000, 900)]
        [DataRow(ConsoleKey.LeftArrow, 100, 90)]
        [DataRow(ConsoleKey.LeftArrow, 10, 10)]
        [DataRow(ConsoleKey.RightArrow, 700, 800)]
        [DataRow(ConsoleKey.RightArrow, 900, 1000)]
        [DataRow(ConsoleKey.RightArrow, 2000, 2000)]
        public void TestChangeDelay(ConsoleKey keyPressed, int initialDelay, int newDelay)
        {
            MainEngine engine = new();
            InputController inputController = new();
            inputController.Inject(engine);
            engine.Delay = initialDelay;

            inputController.ChangeDelay(keyPressed);

            Assert.AreEqual(newDelay, engine.Delay);
        }
    }
}
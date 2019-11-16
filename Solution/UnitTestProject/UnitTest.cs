using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solution;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMap1()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map1.txt");
            const string expectedPath = "@---A---+|C|+---+|+-B-x";
            string expectedLetters = "ACB";

            string actualPath = null, actualLetters = null;

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver backtracingSolver = new Solution.Solver();

            asciiMap.Solve(backtracingSolver, (solvedPath) =>  
            {
                actualPath = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                actualLetters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
            });
            Assert.AreEqual(expectedLetters, actualLetters);
            Assert.AreEqual(expectedPath, actualPath);
        }
        [TestMethod]
        public void TestMap2()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map2.txt");
            const string expectedPath = "@|A+---B--+|+----C|-||+---D--+|x";
            string expectedLetters = "ABCD";
            string actualPath = null, actualLetters = null;

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver backtracingSolver = new Solution.Solver();

            asciiMap.Solve(backtracingSolver, (solvedPath) => 
            {
                actualPath = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                actualLetters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
            });
            Assert.AreEqual(expectedLetters, actualLetters);
            Assert.AreEqual(expectedPath, actualPath);
        }
        [TestMethod]
        public void TestMap3()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map3.txt");
            const string expectedPath = "@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex";
            string expectedLetters = "BEEFCAKE";
            string actualPath = null, actualLetters = null;

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            asciiMap.Solve(tracingSolver, (solvedPath) =>
            {
                actualPath = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                actualLetters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
            });
            Assert.AreEqual(expectedPath, actualPath);
            Assert.AreEqual(expectedLetters, actualLetters);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Ascii map is invalid")]
        public void TestMap4()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map4.txt");
            string actualPath = null, actualLetters = null;

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) => 
            {
                actualPath = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                actualLetters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
            }));
        }
        [TestMethod]
        //[ExpectedException(typeof(Exception), "Ascii map is invalid")]
        public void TestMap5()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map5.txt");
            string actualPath = null, actualLetters = null;

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) => 
            {
                actualPath = String.Join("", solvedPath.Path.Select(t => t.Content).ToArray());
                actualLetters = String.Join("", solvedPath.Letters.Select(x => x).ToArray());
            }));
        }
    }
 }


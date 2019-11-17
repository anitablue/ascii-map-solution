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
        /// <summary>
        /// Test Map1.txt 
        /// expectedPath => @---A---+|C|+---+|+-B-x
        /// expectedLetters => ACB
        /// </summary>
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
        /// <summary>
        /// Test Map2.txt
        /// /// expectedPath => @|A+---B--+|+----C|-||+---D--+|x
        /// expectedLetters => ABCD
        /// </summary>
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
        /// <summary>
        /// Test Map3.txt
        /// /// expectedPath => @---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex
        /// expectedLetters => BEEFCAKE
        /// </summary>
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
        /// <summary>
        /// Test Map4.txt
        /// Exception is excpected
        /// Ascii map is not valid - start node does not have neighbours
        /// </summary>
        [TestMethod]
        public void TestMap4()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map4.txt");

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) => 
            {
                Results results = solvedPath;
            }));
        }
        /// <summary>
        /// Test Map5.txt
        /// Exception is excpected
        /// Ascii map is not valid - plus sign has more than one neightbour
        /// </summary>
        [TestMethod]
        public void TestMap5()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map5.txt");

            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) => 
            {
                Results results = solvedPath;
            }));
        }
        /// Test Map6.txt
        /// Exception is excpected
        /// Ascii map is not valid - horizontaln sign is on wrong position 
        /// </summary>
        [TestMethod]
        public void TestMap6()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map6.txt");


            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) =>
            {
                Results results = solvedPath;
            }));
        }
        /// Test Map7.txt
        /// Exception is excpected
        /// Ascii map is not valid - E node is in dead end position 
        /// </summary>
        [TestMethod]
        public void TestMap7()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map7.txt");


            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) =>
            {
                Results results = solvedPath;
            }));
        }
        /// Test map8.txt
        /// Exception is excpected
        /// Ascii map is not valid - finish node is missing 
        /// </summary>
        [TestMethod]
        public void TestMap8()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map8.txt");


            IAsciiMap asciiMap = new AsciiMap(filePath);
            ISolver tracingSolver = new Solution.Solver();

            MyAssert.MyAssert.Throws<Exception>(() => asciiMap.Solve(tracingSolver, (solvedPath) =>
            {
                Results results = solvedPath;
            }));
        }
        /// <summary>
        /// Test Map9.txt
        /// /// expectedPath => @-+|A+-B-+|Cx
        /// expectedLetters => ABC
        /// </summary>
        [TestMethod]
        public void TestMap9()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map9.txt");
            const string expectedPath = "@-+|A+-B-+|Cx";
            string expectedLetters = "ABC";
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
        /// <summary>
        /// Test Map10.txt
        /// /// expectedPath => @-+|A+-B----C--+|+--D--+|-|+--E--x
        /// expectedLetters => ABCDE
        /// </summary>
        [TestMethod]
        public void TestMap10()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(baseDir, "Maps", "Map10.txt");
            const string expectedPath = "@-+|A+-B----C--+|+--D--+|-|+--E--x";
            string expectedLetters = "ABCDE";
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
    }
 }


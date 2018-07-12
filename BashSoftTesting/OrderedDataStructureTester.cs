using BashSoft.Contracts;
using BashSoft.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoftTesting
{
    [TestFixture]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialParameter()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Nasko");
            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.That(() => this.names.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            string[] expectedResult = { "Balkan", "Georgi", "Rosen" };
            int index = 0;

            foreach (string name in this.names)
            {
                if (name != expectedResult[index])
                {
                    Assert.Fail();
                }

                index++;
            }

            Assert.Pass();
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");
            this.names.Add("Rosen");
            this.names.Add("Georgi");

            Assert.AreEqual(17, this.names.Size);
            Assert.AreNotEqual(16, this.names.Capacity);
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            List<string> inputStrings = new List<string>() { "asas", "grgr" };

            this.names.AddAll(inputStrings);
            Assert.AreEqual(2, this.names.Size);
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            Assert.That(() => this.names.AddAll(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            List<string> inputStrings = new List<string>() { "asas", "grgr", "bafa", "jaja" };
            string[] expectedResult = { "asas", "bafa", "grgr", "jaja" };
            int index = 0;

            foreach (string name in this.names)
            {
                if (name != expectedResult[index])
                {
                    Assert.Fail();
                }

                index++;
            }

            Assert.Pass();
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add("pesho");
            this.names.Remove("pesho");

            Assert.AreEqual(0, this.names.Size);
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names.Add("pesho");
            this.names.Add("nasko");

            this.names.Remove("pesho");

            Assert.That(this.names.First(), Is.Not.EqualTo("pesho"));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            Assert.That(() => this.names.Remove(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names.Add("pesho");
            this.names.Add("nasko");

            Assert.That(() => this.names.JoinWith(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWorksFine()
        {
            const string expectedResult = "gosho, nasko, pesho";

            this.names.Add("pesho");
            this.names.Add("nasko");
            this.names.Add("gosho");

            Assert.AreEqual(expectedResult, this.names.JoinWith(", "));
        }
    }
}

using Backuper;
using FakeItEasy;
using NUnit.Framework;

namespace UnitTests
{
    public class BackupTests
    {
        [Test]
        [TestCase(@"c:\1.txt", ExpectedResult = 10927976802910018380ul)]
        [TestCase(@"c:\2\3.txt", ExpectedResult = 16982997801808016065ul)]
        public ulong Work_ForGivenMockedGetFilesInput_EntryLashHashIsExpectedResult(string getFiles)
        {
            var entry = new Entry { MonitorPath = @"c:\" };
            var zipOperationsProvider = A.Fake<IZipOperationsProvider>();
            var fileOperationsProvider = A.Fake<IFileOperationsProvider>();
            A.CallTo(() => fileOperationsProvider.GetFiles(A<string>._)).Returns(new[] { getFiles });
            new Backup(entry, fileOperationsProvider, zipOperationsProvider).Work();
            return entry.LastHash;
        }

        [Test]
        public void Work_SameHash_NoBackupOccurs()
        {
            var entry = new Entry
            {
                MonitorPath = @"c:\",
                LastHash = 16937313501295102272ul,
            };
            var zipOperationsProvider = A.Fake<IZipOperationsProvider>();
            var fileOperationsProvider = A.Fake<IFileOperationsProvider>();
            A.CallTo(() => fileOperationsProvider.GetFiles(A<string>._)).Returns(new[] { "bla" });
            A.CallTo(() => zipOperationsProvider.Create(A<string>._)).MustNotHaveHappened();
            new Backup(entry, fileOperationsProvider, zipOperationsProvider).Work();
        }

        [Test]
        [TestCase("", ExpectedResult = 4414384243935633412ul)]
        [TestCase("1.txt", ExpectedResult = 16982997801808016065ul)]
        [TestCase("2", ExpectedResult = 10927976802910018380ul)]
        [TestCase("3.txt", ExpectedResult = 4414384243935633412ul)] // same result as for ""
        [TestCase(@"2\3.txt", ExpectedResult = 10927976802910018380ul)] // same result as for "2"
        public ulong Work_ForGivenEntryIgnore_ExpectedResultMatch(string ignore)
        {
            var entry = new Entry
            {
                MonitorPath = @"c:\",
                Ignore = ignore,
            };
            var zipOperationsProvider = A.Fake<IZipOperationsProvider>();
            var fileOperationsProvider = A.Fake<IFileOperationsProvider>();
            A.CallTo(() => fileOperationsProvider.GetFiles(A<string>._)).Returns(new[] { @"c:\1.txt", @"c:\2\3.txt" });
            new Backup(entry, fileOperationsProvider, zipOperationsProvider).Work();
            return entry.LastHash;
        }
    }
}
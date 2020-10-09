// ReSharper disable StringLiteralTypo

namespace AsciiArt.Tests.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;

    using AsciiArt.Core.Service;
    using AsciiArt.Core.ViewModel;
    
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class AsciiArtViewModelTests
    {
        private readonly AsciiArtViewModel _testee;
        private readonly IPlatformSupport _fakePlatformSupport;

        public AsciiArtViewModelTests()
        {
            _fakePlatformSupport = A.Fake<IPlatformSupport>();

            _testee = new AsciiArtViewModel(_fakePlatformSupport);
        }

        [Fact]
        public void Constructor_InitializesProperties()
        {
            // Assert
            _testee.MinimumFontSize.Should().Be(6);
            _testee.MaximumFontSize.Should().Be(48);
            _testee.FontSize.Should().Be(12);
            _testee.Input.Should().BeEmpty();
            _testee.Result.Should().BeEmpty();
            _testee.IsCalculating.Should().BeFalse();
            _testee.CreateAsciiCommand.Should().NotBeNull();
        }

        [Fact]
        public void CreateAsciiCommand_WhenExecuted_WhenInputIsEmpty_ShowsError()
        {
            // Act
            _testee.CreateAsciiCommand.Execute(null);

            // Assert
            A.CallTo(() => _fakePlatformSupport.ShowError(
                "Eingabe fehlt",
                "Kann leider nichts berechnen: Keine Eingabe vorhanden")
            ).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("Hallo Welt", "HelloWorld.txt")]
        [InlineData("Das ist ein langer Text", "LongText.txt")]
        public void CreateAsciiCommand_WhenExecuted_WhenInputIsValid_UpdatesResult(string input, string resultFile)
        {
            // Arrange
            var testDataDirectory = Path.Combine(Environment.CurrentDirectory, "TestData");
            var outputFilePath = Path.Combine(testDataDirectory, resultFile);

            _testee.Input = input;

            // Act
            var waiter = CreatePropertyChangedWaiter(nameof(_testee.Result));
            _testee.CreateAsciiCommand.Execute(null);
            waiter.Wait();

            // Assert
            _testee.Result.Should().NotBeEmpty();
            _testee.Result.Should().Be(File.ReadAllText(outputFilePath));
        }

        #region Private Methods

        private SemaphoreSlim CreatePropertyChangedWaiter(string propertyName)
        {
            var semaphoreSlim = new SemaphoreSlim(0);

            void TesteeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName != propertyName)
                    return;

                _testee.PropertyChanged -= TesteeOnPropertyChanged;
                semaphoreSlim.Release();
            }

            _testee.PropertyChanged += TesteeOnPropertyChanged;

            return semaphoreSlim;
        }

        #endregion
    }
}

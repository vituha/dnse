using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;
using VS.Library.Text;

namespace VS.Library.UT.Text
{
    [TestFixture]
    internal sealed class StringBufferTests
    {
        private Fixture _fixture;

        private StringBuffer _sut;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _sut = new StringBuffer();
        }

        [Test]
        public void Add_should_update_length()
        {
            string someText = _fixture.Create<string>();

            _sut.Add(someText);

            _sut.Length.Should().Be(someText.Length);
        }

        [Test]
        public void Clear_should_set_Length_to_zero()
        {
            Populate(_sut, _fixture);

            _sut.Clear();

            _sut.Length.Should().Be(0);
        }

        [Test]
        public void WriteTo_should_write_all_strings()
        {
            List<string> texts = Populate(_sut, _fixture);

            using (var writer = new StringWriter())
            {
                _sut.WriteTo(writer);
                writer.ToString().Should().Be(string.Concat(texts));
            }
        }

        [Test]
        public async Task WriteToAsync_should_write_all_strings()
        {
            List<string> texts = Populate(_sut, _fixture);

            using (var writer = new StringWriter())
            {
                await _sut.WriteToAsync(writer);
                writer.ToString().Should().Be(string.Concat(texts));
            }
        }

        [Test]
        public void WriteToAsync_should_respect_CancellationToken()
        {
            Populate(_sut, _fixture);

            var tokenSource = new CancellationTokenSource();
            Func<Task> act = async () =>
            {
                using (var writer = new StringWriter())
                {
                    await _sut.WriteToAsync(writer, tokenSource.Token);
                }
            };
            tokenSource.Cancel();

            act.ShouldThrow<OperationCanceledException>();
        }

        private static List<string> Populate(StringBuffer buffer, ISpecimenBuilder fixture)
        {
            List<string> someTexts = fixture.CreateMany<string>().ToList();
            someTexts.ForEach(buffer.Add);
            return someTexts;
        }
    }
}

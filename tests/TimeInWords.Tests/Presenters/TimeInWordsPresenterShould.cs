using TextToTimeGridLib;
using TimeInWords.Presenters;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Tests.Presenters;

public class TimeInWordsPresenterShould
{
    [Fact]
    public void InitialiseTheView()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var now = new DateTime(2024, 1, 1, 14, 12, 0);
        dateTimeProvider.Now.Returns(now);
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        Received.InOrder(() =>
        {
            view.Initialise(settings, TimeGrid.Get(settings.Language));
            view.Update(true);
        });
        view.Time.Should().Be(now);
        view.TimeAsText.ToString().Should().Be("IT IS TEN PAST TWO +2");
        view.GridBitMask.Should()
            .BeEquivalentTo(StrictMask(settings, "IT IS TEN PAST TWO"), options => options.WithStrictOrdering());
    }

    [Theory]
    [InlineData(LanguagePreset.Language.Dutch, "HET IS TIEN OVER TWEE +0")]
    [InlineData(LanguagePreset.Language.German, "ES IST ZEHN NACH ZWEI +0")]
    public void ShowTheTimeInTheConfiguredLanguage(LanguagePreset.Language language, string expected)
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings { Language = language };
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.Now.Returns(new DateTime(2024, 1, 1, 2, 10, 0));
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        view.Received(1).Initialise(settings, TimeGrid.Get(language));
        view.TimeAsText.ToString().Should().Be(expected);
        view.GridBitMask.Should()
            .BeEquivalentTo(StrictMask(settings, view.TimeAsText.TimeAsText), options => options.WithStrictOrdering());
    }

    [Fact]
    public void ConfigureTimerCorrectly()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        timer.Interval.Should().Be(1000);
        timer.Enabled.Should().BeTrue();
    }

    [Fact]
    public void UpdateTheViewToTheCurrentTimeOnTimerTick()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var now1 = new DateTime(2024, 1, 1, 14, 12, 0);
        dateTimeProvider.Now.Returns(_ => now1);
        var timer = Substitute.For<ITimer>();
        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        var now2 = new DateTime(2024, 1, 1, 15, 31, 0);
        dateTimeProvider.Now.Returns(_ => now2);
        timer.Tick += Raise.Event();

        view.Time.Should().Be(now2);
        view.TimeAsText.ToString().Should().Be("IT IS HALF PAST THREE +1");
        view.GridBitMask.Should()
            .BeEquivalentTo(StrictMask(settings, "IT IS HALF PAST THREE"), options => options.WithStrictOrdering());
        view.Received(1).Update(true);
        view.Received(1).Update();
    }

    [Fact]
    public void AdvanceOneWholeMinuteEachTimeTheViewUpdatesInDebugMode()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings { Debug = true };
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var now1 = new DateTime(2024, 1, 1, 14, 12, 42);
        dateTimeProvider.Now.Returns(_ => now1);
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        // the seconds are dropped so the debug clock lands on whole minutes
        view.Time.Should().Be(new DateTime(2024, 1, 1, 14, 13, 0));

        // the debug clock runs on its own, ignoring the real time
        var now2 = new DateTime(2024, 1, 1, 9, 0, 0);
        dateTimeProvider.Now.Returns(_ => now2);
        timer.Tick += Raise.Event();
        timer.Tick += Raise.Event();

        view.Time.Should().Be(new DateTime(2024, 1, 1, 14, 15, 0));
        view.TimeAsText.ToString().Should().Be("IT IS A QUARTER PAST TWO +0");
        view.Received(1).Update(true);
        view.Received(2).Update();
    }

    private static bool[][] StrictMask(TimeInWordsSettings settings, string text) =>
        TimeGrid.Get(settings.Language).GetBitMask(text, true).Mask;
}

using GuessNumber.Entities;
using GuessNumber.Interfaces;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;

namespace GuessNumber.Tests.Entities;

public class GameTests
{
    private int _min;
    private int _max;
    private Game _game;
    private Mock<IRandomNumberGenerator> _randomMock;

    public GameTests()
    {
        _randomMock = new Mock<IRandomNumberGenerator>();
        _randomMock.Setup(r => r.MinValue).Returns(0);
        _randomMock.Setup(r => r.MaxValue).Returns(100);
        _randomMock.Setup(r => r.Generate()).Returns(7);
        _min = _randomMock.Object.MinValue;
        _max = _randomMock.Object.MaxValue;
    }

    [Fact]
    public void Play_NumberWasPassed_GuessedDidntStartNewGame()
    {
        var writtenMessages = new List<string>();
        var expectedAnswers = new List<string>() { $"Hello, try to guess the number from {_min} to {_max}",
                                                    "What number did I guess?",
                                                    "Congratulatutions, you win",
                                                    "Do you want play one more time? Enter Yes or No "};

        var userInputs = new Mock<Func<string>>();
        userInputs
            .SetupSequence(_ => _.Invoke())
            .Returns("7")
            .Returns("No");

        Action<string> writeline = (line) => writtenMessages.Add(line);

        _game = new Game(userInputs.Object, writeline, _randomMock.Object);

        _game.Play();

        Assert.Equal(expectedAnswers, writtenMessages);
    }

    [Fact]
    public void Play_NumberWasPassed_DidntGuessedGuessedDidntStartNewGame()
    {
        var writtenMessages = new List<string>();
        var expectedAnswers = new List<string>() { $"Hello, try to guess the number from {_min} to {_max}",
                                                    "What number did I guess?",
                                                    "Slowly, little bit smaller number",
                                                    "Sorry could not parse number, try again",
                                                    "Congratulatutions, you win",
                                                    "Do you want play one more time? Enter Yes or No "};

        var userInputs = new Mock<Func<string>>();
        userInputs
            .SetupSequence(_ => _.Invoke())
            .Returns("10")
            .Returns("abc")
            .Returns("7")
            .Returns("No");

        Action<string> writeline = (line) => writtenMessages.Add(line);

        _game = new Game(userInputs.Object, writeline, _randomMock.Object);

        _game.Play();

        Assert.Equal(expectedAnswers, writtenMessages);
    }

    [Fact]
    public void Play_NumberWasPassed_GuessedNewGameGuessedDidntStartNewGame()
    {
        var writtenMessages = new List<string>();
        var expectedAnswers = new List<string>() { $"Hello, try to guess the number from {_min} to {_max}",
                                                    "What number did I guess?",
                                                    "Congratulatutions, you win",
                                                    "Do you want play one more time? Enter Yes or No ",
                                                    "What number did I guess?",
                                                    "Congratulatutions, you win",
                                                    "Do you want play one more time? Enter Yes or No "};

        var userInputs = new Mock<Func<string>>();
        userInputs
            .SetupSequence(_ => _.Invoke())
            .Returns("7")
            .Returns("Yes")
            .Returns("7")
            .Returns("No");

        Action<string> writeline = (line) => writtenMessages.Add(line);

        _game = new Game(userInputs.Object, writeline, _randomMock.Object);

        _game.Play();

        Assert.Equal(expectedAnswers, writtenMessages);
    }

    [Fact]
    public void Play_NotValidRandomIsPassed_ReturnsException()
    {
        string expected = "randomNumberGenerator";
        Func<string> readline = () => string.Empty;
        Action<string> writeline = (line) => line = string.Empty;

        var randomException = Assert.Throws<ArgumentNullException>(() => _game = new Game(readline, writeline, null));

        Assert.Equal(randomException.ParamName, expected);
    }

    [Fact]
    public void Play_NotValidFunctionIsPassed_ReturnsException()
    {
        string expected = "inputProvider";
        Func<string> readline = () => string.Empty;
        Action<string> writeline = (line) => line = string.Empty;

        var functionException = Assert.Throws<ArgumentNullException>(() => _game = new Game(null, writeline, null));

        Assert.Equal(functionException.ParamName, expected);
    }

    [Fact]
    public void Play_NotValidActionIsPassed_ReturnsException()
    {
        string expected = "outputProvider";
        Func<string> readline = () => string.Empty;
        Action<string> writeline = (line) => line = string.Empty;

        var actionException = Assert.Throws<ArgumentNullException>(() => _game = new Game(readline, null, null));

        Assert.Equal(actionException.ParamName, expected);
    }
}
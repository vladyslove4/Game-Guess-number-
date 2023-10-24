using GuessNumber.Interfaces;

namespace GuessNumber.Entities;

internal class Game
{
    private IRandomNumberGenerator _randomNumberGenerator;
    private bool _isPlaying;
    private bool _gameOver;
    private readonly Func<string?> _inputProvider;
    private readonly Action<string> _outputProvider;

    public Game(Func<string?> inputProvider, Action<string> outputProvider, IRandomNumberGenerator randomNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(inputProvider);
        ArgumentNullException.ThrowIfNull(outputProvider);
        ArgumentNullException.ThrowIfNull(randomNumberGenerator);

        _inputProvider = inputProvider;
        _outputProvider = outputProvider;
        _randomNumberGenerator = randomNumberGenerator;
    }

    public void Play()
    {
        int customerNumber;
        int randomNumber = _randomNumberGenerator.Generate();
        int minValue = _randomNumberGenerator.MinValue;
        int maxValue = _randomNumberGenerator.MaxValue;
        _isPlaying = true;

        _outputProvider($"Hello, try to guess the number from {minValue} to {maxValue}");
        InitGame();

        while (_isPlaying)
        {
            if (_gameOver)
            {
                randomNumber = _randomNumberGenerator.Generate();
                InitGame();
            }

            customerNumber = AskNumber();

            switch (customerNumber)
            {
                case var outOfRange when outOfRange < minValue || outOfRange > maxValue:
                    _outputProvider($"Number is out of {minValue} - {maxValue} range");

                    break;


                case var bigger when customerNumber > randomNumber:
                    _outputProvider("Slowly, little bit smaller number");

                    break;

                case var smaller when customerNumber < randomNumber:
                    _outputProvider("Close, but little bit bigger number");

                    break;

                default:
                    FinishGame();
                    break;
            }
        }
    }

    private void InitGame()
    {
        _gameOver = false;
        _outputProvider("What number did I guess?");
    }

    private int AskNumber()
    {
        while (true)
        {
            string data = _inputProvider() ?? string.Empty;
            if ((int.TryParse(data, out int result)))
            {
                return result;
            }
            _outputProvider("Sorry could not parse number, try again");
        }
    }

    private void FinishGame()
    {
        _outputProvider("Congratulatutions, you win");
        _gameOver = true;

        AskAboutNewGame();
    }

    private void AskAboutNewGame()
    {
        _outputProvider("Do you want play one more time? Enter Yes or No ");

        string answer = _inputProvider() ?? string.Empty;

        if (!answer.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            _isPlaying = false;
        }
    }
}
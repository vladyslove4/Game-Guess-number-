using GuessNumber.Interfaces;

namespace GuessNumber.Entities;

internal class RandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Random _random;
    public int MinValue { get; }
    public int MaxValue { get; }

    public RandomNumberGenerator(int minValue, int maxValue)
    {
        _random = new Random((int)DateTime.Now.Ticks);
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public int Generate()
    {
        return _random.Next(MinValue, MaxValue + 1);
    }
}

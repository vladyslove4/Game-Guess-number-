namespace GuessNumber.Interfaces;

public interface IRandomNumberGenerator
{
    int MinValue { get; }
    int MaxValue { get; }
    int Generate();
}

using GuessNumber.Entities;
using GuessNumber.Interfaces;

namespace GuessNumber.Tests.Entities;

public class RandomNumberGeneratorTests
{
    private IRandomNumberGenerator _randomNumberGenerator;

    public RandomNumberGeneratorTests()
    {
        _randomNumberGenerator = new RandomNumberGenerator(0, 101);
    }

    [Fact]
    public void Generate_GenerateRandomNumber_ReturnsNumber()
    {
        var expectedNumber = _randomNumberGenerator.Generate();

        Assert.True(expectedNumber > _randomNumberGenerator.MinValue && expectedNumber < _randomNumberGenerator.MaxValue);
    }
}
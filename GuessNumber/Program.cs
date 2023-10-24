using GuessNumber.Entities;
using GuessNumber.Interfaces;

namespace GuessNumber;

class Program
{
    static void Main(string[] args)
    {
        IRandomNumberGenerator _random = new RandomNumberGenerator(0,100);

        var game = new Game(Console.ReadLine, Console.WriteLine, _random);

        game.Play();
    }
}

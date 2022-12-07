enum GameMove
{
    Rock, Paper, Scissors
}

enum GameResult
{
    Win, Lose, Draw
}

internal class Program
{
    public int GetMovePoints(GameMove move) => move switch
    {
        GameMove.Rock => 1,
        GameMove.Paper => 2,
        GameMove.Scissors => 3,
        _ => throw new ArgumentException("Incorrect Move")
    };

    public static (GameResult, GameResult)? ResultHelper(GameMove move1, GameMove move2,
            GameMove winnerMove, GameMove loserMove)
    {
        return
            move1 == winnerMove && move2 == loserMove ?
                (GameResult.Win, GameResult.Lose) :
            move1 == loserMove && move2 == winnerMove ?
                (GameResult.Lose, GameResult.Win) : null;
    }

    public static (GameResult, GameResult) GetPlayerResult(GameMove movePlayer1, GameMove movePlayer2)
    {
        return
            ResultHelper(movePlayer1, movePlayer2, GameMove.Rock, GameMove.Scissors) ??
            ResultHelper(movePlayer1, movePlayer2, GameMove.Scissors, GameMove.Paper) ??
            ResultHelper(movePlayer1, movePlayer2, GameMove.Paper, GameMove.Rock) ??
            (GameResult.Draw, GameResult.Draw);
    }

    private static void Main(string[] args)
    {
        var lines = File.ReadLines("./input.txt");

        var scorePart1 = (0, 0);

        var oponnentMoves = new Dictionary<string, GameMove>()
        {
            { "A", GameMove.Rock },
            { "B", GameMove.Paper },
            { "C", GameMove.Scissors }
        };

        var myMoves = new Dictionary<string, GameMove>()
        {
            { "X", GameMove.Rock },
            { "Y", GameMove.Paper },
            { "Z", GameMove.Scissors }
        };

        var pointsPerMove = new Dictionary<GameMove, int>()
        {
            { GameMove.Rock, 1 },
            { GameMove.Paper, 2 },
            { GameMove.Scissors, 3 }
        };

        var pointsPerResult = new Dictionary<GameResult, int>()
        {
            { GameResult.Draw, 3 },
            { GameResult.Win, 6 },
            { GameResult.Lose, 0 }
        };


        foreach (var line in lines)
        {
            var playerMoves = line.Split(' ');
            var movePlayer1 = oponnentMoves[playerMoves[0]];
            var movePlayer2 = myMoves[playerMoves[1]];

            var result = GetPlayerResult(movePlayer1, movePlayer2);

            scorePart1.Item1 += pointsPerMove[movePlayer1] + pointsPerResult[result.Item1];
            scorePart1.Item2 += pointsPerMove[movePlayer2] + pointsPerResult[result.Item2];
        }

        Console.WriteLine("Parte1: Oponente: {0}, Eu: {1}", scorePart1.Item1, scorePart1.Item2);

        // part 2

        var getNextMove = (string ac, GameMove move) =>
        {
            if (ac == "Y") // draw
                return move;
            else if (ac == "X") //lose
                return move switch
                {
                    GameMove.Rock => GameMove.Scissors,
                    GameMove.Scissors => GameMove.Paper,
                    GameMove.Paper => GameMove.Rock,
                    _ => throw new ArgumentException("Incorrect Move")
                };
            else
                return move switch
                {
                    GameMove.Rock => GameMove.Paper,
                    GameMove.Paper => GameMove.Scissors,
                    GameMove.Scissors => GameMove.Rock,
                    _ => throw new ArgumentException("Incorrect Move")
                };
        };

        var scorePart2 = (0, 0);
        foreach (var line in lines)
        {
            var playerMoves = line.Split(' ');
            var movePlayer1 = oponnentMoves[playerMoves[0]];
            var movePlayer2 = getNextMove(playerMoves[1], oponnentMoves[playerMoves[0]]);

            var result = GetPlayerResult(movePlayer1, movePlayer2);

            scorePart2.Item1 += pointsPerMove[movePlayer1] + pointsPerResult[result.Item1];
            scorePart2.Item2 += pointsPerMove[movePlayer2] + pointsPerResult[result.Item2];
        }

        Console.WriteLine("Parte2: Oponente: {0}, Eu: {1}", scorePart2.Item1, scorePart2.Item2);
    }
}


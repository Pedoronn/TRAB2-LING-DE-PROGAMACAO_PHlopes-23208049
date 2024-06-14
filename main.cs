using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int[,] bingoCard = GenerateBingoCard();
        Console.WriteLine("Esta é sua cartela da sorte, bom jogo:");
        DisplayBingoCard(bingoCard);

        List<int> drawnNumbers = new List<int>();
        Random random = new Random();

        bool hasWon = false;
        int rounds = 0;

        while (rounds < 75 && !hasWon)
        {
            rounds++;
            int drawnNumber = DrawNumber(drawnNumbers, random);
            drawnNumbers.Add(drawnNumber);
            Console.WriteLine($"Número sorteado: {drawnNumber}");
            if (MarkBingoCard(bingoCard, drawnNumber))
            {
                Console.WriteLine("Parabéns, você marcou uma casa!");
            }

            DisplayBingoCard(bingoCard);
            hasWon = CheckWin(bingoCard);

            if (hasWon)
            {
                Console.WriteLine("Parabéns, você venceu!");
            }

            Thread.Sleep(5000); // Espera 5 segundos entre cada sorteio
        }

        if (!hasWon)
        {
            Console.WriteLine("Você perdeu! Não conseguiu marcar todos os números em 75 sorteios.");
        }
    }

    static int[,] GenerateBingoCard()
    {
        Random random = new Random();
        HashSet<int> numbers = new HashSet<int>();

        while (numbers.Count < 24)
        {
            numbers.Add(random.Next(1, 100));
        }

        List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();

        int[,] card = new int[5, 5];
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 2 && j == 2)
                {
                    card[i, j] = 0; // Espaço central vazio
                }
                else
                {
                    card[i, j] = sortedNumbers[index];
                    index++;
                }
            }
        }

        return card;
    }

    static void DisplayBingoCard(int[,] card)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 2 && j == 2)
                {
                    Console.Write("  *  "); // Espaço central vazio
                }
                else
                {
                    Console.Write($"{card[i, j],4} ");
                }
            }
            Console.WriteLine();
        }
    }

    static int DrawNumber(List<int> drawnNumbers, Random random)
    {
        int number;
        do
        {
            number = random.Next(1, 100);
        } while (drawnNumbers.Contains(number));
        return number;
    }

    static bool MarkBingoCard(int[,] card, int number)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (card[i, j] == number)
                {
                    card[i, j] = -1; // Marcando o número
                    return true;
                }
            }
        }
        return false;
    }

    static bool CheckWin(int[,] card)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Enumerable.Range(0, 5).All(j => card[i, j] == -1) || Enumerable.Range(0, 5).All(j => card[j, i] == -1))
            {
                return true;
            }
        }

        if (Enumerable.Range(0, 5).All(i => card[i, i] == -1) || Enumerable.Range(0, 5).All(i => card[i, 4 - i] == -1))
        {
            return true;
        }

        return false;
    }
}


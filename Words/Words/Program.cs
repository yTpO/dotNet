using System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string path = "inputData.txt";
        var initialWordsList = new List<string>();

        try
        {
            using var reader = new StreamReader(path);
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
                initialWordsList.Add(line);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
            return;
        }

        Console.Write("Введите время ввода слова (сек): ");

        int timeForReadline = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Выберите слово, с которым будете играть");

        for (int i = 1; i <= initialWordsList.Count; i++)
            Console.WriteLine($"{i}. {initialWordsList[i - 1]}");

        string parentWord;
        try
        {
            int parentWordIndex = Convert.ToInt32(Console.ReadLine());
            parentWord = initialWordsList[--parentWordIndex];
        }
        catch
        {
            Console.WriteLine("Возникла ошибка!");
            return;
        }

        if (parentWord != "")
        {
            Console.Clear();
            Console.WriteLine($"Вы выбрали слово '{parentWord}'\n");
        }

        bool gameOver = false;
        var usedWords = new List<string>();

        while (!gameOver)
        {
            DateTime t = DateTime.Now;

            Console.Write("Введите слово: ");
            string inputWord = Console.ReadLine();

            if ((DateTime.Now - t).TotalSeconds < timeForReadline)
            {
                if (CheckWords(inputWord, parentWord))
                {
                    if (!ExistWords(inputWord, usedWords))
                        gameOver = GameOverText("Такое слово уже вводилось!");
                }
                else
                    gameOver = GameOverText("Такое слово нельзя составить из родительского слова!");
            }
            else
                gameOver = GameOverText("Превышено время ожидания!");
        }

        Console.ReadKey();
    }

    public static bool CheckWords(string newWord, string parentWord)
    {
        var word2List = parentWord.ToList();
        foreach (char ch in newWord)
        {
            if (word2List.IndexOf(ch) < 0)
                return false;
            word2List.Remove(ch);
        }
        return true;
    }

    public static bool ExistWords(string newWord, List<string> usedWords)
    {
        if (usedWords.IndexOf(newWord) < 0)
        {
            usedWords.Add(newWord);
            return true;
        }
        else
            return false;
    }

    public static bool GameOverText(string errorMessage)
    {
        Console.WriteLine($"\n{errorMessage}\nВы проиграли");
        return true;
    }
}
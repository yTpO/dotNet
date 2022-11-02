using System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        bool exception = false;
        string path = "inputData.txt";
        var initialWordsList = new List<string>();

        try
        {
            using StreamReader reader = new StreamReader(path);
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                initialWordsList.Add(line);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
            exception = true;
        }

        Console.Write("Введите время ввода слова (сек): ");
        int timeForReadline = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        if (!exception)
        {
            Console.WriteLine("Выберите слово, с которым будете играть");
            int i = 0;
            foreach (string word in initialWordsList)
            {
                Console.WriteLine($"{++i}. {word}");
            }
            string parentWord = "";
            try
            {
                int parentWordIndex = Convert.ToInt32(Console.ReadLine());
                parentWord = initialWordsList[--parentWordIndex];
            }
            catch
            {
                Console.WriteLine("Возникла ошибка!");
                exception = true;
            }

            if (exception != true)
            {
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
                        if (checkWords(inputWord, parentWord))
                        {
                            if (!existWords(inputWord, usedWords))
                                gameOver = gameOverText("Такое слово уже вводилось!");
                        }
                        else
                            gameOver = gameOverText("Такое слово нельзя составить из родительского слова!");
                    }
                    else
                        gameOver = gameOverText("Превышено время ожидания!");
                }
            }
        }

        Console.ReadKey();
    }

    public static bool checkWords(string word1, string word2)
    {
        var word2List = word2.ToList();
        foreach (char ch in word1)
        {
            if (word2List.IndexOf(ch) < 0)
                return false;
            word2List.Remove(ch);
        }
        return true;
    }

    public static bool existWords(string newWord, List<string> usedWords)
    {
        if (usedWords.IndexOf(newWord) < 0)
        {
            usedWords.Add(newWord);
            return true;
        }
        else
            return false;
    }

    public static bool gameOverText(string errorMessage)
    {
        Console.WriteLine($"\n{errorMessage}\nВы проиграли");
        return true;
    }
}
internal class Program
{
    private static async Task Main(string[] args)
    {
        bool exception = false;
            string path = "inputData.txt";
            var wordsList = new List<string>();

            try
            {
                using StreamReader reader = new StreamReader(path);
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    wordsList.Add(line);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
                exception = true;
            }

        if (!exception)
        {
            Console.WriteLine("Выберите слово, с которым будете играть");
            int i = 0;
            foreach (string word in wordsList)
            {
                Console.WriteLine($"{++i}. {word}");
            }
            string parentWord = "";
            try
            {
                int parentWordIndex = Convert.ToInt32(Console.ReadLine());
                parentWord = wordsList[--parentWordIndex];
            }
            catch
            {
                Console.WriteLine("Возникла ошибка!");
                exception = true;
            }

            if (parentWord != "")
                Console.WriteLine($"\nВы выбрали слово '{parentWord}'");

            bool gameOver = false;
            while (!gameOver)
            {
                Console.Write("Введите слово: ");
                string inputWord = Console.ReadLine();
                if (!CheckWords(inputWord, parentWord))
                {
                    gameOver = true;
                    Console.WriteLine("Не верно! Вы проиграли");
                }
            }
        }
    }

    public static bool CheckWords(string word1, string word2)
    {
        var word2List = word2.ToList();
        foreach (char ch in word1)
        {
            int qwe = word2List.IndexOf(ch);
            if (qwe < 0)
                return false;
            word2List.Remove(ch);
        }
        return true;
    }
}
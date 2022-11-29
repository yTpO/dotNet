namespace Words
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // настройки игры
            string lvl = await DataBase.GetAllLevels();
            string parentWordId = await DataBase.GetWordsByLvl(lvl);
            string parentWord = await DataBase.GetWordById(parentWordId, lvl);
            int timeForReadline = await DataBase.GetTimeByLvl(lvl);
            Console.Clear();

            // логика игры
            if (Engine.PrintWord(parentWord))
            {
                bool gameOver = false;
                var usedWords = new List<string>();

                while (!gameOver)
                {
                    DateTime t = DateTime.Now;

                    Console.Write("Введите слово: ");
                    string inputWord = Console.ReadLine();

                    if ((DateTime.Now - t).TotalSeconds < timeForReadline)
                    {
                        if (Engine.CheckWords(inputWord, parentWord))
                        {
                            if (!Engine.ExistWords(inputWord, usedWords))
                                gameOver = Engine.GameOverText("Такое слово уже вводилось!");
                        }
                        else
                            gameOver = Engine.GameOverText("Такое слово нельзя составить из родительского слова!");
                    }
                    else
                        gameOver = Engine.GameOverText("Превышено время ожидания!");
                }
            }

            Console.ReadKey();
        }
    }
}
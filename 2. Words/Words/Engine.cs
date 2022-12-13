namespace Words
{
    internal class Engine
    {
        /// <summary>
        /// Главный метод класса, запускающий логику игры
        /// </summary>
        /// <param name="parentWord">Исходное слово</param>
        /// <param name="timeForReadline">Время на ход</param>
        public static void Play(string parentWord, int timeForReadline)
        {
            bool gameOver = false;
            var usedWords = new List<string>();

            while (!gameOver)
            {
                DateTime t = DateTime.Now;

                Console.Write("Введите слово: ");
                string? inputWord = Console.ReadLine();

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
        }

        /// <summary>
        /// Проверка на корректность введённого слова 
        /// </summary>
        /// <param name="newWord">Введённое слово</param>
        /// <param name="parentWord">Исходное слово</param>
        /// <returns>true - введённое слово содержится в исходном, false в противоположном случае</returns>
        private static bool CheckWords(string newWord, string parentWord)
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

        /// <summary>
        /// Проверка введённого слова на уникальность
        /// </summary>
        /// <param name="newWord">Введённое слово</param>
        /// <param name="usedWords">Список уже использованных слов</param>
        /// <returns>true - введённое слово уникально и добавленио в список образованных слов, false в противоположном случае</returns>
        private static bool ExistWords(string newWord, List<string> usedWords)
        {
            if (usedWords.IndexOf(newWord) < 0)
            {
                usedWords.Add(newWord);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Отображение текста ошибки и уведомления о проигрыше
        /// </summary>
        /// <param name="errorMessage">Текст ошибки</param>
        /// <returns>true - игра проиграна, false - в противоположном случае</returns>
        private static bool GameOverText(string errorMessage)
        {
            Console.WriteLine($"\n{errorMessage}\nВы проиграли");
            Console.ReadKey();
            return true;
        }
    }
}

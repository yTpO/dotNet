using Microsoft.EntityFrameworkCore;

namespace Words
{
    internal class DbClass
    {
        private IWordRepository wordRepository;

        public DbClass()
        {
            this.wordRepository = new WordRepository(new WordsDbContext());
        }
        public DbClass(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        /// <summary>
        /// Метод, реализующий взаимодействие пользователя с БД
        /// </summary>
        public static void DbOperations()
        {
            Console.WriteLine("1) Отобразить все слова");
            Console.WriteLine("2) Добавить новое слово");
            Console.WriteLine("3) Изменить статус слова по ID");
            Console.WriteLine("4) Удалить слово по ID");
            Console.WriteLine("Другое = выход\n");
            Console.Write("Сделайте выбор:  ");

            string libraryMenu = Console.ReadLine();
            Console.Clear();
            int wordId;

            switch (libraryMenu)
            {
                case "1":
                    // Отобразить все слова
                    ShowAllWords();
                    break;
                case "2":
                    // Добавить новое слово
                    string inputWordValue;
                    int inputWordLvl;
                    bool inputWordActive;

                    Console.Write("Введите слово:  ");
                    inputWordValue = Console.ReadLine();
                    Console.Write("Задайте уровень сложности слову:  ");
                    try
                    {
                        inputWordLvl = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка ввода уровня сложности. По-умолчанию установлен уровень = 1");
                        inputWordLvl = 1;
                    }
                    Console.Write("Активировать слово? (1 = да, 0 = нет):  ");
                    inputWordActive = Console.ReadLine() == "1" ? true : false;
                    Word newWord = new Word
                    {
                        Value = inputWordValue.Trim(),
                        LvlId = inputWordLvl,
                        CreatedOn = DateTime.Now,
                        IsActive = inputWordActive
                    };

                    using (WordsDbContext db = new WordsDbContext())
                    {
                        var wordRepository = new WordRepository(db);

                        wordRepository.InsertWord(newWord); // вызов метода добавления
                        wordRepository.Save();
                        Console.WriteLine("Слово добавлено!");
                    }
                    break;
                case "3":
                    // Изменить статус слова по ID
                    Console.Write("Введите ID слова:  ");
                    wordId = Convert.ToInt32(Console.ReadLine());
                    using (WordsDbContext db = new WordsDbContext())
                    {
                        var wordRepository = new WordRepository(db);
                        var result = db.Words.SingleOrDefault(w => w.Id == wordId);
                        if (result != null)
                        {
                            result.IsActive = !result.IsActive;
                            wordRepository.UpdateWord(result); // вызов метода обновления из РЕПО
                            wordRepository.Save();
                        }

                        Console.WriteLine("Статус слова изменён!");
                    }
                    Console.ReadKey();
                    break;
                case "4":
                    // Удалить слово по ID
                    Console.Write("Введите ID слова для удаления:  ");
                    wordId = Convert.ToInt32(Console.ReadLine());
                    using (WordsDbContext db = new WordsDbContext())
                    {
                        var wordRepository = new WordRepository(db);
                        wordRepository.DeleteWord(wordId); // вызов метода удаления из РЕПО
                        wordRepository.Save();

                        //DeleteWord();
                        Console.WriteLine("Слово удалено!");
                    }
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Метод для отображения всех слов из БД
        /// </summary>
        private static void ShowAllWords()
        {
            Console.WriteLine("Идёт загрузка слов");
            using (WordsDbContext db = new WordsDbContext())
            {
                var wordRepository = new WordRepository(db);
                var words = from w in wordRepository.GetWords() select w; // вызов метода отображения всех слов из РЕПО

                Console.Clear();
                Console.WriteLine("Список слов:");
                Console.WriteLine("Id - Value - LvlId - CreatedOn - IsActive");
                foreach (Word w in words)
                    Console.WriteLine($"{w.Id} - {w.Value} - {w.LvlId} - {w.CreatedOn} - {w.IsActive}");
            };
            Console.ReadKey();
        }
    }
}

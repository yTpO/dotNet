namespace Words
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("1) Играть");
                Console.WriteLine("2) Словарь");
                Console.WriteLine("0) Выход\n");
                Console.Write("Сделайте выбор:  ");
                string? menu = Console.ReadLine();
                Console.Clear();

                switch (menu)
                {
                    case "1":
                        int lvl;
                        int timePerStep;
                        string selectedWord;

                        Console.WriteLine("Выберите уровень сложности:  ");
                        using (WordsDbContext db = new WordsDbContext())
                        {
                            var lvls = db.Lvls.ToList();
                            Console.WriteLine("Id - Name - Descriprion");
                            foreach (Lvl l in lvls)
                                Console.WriteLine($"{l.Id} - {l.Name} - {l.Description}");
                            try
                            {
                                lvl = Convert.ToInt32(Console.ReadLine());
                                timePerStep = Convert.ToInt32(db.Lvls.FirstOrDefault(l => l.Id == lvl).Time);
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка выбора уровня. Выбран 1 уровень; Время на ход = 30с");
                                lvl = 1;
                                timePerStep = 30;
                            }
                        };

                        using (WordsDbContext db = new WordsDbContext())
                        {
                            var words = db.Words.Where(l => l.LvlId == lvl);
                            Console.Clear();
                            Console.WriteLine("Список доступных слов:");
                            Console.WriteLine("Id - Value");
                            foreach (Word w in words)
                                Console.WriteLine($"{w.Id} - {w.Value}");
                            int selectedWordId = Convert.ToInt32(Console.ReadLine());
                            Word? selWord = db.Words.FirstOrDefault(w => w.Id == selectedWordId && w.LvlId == lvl && w.IsActive == true);
                            selectedWord = selWord != null ? selWord.Value : null;
                        };
                        if (selectedWord != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Выбранное слово:  {selectedWord}");
                            Engine.Play(selectedWord, timePerStep);
                        }
                        else
                            Console.WriteLine("Ошибка выбора слова");

                        break;
                    case "2":
                        DbClass.DbOperations();
                        break;
                    default:
                        exit = true;
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
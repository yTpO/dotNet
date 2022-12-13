using Microsoft.EntityFrameworkCore;

namespace Words
{
    public class WordRepository : IWordRepository, IDisposable
    {
        private WordsDbContext context;

        public WordRepository(WordsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Word> GetWords()
        {
            return context.Words.ToList();
        }

        public async Task<IEnumerable<Word>> GetWordsAsync()
        {
            return await context.Words.ToListAsync();
        }

        public Word GetWordByID(int id)
        {
            return context.Words.Find(id);
        }

        public void InsertWord(Word word)
        {
            context.Words.Add(word);
        }

        public void DeleteWord(int wordID)
        {
            Word word = context.Words.Find(wordID);
            context.Words.Remove(word);
        }

        public void UpdateWord(Word word)
        {
            context.Entry(word).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

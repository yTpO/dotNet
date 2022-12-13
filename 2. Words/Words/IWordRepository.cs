using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    public interface IWordRepository : IDisposable
    {
        IEnumerable<Word> GetWords();
        Task<IEnumerable<Word>> GetWordsAsync();
        Word GetWordByID(int wordId);
        void InsertWord(Word word);
        void DeleteWord(int wordId);
        void UpdateWord(Word word);
        void Save();
    }
}

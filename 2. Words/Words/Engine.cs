using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    internal class Engine
    {
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

        public static bool PrintWord(string word)
        {
            if (word != "")
            {
                Console.Clear();
                Console.WriteLine($"Вы выбрали слово '{word}'\n");
            }

            return (word != "");
        }
    }
}

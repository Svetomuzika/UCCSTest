using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UCSB.Controllers;
using UCSB.Models;

namespace UCSB
{
    public static class CountToDB
    {
        public static string? Domain;

        public static void LettersToDB(string text, string domain, ILogger<VKController> logger)
        {
            Domain = domain;
            var lettersDictionary = new Dictionary<char, int>();

            logger.LogInformation($"Начало подсчета вхождение одинаковых букв");

            foreach (var letter in text)
            {
                if (!lettersDictionary.ContainsKey(letter) && char.IsLetter(letter))
                {
                    var count = text.Count(e => e == letter);
                    lettersDictionary.Add(letter, count);
                }
            }

            logger.LogInformation($"Конец подсчёта. Итогове число вхождения - {lettersDictionary.Count}");

            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var e in lettersDictionary.OrderBy(x => x.Key))
                {
                    Letter letter = new Letter { Count = e.Value, Name = e.Key };
                    db.Add(letter);
                }

                db.SaveChanges();
            }
        }
    }
}

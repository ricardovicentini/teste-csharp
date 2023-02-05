using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilterAndRank
{
    public static class Ranking
    {
        public static IList<RankedResult> FilterByCountryWithRank(
            IList<Person> people,
            IList<CountryRanking> rankingData,
            IList<string> countryFilter,
            int minRank, int maxRank,
            int maxCount)
        {
            // TODO: write your solution here.  Do not change the method signature in any way as this is called from
            //       another test suite that would fail.  

            // return people
            // .Join(rankingData, p => p.Id, r => r.PersonId, (p, r) => new { p, r })
            // .OrderBy(x=> x.r.Rank)
            // .ThenBy(x => x.r.Country, StringComparer.Ordinal)
            // .ThenBy(x => x.p.Name, StringComparer.InvariantCultureIgnoreCase)
            // .Where(x => countryFilter.Contains(x.r.Country, StringComparer.OrdinalIgnoreCase) &&
            //  x.r.Rank >= minRank &&
            //  x.r.Rank <= maxRank)
            //  .GroupBy(x => new {x.r.Rank, x.r.Country, x.p.Name, x.p.Id})
            //  .SelectMany(g=> g.Take(maxCount)
            //  .Select(x=> new RankedResult(g.Key.Id,g.Key.Rank)))
            //  .ToList();


           var peopleWithRanking = people
                .Join(rankingData, p => p.Id, r => r.PersonId, (p, r) => new { Person = p, Rank = r })
                .OrderBy(pr => pr.Rank.Rank)
                .ThenBy(pr => countryFilter.Contains(pr.Rank.Country,StringComparer.Ordinal))
                .ThenBy(pr => pr.Person.Name, StringComparer.OrdinalIgnoreCase)
                .Where(pr => countryFilter.Contains(pr.Rank.Country, StringComparer.OrdinalIgnoreCase) && pr.Rank.Rank >= minRank && pr.Rank.Rank <= maxRank)
                .Take(maxCount);

            return peopleWithRanking
                .Select(pr => new RankedResult(pr.Person.Id, pr.Rank.Rank))
                .ToList();
        }
    }
}

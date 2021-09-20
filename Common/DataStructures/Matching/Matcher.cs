#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using System.Linq;
using Rejuvena.Common.Utilities;

namespace Rejuvena.Common.DataStructures.Matching
{
    public class Matcher<TMatcher> : IMatchCondition<TMatcher>
    {
        public virtual List<IMatchCondition<TMatcher>> Conditions { get; }

        public virtual MatchType MatchType { get; }

        public Matcher(MatchType matchType = MatchType.All)
        {
            MatchType = matchType;
            Conditions = new List<IMatchCondition<TMatcher>>();
        }

        public virtual Matcher<TMatcher> WithCondition(IMatchCondition<TMatcher> condition) =>
            Builder.Do(this, () => Conditions.Add(condition));

        public virtual bool Match(TMatcher matcher) => MatchType switch
        {
            MatchType.All => Conditions.TrueForAll(x => x.Match(matcher)),
            MatchType.One => Conditions.Any(x => x.Match(matcher)),
            _ => false
        };
    }
}
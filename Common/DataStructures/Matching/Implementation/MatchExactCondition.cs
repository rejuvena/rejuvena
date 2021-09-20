#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Linq;
using Rejuvena.Common.Utilities;

namespace Rejuvena.Common.DataStructures.Matching.Implementation
{
    public class MatchExactCondition<TMatcher> : MatchCondition<TMatcher>
    {
        public virtual TMatcher[] Matchable { get; }

        public MatchExactCondition(params TMatcher[] toMatch)
        {
            Matchable = toMatch;
        }

        public MatchExactCondition(TMatcher toMatch) : this(toMatch.AsArray())
        {
        }

        public override bool Match(TMatcher matcher) => Matchable.Any(x => x.Equals(matcher));
    }
}
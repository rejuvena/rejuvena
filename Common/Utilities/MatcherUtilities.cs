#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using TomatoLib.Common.Utilities.Matching;
using TomatoLib.Common.Utilities.Matching.Implementation;

namespace Rejuvena.Common.Utilities
{
    public static class MatcherUtilities
    {
        public static Matcher<TMatcher> MatchExact<TMatcher>(this Matcher<TMatcher> matcher,
            params TMatcher[] toMatch) => matcher.WithCondition(new MatchExactCondition<TMatcher>(toMatch));
    }
}
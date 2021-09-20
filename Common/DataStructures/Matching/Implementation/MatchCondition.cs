#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace Rejuvena.Common.DataStructures.Matching.Implementation
{
    public abstract class MatchCondition<TMatcher> : IMatchCondition<TMatcher>
    {
        public abstract bool Match(TMatcher matcher);
    }
}
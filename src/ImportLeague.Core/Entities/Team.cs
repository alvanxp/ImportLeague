using System;
using System.Collections.Generic;

namespace ImportLeague.Core.Entities
{
    public partial class Team
    {
        public Team()
        {
        }
        public long Id { get; set; }

        public string AreaName { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Tla { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public virtual ICollection<TeamByCompetition> TeamByCompetition { get; set; }
    }

    public partial class TeamByCompetition
    {
        public long CompetitionId { get; set; }
        public long TeamId { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual Team Team { get; set; }
    }
}

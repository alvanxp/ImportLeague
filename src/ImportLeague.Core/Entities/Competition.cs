using System;
using System.Collections.Generic;

namespace ImportLeague.Core.Entities
{
    public class Competition
    {
        public long Id { get; set; }

        public string AreaName { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public virtual ICollection<TeamByCompetition> TeamByCompetition { get; set; }
    }
}

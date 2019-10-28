using System;
using System.Collections.Generic;

namespace ImportLeague.Infrastructure.ExternalModels
{
    public partial class Competition
    {
        public long Id { get; set; }

        public Area Area { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime LastUpdated { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ImportLeague.Infrastructure.ExternalModels
{
    public partial class Team
    {
        public long Id { get; set; }

        public Area Area { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Tla { get; set; }
        public string Email { get; set; }

        public List<Player> Squad { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

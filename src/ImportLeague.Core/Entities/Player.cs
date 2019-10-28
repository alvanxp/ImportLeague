using System;

namespace ImportLeague.Core.Entities
{
    public class Player
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CountryOfBirth { get; set; }

        public string Nationality { get; set; }

        public long TeamId { get; set; }

        public DateTime? LastUpdated { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public virtual Team Team { get; set; }
    }
}

using Remote.DbModels;
using System;
using System.Collections.Generic;

namespace DataImport
{
    public class LocationImporter
    {
        private DataContext _context;

        public LocationImporter(DataContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }

        public void ImportStates()
        {
            IEnumerable<Location> states = new List<Location>
            {
                new Location { Name = "AL" },
                new Location { Name = "AR" },
                new Location { Name = "AZ" },
                new Location { Name = "CA" },
                new Location { Name = "CO" },
                new Location { Name = "CT" },
                new Location { Name = "DE" },
                new Location { Name = "FL" },
                new Location { Name = "GA" },
                new Location { Name = "IA" },
                new Location { Name = "ID" },
                new Location { Name = "IL" },
                new Location { Name = "IN" },
                new Location { Name = "KS" },
                new Location { Name = "KY" },
                new Location { Name = "LA" },
                new Location { Name = "MA" },
                new Location { Name = "MD" },
                new Location { Name = "ME" },
                new Location { Name = "MI" },
                new Location { Name = "MN" },
                new Location { Name = "MO" },
                new Location { Name = "MS" },
                new Location { Name = "MT" },
                new Location { Name = "NC" },
                new Location { Name = "ND" },
                new Location { Name = "NE" },
                new Location { Name = "NH" },
                new Location { Name = "NJ" },
                new Location { Name = "NM" },
                new Location { Name = "NV" },
                new Location { Name = "NY" },
                new Location { Name = "OH" },
                new Location { Name = "OK" },
                new Location { Name = "OR" },
                new Location { Name = "PA" },
                new Location { Name = "RI" },
                new Location { Name = "SC" },
                new Location { Name = "SD" },
                new Location { Name = "TN" },
                new Location { Name = "TX" },
                new Location { Name = "UT" },
                new Location { Name = "VA" },
                new Location { Name = "VT" },
                new Location { Name = "WA" },
                new Location { Name = "WI" },
                new Location { Name = "WV" },
                new Location { Name = "WY" }
            };

            _context.Locations.AddRange(states);
            _context.SaveChanges();
        }
    }
}

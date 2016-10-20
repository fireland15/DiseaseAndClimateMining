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
                new Location { Name = "ALABAMA" },
                new Location { Name = "ARKANSAS" },
                new Location { Name = "ARIZONA" },
                new Location { Name = "CALIFORNIA" },
                new Location { Name = "COLORADO" },
                new Location { Name = "CONNECTICUT" },
                new Location { Name = "DELAWARE" },
                new Location { Name = "FLORIDA" },
                new Location { Name = "GEORGIA" },
                new Location { Name = "IOWA" },
                new Location { Name = "IDAHO" },
                new Location { Name = "ILLINOIS" },
                new Location { Name = "INDIANA" },
                new Location { Name = "KANSAS" },
                new Location { Name = "KENTUCKY" },
                new Location { Name = "LOUISIANA" },
                new Location { Name = "MASSACHUSETTS" },
                new Location { Name = "MARYLAND" },
                new Location { Name = "MAINE" },
                new Location { Name = "MICHIGAN" },
                new Location { Name = "MINNESOTA" },
                new Location { Name = "MISSOURI" },
                new Location { Name = "MISSISSIPPI" },
                new Location { Name = "MONTANA" },
                new Location { Name = "NORTH CAROLINA" },
                new Location { Name = "NORTH DAKOTA" },
                new Location { Name = "NEBRASKA" },
                new Location { Name = "NEW HAMPSHIRE" },
                new Location { Name = "NEW JERSEY" },
                new Location { Name = "NEW MEXICO" },
                new Location { Name = "NEVADA" },
                new Location { Name = "NEW YORK" },
                new Location { Name = "OHIO" },
                new Location { Name = "OKLAHOMA" },
                new Location { Name = "OREGON" },
                new Location { Name = "PENNSYLVANIA" },
                new Location { Name = "RHODE ISLAND" },
                new Location { Name = "SOUTH CAROLINA" },
                new Location { Name = "SOUTH DAKOTA" },
                new Location { Name = "TENNESSEE" },
                new Location { Name = "TEXAS" },
                new Location { Name = "UTAH" },
                new Location { Name = "VIRGINIA" },
                new Location { Name = "VERMONT" },
                new Location { Name = "WASHINGTON" },
                new Location { Name = "WISCONSIN" },
                new Location { Name = "WEST VIRGINIA" },
                new Location { Name = "WYOMING" }
            };

            _context.Locations.AddRange(states);
            _context.SaveChanges();
        }
    }
}

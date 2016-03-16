using System;
using System.Collections.Generic;

namespace Four2n.JustApp.Domain.Applications
{
    public class Application
    {
        [Obsolete("For EF only", true)]
        public Application()
        {
        }

        public Application(string name)
        {
            this.Name = name;
            RentedTo = new List<ApplicationRental>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public IList<ApplicationRental> RentedTo { get; set; }
    }
}
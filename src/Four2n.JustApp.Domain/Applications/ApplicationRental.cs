using System;

using Four2n.JustApp.Domain.Organizational;

namespace Four2n.JustApp.Domain.Applications
{
    public class ApplicationRental
    {
        [Obsolete("For EF only", true)]
        public ApplicationRental()
        {
        }

        public ApplicationRental(Application application, OrganizationalUnit organizationalUnit, bool inherited)
        {
            Application = application;
            OrganizationalUnit = organizationalUnit;
            Inherited = inherited;
        }

        public long Id { get; set; }

        public Application Application { get; set; }

        public OrganizationalUnit OrganizationalUnit { get; set; }

        public bool Inherited { get; set; }

        public DateTime? Begins { get; set; }

        public DateTime? Ends { get; set; }
    }
}
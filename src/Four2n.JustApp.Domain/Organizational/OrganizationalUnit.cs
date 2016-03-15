using System.Collections.Generic;

using Four2n.JustApp.Domain.Applications;

namespace Four2n.JustApp.Domain.Organizational
{
    public class OrganizationalUnit
    {
        protected OrganizationalUnit() { }

        public OrganizationalUnit(string name) : this(name, null)
        {
        }

        public OrganizationalUnit(string name, OrganizationalUnit parent)
        {
            this.Children = new List<OrganizationalUnit>();
            this.Name = name;
            this.Parent = parent;
            parent?.Children.Add(this);
        }

        public int Id { get; set; }

        public OrganizationalUnit Parent { get; set; }

        public string Name { get; set; }

        public IList<OrganizationalUnit> Children { get; set; }

        public IList<ApplicationRental> ApplicationRentals { get; set; }
    }
}

namespace Four2n.JustApp.Domain.Organizational
{
    public class OrganizationalUnit
    {
        public int Id { get; set; }

        public OrganizationalUnit Parent { get; set; }

        public string Name { get; set; }
    }
}

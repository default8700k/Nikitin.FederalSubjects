namespace Nikitin.FederalSubjects.Database.Models.DbModels
{
    public partial class FederalSubject
    {
        public FederalSubject()
        {
            Map = new HashSet<Map>();
        }

        public short Id { get; set; }
        public short FederalDistrictId { get; set; }
        public short FederalSubjectTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }

        public virtual FederalDistrict FederalDistrict { get; set; } = null!;
        public virtual FederalSubjectType FederalSubjectType { get; set; } = null!;
        public virtual ICollection<Map> Map { get; set; }
    }
}

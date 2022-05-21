namespace Nikitin.FederalSubjects.Database.Models.DbModels
{
    public partial class FederalSubjectType
    {
        public FederalSubjectType()
        {
            FederalSubjects = new HashSet<FederalSubject>();
        }

        public short Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<FederalSubject> FederalSubjects { get; set; }
    }
}

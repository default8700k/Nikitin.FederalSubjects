namespace Nikitin.FederalSubjects.Database.Models.DbModels
{
    public partial class Map
    {
        public short Id { get; set; }
        public short FederalSubjectId { get; set; }
        public string? Path { get; set; }

        public virtual FederalSubject FederalSubject { get; set; } = null!;
    }
}

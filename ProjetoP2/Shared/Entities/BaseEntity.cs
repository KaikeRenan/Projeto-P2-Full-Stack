namespace ProjetoP2.Shared.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RemovedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}

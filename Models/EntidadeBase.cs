namespace EbookStore.Models
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }


        public void UpdateDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }

}

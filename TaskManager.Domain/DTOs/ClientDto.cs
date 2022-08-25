using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs
{
    public sealed class ClientDto : Entity
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Note { get; set; }
    }
}
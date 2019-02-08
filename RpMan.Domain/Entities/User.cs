using RpMan.Domain.ValueObjects;

namespace RpMan.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public AdAccount AdAccount { get; set; }
    }
}

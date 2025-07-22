using System.ComponentModel.DataAnnotations;

namespace FlexPerks.Domain.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoucairTest.Domain.Entities.Base;

public class EntityBase<T> : DomainEntity, IEntityBase<T>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; } = default!;

}
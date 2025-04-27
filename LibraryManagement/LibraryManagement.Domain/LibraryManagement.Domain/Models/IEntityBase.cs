namespace LibraryManagement.Domain.Models;

public interface IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
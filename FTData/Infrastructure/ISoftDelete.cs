namespace FTData.Infrastructure
{
    public interface ISoftDelete
    {
         bool IsDeleted { get; set; }
    }
}

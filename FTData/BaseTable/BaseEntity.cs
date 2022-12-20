using FTData.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.BaseTable
{
    public abstract class BaseTrackable<T> : ISoftDelete
    {
        
        [Key]        
        public virtual T Id { get; set; }

        [NotMapped]
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public string UpdatedBy { get; set; }
        [NotMapped]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public string DeletedBy { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public DateTime? DeletedDate { get; set; }
    }
    public abstract class BasePrimaryKeyHolder<T>
    {
        [Key]
        public T Id { get; set; }
    }
    public abstract class BasePrimaryKey<T> : ISoftDelete
    {
        [Key]
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}

namespace ClosDotNet.Domain.Base.Model
{
    using System;

    public interface IValueObject
    {
        int Id { get; set; }

        int VersionNumber { get; set; }

        string CreateBy { get; set; }

        DateTime CreationDate { get; set; }

        string LastUpdateBy { get; set; }

        DateTime LastUpdateDate { get; set; }
    }

    [Serializable()]
    public abstract class ValueObject : IValueObject
    {
        public ValueObject()
        {
            VersionNumber = 0;
            CreateBy = "SML";
            CreationDate = DateTime.Now;
            LastUpdateBy = "SML";
            LastUpdateDate = DateTime.Now;
        }

        public virtual int Id { get; set; }

        public virtual int VersionNumber { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual string LastUpdateBy { get; set; }

        public virtual DateTime LastUpdateDate { get; set; }
    }
}
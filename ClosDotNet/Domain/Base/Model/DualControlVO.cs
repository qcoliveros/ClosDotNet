namespace ClosDotNet.Domain.Base.Model
{
    using System;

    public interface IDualControlVO : IValueObject
    {
        int? MasterId { get; set; }

        string Deprecated { get; set; }

        string Status { get; set; }
    }

    [Serializable()]
    public abstract class DualControlVO : ValueObject, IDualControlVO
    {
        public DualControlVO () 
            : base ()
        {
        }

        public virtual int? MasterId { get; set; }

        public virtual string Deprecated { get; set; }

        public virtual string Status { get; set; }
    }
}
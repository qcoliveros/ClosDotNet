namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.Model;
    using System;

    public interface ICodeValueVO : IDualControlVO
    {
        string Code { get; set; }

        string Description { get; set; }

        int CodeSetId { get; set; }

        int DisplayOrder { get; set; }

        string ExtValue1 { get; set; }

        string ExtValue2 { get; set; }

        string ExtValue3 { get; set; }

        string ExtValue4 { get; set; }

        int ParentId { get; set; }

        string ParentCode { get; set; }
    }

    [Serializable()]
    public class CodeValueVO : DualControlVO, ICodeValueVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate"
        };

        public CodeValueVO() 
            : base ()
        {
        }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }

        public virtual int CodeSetId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual string ExtValue1 { get; set; }

        public virtual string ExtValue2 { get; set; }

        public virtual string ExtValue3 { get; set; }

        public virtual string ExtValue4 { get; set; }

        public virtual int ParentId { get; set; }

        public virtual string ParentCode { get; set; }
    }
}
namespace ClosDotNet.Domain.User
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.CodeSet;
    using System;

    public interface IUserVO : IDualControlVO
    {
        string LoginId { get; set; }
        
        string FullName { get; set; }

        ICodeValueVO Designation { get; set; }
    }

    [Serializable()]
    public class UserVO : DualControlVO, IUserVO
    {
        public UserVO()
            : base()
        {
        }

        public virtual string LoginId { get; set; }

        public virtual string FullName { get; set; }

        public virtual ICodeValueVO Designation { get; set; }
    }
}
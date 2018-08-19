namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.Model;

    public interface IActionTypeVO : IValueObject
    {
        string Code { get; set; }

        string Description { get; set; }
    }

    public class ActionTypeVO : ValueObject, IActionTypeVO
    {
        public ActionTypeVO()
        {
        }

        public ActionTypeVO(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }
    }
}
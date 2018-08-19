namespace ClosDotNet.Helper
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;

    public enum DisplayMode
    {
        Edit,
        View
    }

    public enum MessageType
    {
        Success,
        Error
    }

    public enum AddressType
    {
        [Description("B")]
        Business,
        [Description("H")]
        Home,
        [Description("M")]
        Mailing
    }

    public enum ActionType
    {
        [Description("Save")]
        Save,
        [Description("Process")]
        Process,
        [Description("Submit")]
        Submit,
        [Description("Cancel")]
        Cancel,
        [Description("Add New")]
        AddNew,
        [Description("Remove")]
        Remove
    }

    public enum TaskViewType
    {
        Draft,
        Submitted
    }

    public enum TaskStatus
    {
        [Description("COMPLETED")]
        COMPLETED,
        [Description("IN PROGRESS")]
        IN_PROGRESS
    }

    public enum UserRole
    {
        [Description("RM")]
        RM,
        [Description("RMTL")]
        RMTL,
        [Description("CA")]
        CA
    }

    public enum Country
    {
        [Description("SG")]
        Singapore
    }

    public enum Currency
    {
        [Description("SGD")]
        SingaporeDollar
    }

    public enum CustomerType
    {
        [Description("I")]
        Individual,
        [Description("C")]
        Corporate
    }

    public enum CodeSetCode
    {
        [Description("COUNTRY")]
        COUNTRY,
        [Description("CURRENCY")]
        CURRENCY,
        [Description("CALL_PURPOSE")]
        CALL_PURPOSE,
        [Description("ID_TYPE")]
        ID_TYPE,
        [Description("BORROWER_TYPE")]
        BORROWER_TYPE,
        [Description("CLASSIFICATION")]
        CLASSIFICATION,
        [Description("SALUTATION")]
        SALUTATION,
        [Description("GENDER")]
        GENDER,
        [Description("MARITAL_STATUS")]
        MARITAL_STATUS,
        [Description("RACE")]
        RACE,
        [Description("EDUCATION_LEVEL")]
        EDUCATION_LEVEL,
        [Description("RES_OWNER")]
        RESIDENTIAL_OWNERSHIP,
        [Description("RES_TYPE")]
        RESIDENTIAL_TYPE,
        [Description("ADDRESS_FORMAT")]
        ADDRESS_FORMAT
    }

    public class Constants
    {
        private static StringDictionary enumDictionary = new StringDictionary();

        public static string GetEnumDescription<EnumType>(EnumType @enum)
        {
            Type enumType = @enum.GetType();
            string key = enumType.ToString() + "___" + @enum.ToString();
            if (enumDictionary[key] == null)
            {
                FieldInfo info = enumType.GetField(@enum.ToString());
                if (info != null)
                {
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])
                        info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                    {
                        enumDictionary[key] = attributes[0].Description;

                        return enumDictionary[key];
                    }
                }

                enumDictionary[key] = @enum.ToString();
            }

            return enumDictionary[key];
        }
    }

}
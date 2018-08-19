namespace ClosDotNet.Filters
{
    using AutoMapper;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.User;
    using Common.Logging;
    using System;
    using System.Globalization;

    public class NullableDateTimeToStringResolver : ValueResolver<DateTime?, string>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NullableDateTimeToStringResolver));

        protected override string ResolveCore(DateTime? source)
        {
            Logger.Debug("NullableDateTimeToStringResolver|Source: " + source);

            if (source.HasValue)
            {
                Logger.Debug("NullableDateTimeToStringResolver|Value: " + source.Value.ToString("dd'/'MM'/'yyyy"));

                return source.Value.ToString("dd'/'MM'/'yyyy");
            }
            
            return string.Empty;
        }
    }

    public class StringToNullableDateTimeResolver : ValueResolver<string, DateTime?>
    {
        protected override DateTime? ResolveCore(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return DateTime.ParseExact(source, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            return default(DateTime?);
        }
    }

    public class StringToBoolResolver : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return ("Y".Equals(source) ? true : false);
        }
    }

    public class BoolToStringResolver : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return (source ? "Y" : "N");
        }
    }

    public class StringToCodeValueResolver : ValueResolver<string, CodeValueVO>
    {
        protected override CodeValueVO ResolveCore(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return new CodeValueVO { Id = Int32.Parse(source) };
            }
            
            return null;
        }
    }

    public class StringToCountryResolver : ValueResolver<string, CountryVO>
    {
        protected override CountryVO ResolveCore(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return new CountryVO { Code = source };
            }

            return null;
        }
    }

    public class StringToCurrencyResolver : ValueResolver<string, CurrencyVO>
    {
        protected override CurrencyVO ResolveCore(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return new CurrencyVO { Code = source };
            }

            return null;
        }
    }

    public class StringToUserResolver : ValueResolver<string, UserVO>
    {
        protected override UserVO ResolveCore(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return new UserVO { Id = Int32.Parse(source) };
            }

            return null;
        }
    }

    public class AgeResolver : ValueResolver<DateTime?, int>
    {
        protected override int ResolveCore(DateTime? source)
        {
            if (source.HasValue)
            {
                return (DateTime.Now.Year - source.Value.Year);
            }

            return 0;
        }
    }
}
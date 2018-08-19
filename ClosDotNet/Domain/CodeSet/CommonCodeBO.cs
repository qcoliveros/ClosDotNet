namespace ClosDotNet.Domain.CodeSet
{
    using System.Collections.Generic;

    public interface ICommonCodeBO
    {
        IList<ICountryVO> RetrieveAllCountries();

        IList<ICurrencyVO> RetrieveAllCurrencies();

        IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode);

        IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode, string referenceCode);
    }

    public class CommonCodeBOImpl : ICommonCodeBO
    {
        private static IList<ICountryVO> countryCache = new List<ICountryVO>();
        private static IList<ICurrencyVO> currencyCache = new List<ICurrencyVO>();
        private static IDictionary<string, IList<ICodeValueVO>> codeValueCache = new Dictionary<string, IList<ICodeValueVO>>();

        public ICountryDAO CountryDAO { private get; set; }
        public ICurrencyDAO CurrencyDAO { private get; set; }
        public ICodeValueADO CodeValueADO { private get; set; }

        public IList<ICountryVO> RetrieveAllCountries()
        {
            if (countryCache.Count == 0)
            {
                countryCache = CountryDAO.RetrieveAllCountries();
            }

            return countryCache;
        }

        public IList<ICurrencyVO> RetrieveAllCurrencies()
        {
            if (currencyCache.Count == 0)
            {
                currencyCache = CurrencyDAO.RetrieveAllCurrencies();
            }

            return currencyCache;
        }

        public IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode)
        {
            IList<ICodeValueVO> codeValueList = new List<ICodeValueVO>();
            if (!codeValueCache.ContainsKey(codeSetCode))
            {
                codeValueList = CodeValueADO.RetrieveCommonCodesByCodeSet(codeSetCode);
                codeValueCache.Add(codeSetCode, codeValueList);
            }
            else
            {
                codeValueCache.TryGetValue(codeSetCode, out codeValueList);
            }

            return codeValueList;
        }

        public IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode, string referenceCode)
        {
            IList<ICodeValueVO> codeValueList = new List<ICodeValueVO>();
            string key = codeSetCode + referenceCode;

            if (!codeValueCache.ContainsKey(key))
            {
                codeValueList = CodeValueADO.RetrieveCommonCodesByCodeSetAndRefCode(codeSetCode, referenceCode);
                codeValueCache.Add(key, codeValueList);
            }
            else
            {
                codeValueCache.TryGetValue(key, out codeValueList);
            }

            return codeValueList;
        }
    }
}
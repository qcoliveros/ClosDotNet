namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Helper;
    using ClosDotNet.Mapper;
    using Common.Logging;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CommonController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonController));

        public ICommonCodeBO CommonCodeBO { get; set; }

        #region Utility Method
        protected IList<SelectListItem> GetDropdownList(CodeSetCode code, IMapper mapper)
        {
            return GetDropdownList(code, null, mapper);
        }

        protected IList<SelectListItem> GetDropdownList(CodeSetCode code, string referenceCode, IMapper mapper)
        {
            switch (code)
            {
                case CodeSetCode.COUNTRY:
                    IList<ICountryVO> countryList = CommonCodeBO.RetrieveAllCountries();
                    Logger.Debug("GetDropdownList|Country DDL: " + (countryList != null ? countryList : null));

                    return (IList<SelectListItem>)
                        mapper.Map(countryList, typeof(IList<ICountryVO>), typeof(IList<SelectListItem>));

                case CodeSetCode.CURRENCY:
                    IList<ICurrencyVO> currencyList = CommonCodeBO.RetrieveAllCurrencies();
                    Logger.Debug("GetDropdownList|Currency DDL: " + (currencyList != null ? currencyList : null));

                    return (IList<SelectListItem>)
                        mapper.Map(currencyList, typeof(IList<ICurrencyVO>), typeof(IList<SelectListItem>));

                default:
                    IList<ICodeValueVO> codeList = null;

                    if (!string.IsNullOrEmpty(referenceCode))
                    {
                        codeList = CommonCodeBO.RetrieveCommonCodesByCodeSet(Constants.GetEnumDescription(code), referenceCode);
                    }
                    else
                    {
                        codeList = CommonCodeBO.RetrieveCommonCodesByCodeSet(Constants.GetEnumDescription(code));
                    }

                    Logger.Debug("GetDropdownList|Common Code DDL: " + (codeList != null ? codeList : null));

                    return (IList<SelectListItem>)
                        mapper.Map(codeList, typeof(IList<ICodeValueVO>), typeof(IList<SelectListItem>));
            }
        }
        #endregion Utility Method
    }
}
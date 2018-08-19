namespace ClosDotNet.Controllers
{
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Filters;
    using ClosDotNet.Helper;
    using ClosDotNet.Mapper;
    using ClosDotNet.Models;
    using ClosDotNet.Resources.Resources;
    using Common.Logging;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Authorize]
    public class CustomerController : CommonController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerController));

        public IUserBO UserBO { get; set; }
        public ICustomerBO CustomerBO { get; set; }
        public IMapper CustomerMapper { get; set; }

        public CustomerController()
        {
        }

        #region Search
        public ActionResult Search()
        {
            Session.Clear();
            TempData.Clear();

            ViewBag.IdTypeList = GetDropdownList(CodeSetCode.ID_TYPE, CustomerMapper);
            ViewBag.CountryList = GetDropdownList(CodeSetCode.COUNTRY, CustomerMapper);

            return View(new SearchViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByCustomerName(SearchViewModel searchModel)
        {
            Logger.Debug("SearchByCustomerName|Search customer by name: " 
                + searchModel.SearchCustomerName.CustomerName);

            IList<ICustomerVO> customerList = 
                CustomerBO.RetrieveCustomersByName(searchModel.SearchCustomerName.CustomerName);
            Logger.Debug("SearchByCustomerName|Search Result: " + (customerList != null ? customerList.Count : 0));

            TempData["SearchResultList"] = customerList;
            return RedirectToAction("ViewSearchResult");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByCifNumber(SearchViewModel searchModel)
        {
            Logger.Debug("SearchByCifNumber|Search CIF number: " + searchModel.SeachCifNumber.CifNumber);
            IList<ICustomerVO> customerList = new List<ICustomerVO>();

            ICustomerVO customer = CustomerBO.RetrieveCustomerByCifNumber(searchModel.SeachCifNumber.CifNumber);
            if (customer != null)
            {
                customerList.Add(customer);
            }

            Logger.Debug("SearchByCifNumber|Search Result: " + (customerList != null ? customerList.Count : 0));

            TempData["SearchResultList"] = customerList;
            return RedirectToAction("ViewSearchResult");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByIdCombination(SearchViewModel searchModel)
        {
            Logger.Debug("SearchByIdCombination|Search ID combination: " +
                searchModel.SeachIdCombination.IdType + "|" +
                searchModel.SeachIdCombination.IdNumber + "|" +
                searchModel.SeachIdCombination.IdIssuedCountry);
            IList<ICustomerVO> customerList = new List<ICustomerVO>();

            ICustomerVO customer = CustomerBO.RetrieveCustomerByIdCombination(
                searchModel.SeachIdCombination.IdType,
                searchModel.SeachIdCombination.IdNumber,
                searchModel.SeachIdCombination.IdIssuedCountry);
            if (customer != null)
            {
                customerList.Add(customer);
            }

            Logger.Debug("SearchByIdCombination|Search Result: " + (customerList != null ? customerList.Count : 0));

            TempData["SearchResultList"] = customerList;
            return RedirectToAction("ViewSearchResult");
        }

        public ActionResult ViewSearchResult()
        {
            IList<ICustomerVO> customerList = (IList<ICustomerVO>) TempData["SearchResultList"];
            IEnumerable<SearchResultViewModel> model = (IEnumerable<SearchResultViewModel>)
                CustomerMapper.Map(customerList,
                    typeof(IList<ICustomerVO>), typeof(IEnumerable<SearchResultViewModel>));

            return View(model);
        }
        #endregion Search

        #region Detail
        [Button(ButtonName = "PrepareCreateCompany")]
        [ActionName("PrepareCreateCustomer")]
        public ActionResult PrepareCreateCompanyCustomer()
        {
            ICompanyCustomerVO customer = new CompanyCustomerVO();
            // Default values for new customer.
            customer.IdIssuedCountry = new CountryVO() { Code = Constants.GetEnumDescription(Country.Singapore) };
            customer.CustomerType = Constants.GetEnumDescription(CustomerType.Corporate);
            customer.RelationshipManager = UserBO.RetrieveUserByLoginId(User.Identity.Name);
            customer.AddressList = new List<IAddressVO>();
            customer.AddressList.Add(new AddressVO() { AddressType = Constants.GetEnumDescription(AddressType.Business) });
            customer.AddressList.Add(new AddressVO() { AddressType = Constants.GetEnumDescription(AddressType.Mailing) });

            Session["SessionCustomer"] = customer;
            return RedirectToAction("ViewCompanyDetails");
        }

        [Button(ButtonName = "PrepareCreateIndividual")]
        [ActionName("PrepareCreateCustomer")]
        public ActionResult PrepareCreateIndividualCustomer()
        {
            IIndividualCustomerVO customer = new IndividualCustomerVO();
            // Default values for new customer.
            customer.IdIssuedCountry = new CountryVO() { Code = Constants.GetEnumDescription(Country.Singapore) };
            customer.CustomerType = Constants.GetEnumDescription(CustomerType.Individual);
            customer.RelationshipManager = UserBO.RetrieveUserByLoginId(User.Identity.Name);
            customer.AddressList = new List<IAddressVO>();
            customer.AddressList.Add(new AddressVO() { AddressType = Constants.GetEnumDescription(AddressType.Home) });
            customer.AddressList.Add(new AddressVO() { AddressType = Constants.GetEnumDescription(AddressType.Mailing) });

            Session["SessionCustomer"] = customer;
            return RedirectToAction("ViewIndividualDetails");
        }

        public ActionResult ViewCustomer(int? customerId, string customerType)
        {
            Logger.Debug("ViewCustomer|Selected Customer ID: " + customerId);

            if (customerId != null)
            {
                if (Constants.GetEnumDescription(CustomerType.Individual).Equals(customerType))
                {
                    Session["SessionCustomer"] = 
                        (IIndividualCustomerVO)CustomerBO.RetrieveIndividualCustomer(customerId ?? default(int));
                }
                else
                {
                    Session["SessionCustomer"] =
                        (ICompanyCustomerVO)CustomerBO.RetrieveCompanyCustomer(customerId ?? default(int));
                }
            }
            
            if (Constants.GetEnumDescription(CustomerType.Individual).Equals(customerType))
            {
                return RedirectToAction("ViewIndividualDetails");
            }   

            return RedirectToAction("ViewCompanyDetails");
        }

        public ActionResult ViewCompanyDetails()
        {
            CustomerViewModel model = (CompanyCustomerViewModel) TempData["CustomerDetailModel"];
            if (model == null)
            {
                ICustomerVO customer = (ICompanyCustomerVO)Session["SessionCustomer"];
                if (customer != null)
                {
                    model = (CompanyCustomerViewModel)
                        CustomerMapper.Map(customer, typeof(ICompanyCustomerVO), typeof(CompanyCustomerViewModel));
                    TempData["CustomerDetailModel"] = model;
                }
            }

            PrepareOptionsList(CustomerType.Corporate);            
            ViewBag.DisplayMode = DisplayMode.Edit;
            if (User.IsInRole(Constants.GetEnumDescription(UserRole.CA)))
            {
                ViewBag.DisplayMode = DisplayMode.View;
            }

            return View(model);
        }

        public ActionResult ViewIndividualDetails()
        {
            CustomerViewModel model = (IndividualCustomerViewModel) TempData["CustomerDetailModel"];
            if (model == null)
            {
                ICustomerVO customer = (IIndividualCustomerVO)Session["SessionCustomer"];
                if (customer != null)
                {
                    model = (IndividualCustomerViewModel)
                        CustomerMapper.Map(customer, typeof(IIndividualCustomerVO), typeof(IndividualCustomerViewModel));
                    TempData["CustomerDetailModel"] = model;
                }
            }

            PrepareOptionsList(CustomerType.Individual);
            ViewBag.DisplayMode = DisplayMode.Edit;
            if (User.IsInRole(Constants.GetEnumDescription(UserRole.CA)))
            {
                ViewBag.DisplayMode = DisplayMode.View;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCompanyCustomer(CompanyCustomerViewModel model, ActionType actionType)
        {
            Logger.Debug("SaveCompanyCustomer|Action type: " + actionType);

            if (actionType == ActionType.Save)
            {
                try
                {
                    if (CustomerBO.CheckIfIdCombinationExists(
                        Int32.Parse(model.IdType), model.IdNumber, model.IdIssuedCountry, model.Id))
                    {
                        TempData["MessageType"] = MessageType.Error;
                        TempData["MessageDescription"] = CommonResources.MessageIdAlreadyExists;

                        TempData["CustomerDetailModel"] = model;
                        return RedirectToAction("ViewCustomerDetails");
                    }

                    ICompanyCustomerVO customer = (CompanyCustomerVO)
                        CustomerMapper.Map(model, typeof(CompanyCustomerViewModel), typeof(CompanyCustomerVO));
                    customer.LastUpdateBy = User.Identity.Name;

                    if (customer.Id == 0)
                    {
                        customer = CustomerBO.CreateCompanyCustomer(customer);
                    }
                    else
                    {
                        ICompanyCustomerVO sessionCustomer = (ICompanyCustomerVO)Session["SessionCustomer"];
                        customer = CustomerBO.UpdateCompanyCustomer(sessionCustomer, customer);
                    }

                    model = (CompanyCustomerViewModel)
                        CustomerMapper.Map(customer, typeof(ICompanyCustomerVO), typeof(CompanyCustomerViewModel));

                    Session["SessionCustomer"] = customer;
                    TempData["MessageType"] = MessageType.Success;
                    TempData["MessageDescription"] = CommonResources.MessageSaveSuccess;
                }
                catch (Exception exception)
                {
                    Logger.Debug("Exception encountered: " + exception.StackTrace);

                    TempData["MessageType"] = MessageType.Error;
                    TempData["MessageDescription"] = CommonResources.MessageSaveError + exception.Message;
                }

                TempData["CustomerDetailModel"] = model;
                return RedirectToAction("ViewCompanyDetails");
            }

            return RedirectToAction("Welcome", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveIndividualCustomer(IndividualCustomerViewModel model, ActionType actionType)
        {
            Logger.Debug("SaveIndividualCustomer|Action type: " + actionType);

            if (actionType == ActionType.Save)
            {
                try
                {
                    if (CustomerBO.CheckIfIdCombinationExists(
                        Int32.Parse(model.IdType), model.IdNumber, model.IdIssuedCountry, model.Id))
                    {
                        TempData["MessageType"] = MessageType.Error;
                        TempData["MessageDescription"] = CommonResources.MessageIdAlreadyExists;

                        TempData["CustomerDetailModel"] = model;
                        return RedirectToAction("ViewCustomerDetails");
                    }

                    IIndividualCustomerVO customer = (IndividualCustomerVO)
                        CustomerMapper.Map(model, typeof(IndividualCustomerViewModel), typeof(IndividualCustomerVO));
                    customer.LastUpdateBy = User.Identity.Name;

                    if (customer.Id == 0)
                    {
                        customer = CustomerBO.CreateIndividualCustomer(customer);
                    }
                    else
                    {
                        IIndividualCustomerVO sessionCustomer = (IIndividualCustomerVO)Session["SessionCustomer"];
                        customer = CustomerBO.UpdateIndividualCustomer(sessionCustomer, customer);
                    }

                    model = (IndividualCustomerViewModel)
                        CustomerMapper.Map(customer, typeof(IIndividualCustomerVO), typeof(IndividualCustomerViewModel));

                    Session["SessionCustomer"] = customer;
                    TempData["MessageType"] = MessageType.Success;
                    TempData["MessageDescription"] = CommonResources.MessageSaveSuccess;
                }
                catch (Exception exception)
                {
                    Logger.Debug("Exception encountered: " + exception.StackTrace);

                    TempData["MessageType"] = MessageType.Error;
                    TempData["MessageDescription"] = CommonResources.MessageSaveError + exception.Message;
                }

                TempData["CustomerDetailModel"] = model;
                return RedirectToAction("ViewIndividualDetails");
            }

            return RedirectToAction("Welcome", "Home");
        }
        #endregion Detail

        private void PrepareOptionsList(CustomerType customerType)
        {
            // Prepare Customer Details DDL.
            ViewBag.IdTypeList = GetDropdownList(
                CodeSetCode.ID_TYPE, Constants.GetEnumDescription(customerType), CustomerMapper);
            ViewBag.CountryList = GetDropdownList(CodeSetCode.COUNTRY, CustomerMapper);            
            ViewBag.ClassificationList = GetDropdownList(CodeSetCode.CLASSIFICATION, CustomerMapper);
            // Prepare Address Details DDL.
            ViewBag.AddressFormatList = GetDropdownList(CodeSetCode.ADDRESS_FORMAT, CustomerMapper);
            
            if (customerType == CustomerType.Individual)
            {
                // Prepare Customer Details DDL.
                ViewBag.SalutationList = GetDropdownList(CodeSetCode.SALUTATION, CustomerMapper);
                ViewBag.GenderList = GetDropdownList(CodeSetCode.GENDER, CustomerMapper);
                ViewBag.MaritalStatusList = GetDropdownList(CodeSetCode.MARITAL_STATUS, CustomerMapper);
                ViewBag.RaceList = GetDropdownList(CodeSetCode.RACE, CustomerMapper);
                ViewBag.EducationLevelList = GetDropdownList(CodeSetCode.EDUCATION_LEVEL, CustomerMapper);
                // Prepare Address Details DDL.
                ViewBag.ResidentialOwnershipList = GetDropdownList(CodeSetCode.RESIDENTIAL_OWNERSHIP, CustomerMapper);
                ViewBag.ResidentialTypeList = GetDropdownList(CodeSetCode.RESIDENTIAL_TYPE, CustomerMapper);
            }
            else
            {
                ViewBag.ConstitutionList = GetDropdownList(CodeSetCode.BORROWER_TYPE, CustomerMapper);
            }
        }
    }
}
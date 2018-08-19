namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.ORM;
    using Spring.Data.Common;
    using Spring.Data.Generic;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface ICodeValueADO
    {
        IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode);

        IList<ICodeValueVO> RetrieveCommonCodesByCodeSetAndRefCode(string codeSetCode, string referenceCode);
    }

    [Repository]
    public class CodeValueADOImpl : AdoDaoSupport, ICodeValueADO
    {
        private static readonly string RETRIEVE_CODE_VALUE_BY_CODE_SET = @" SELECT "
            + " cv.id, "
            + " cv.code, "
            + " cv.code_value "
            + " FROM code_value cv INNER JOIN code_set cs ON cv.code_set_value_id = cs.id "
            + " AND cs.status = 'ACTIVE' AND cs.deprecated = 'N' AND cs.master_id IS NULL "
            + " AND cv.status = 'ACTIVE' AND cv.deprecated = 'N' AND cv.master_id IS NULL "
            + " WHERE cs.code = @CodeSetCode ";

        private static readonly string SORT_CODE_VALUE = @" ORDER BY cv.display_order, cv.code ";

        [Transaction(ReadOnly = true)]
        public IList<ICodeValueVO> RetrieveCommonCodesByCodeSet(string codeSetCode)
        {
            if (!String.IsNullOrEmpty(codeSetCode))
            {
                string query = RETRIEVE_CODE_VALUE_BY_CODE_SET + SORT_CODE_VALUE;

                return (IList<ICodeValueVO>)
                    AdoTemplate.QueryWithRowMapperDelegate(
                        CommandType.Text,
                        query,
                        delegate(IDataReader dataReader, int rowNum)
                        {
                            ICodeValueVO codeValue = new CodeValueVO();
                            codeValue.Id = dataReader.GetSafeInt32("id");
                            codeValue.Code = dataReader.GetSafeString("code");
                            codeValue.Description = dataReader.GetSafeString("code_value");

                            return codeValue;
                        },
                        "CodeSetCode",
                        DbType.String,
                        50,
                        codeSetCode
                    );
            }

            return null;
        }

        [Transaction(ReadOnly = true)]
        public IList<ICodeValueVO> RetrieveCommonCodesByCodeSetAndRefCode(string codeSetCode, string referenceCode)
        {
            if (!String.IsNullOrEmpty(codeSetCode) && !String.IsNullOrEmpty(referenceCode))
            {
                string query = RETRIEVE_CODE_VALUE_BY_CODE_SET
                    + " AND cv.code_value_parent_code = @ReferenceCode "
                    + SORT_CODE_VALUE;

                IDbParametersBuilder builder = CreateDbParametersBuilder();
                builder.Create().Name("CodeSetCode").Type(DbType.String).Size(50).Value(codeSetCode);
                builder.Create().Name("ReferenceCode").Type(DbType.String).Size(50).Value(referenceCode);

                return (IList<ICodeValueVO>)
                    AdoTemplate.QueryWithRowMapperDelegate(
                        CommandType.Text,
                        query,
                        delegate(IDataReader dataReader, int rowNum)
                        {
                            ICodeValueVO codeValue = new CodeValueVO();
                            codeValue.Id = dataReader.GetSafeInt32("id");
                            codeValue.Code = dataReader.GetSafeString("code");
                            codeValue.Description = dataReader.GetSafeString("code_value");

                            return codeValue;
                        },
                        builder.GetParameters()
                    );
            }

            return null;
        }
    }
}
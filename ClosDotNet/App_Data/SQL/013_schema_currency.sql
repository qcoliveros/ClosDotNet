CREATE SEQUENCE [clos].[sci_currency_seq] START WITH 1 INCREMENT BY 1;
  
CREATE TABLE [clos].[sci_currency] (
    [id]                 INT           DEFAULT (NEXT VALUE FOR [clos].[sci_currency_seq]) NOT NULL,
    [cur_crrncy_iso_code] NVARCHAR (4)  NOT NULL,
    [cur_crrncy_desc]     NVARCHAR (100) NOT NULL,
    [cur_cntry_iso_code] NVARCHAR (4)  NOT NULL,
    CONSTRAINT [sci_currency_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
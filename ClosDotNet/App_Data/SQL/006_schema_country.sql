CREATE SEQUENCE [clos].[sci_country_seq] START WITH 1 INCREMENT BY 1;
  
CREATE TABLE [clos].[sci_country] (
    [id]                 INT           DEFAULT (NEXT VALUE FOR [clos].[sci_country_seq]) NOT NULL,
    [ctr_cntry_iso_code] NVARCHAR (3)  NOT NULL,
    [ctr_cntry_name]     NVARCHAR (60) NOT NULL,
    CONSTRAINT [sci_country_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
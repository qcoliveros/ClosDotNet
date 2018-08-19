CREATE SEQUENCE [clos].[sml_business_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_business] (
    [id]                         INT             DEFAULT (NEXT VALUE FOR [clos].[sml_business_seq]) NOT NULL,
    [version_time]               INT             DEFAULT ((0)) NOT NULL,
    [create_by]                  NVARCHAR (20)   NOT NULL,
    [creation_date]              DATETIME        NOT NULL,
    [last_update_by]             NVARCHAR (20)   NOT NULL,
    [last_update_date]           DATETIME        NOT NULL,
    [customer_id]                INT             NULL,
    [cur_paid_up_capital_ccy]    CHAR (3)        NULL,
    [cur_paid_up_capital]        DECIMAL (19, 2) NULL,
    [customer_management]        VARCHAR (MAX)   NULL,
    [customer_background]        VARCHAR (MAX)   NULL,
    [customer_business_activity] VARCHAR (MAX)   NULL,
    [customer_account_strategy]  VARCHAR (MAX)   NULL,
    CONSTRAINT [sml_business_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
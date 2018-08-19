CREATE TABLE [clos].[sml_company_customer] (
    [id]                            INT           NOT NULL,
    [create_by]                     NVARCHAR (20) NOT NULL,
    [creation_date]                 DATETIME      NOT NULL,
    [last_update_by]                NVARCHAR (20) NOT NULL,
    [last_update_date]              DATETIME      NOT NULL,
    [borrower_type_id]              INT           NULL,
    [mandatory_info_unavail_ind]    CHAR (1)      NULL,
    [incorporation_country]             CHAR (2)      NULL,
    [incorporation_date]             DATETIME      NULL,
    [operation_country]       CHAR (2)      NULL,
    [more_than_50_prct_turnovr_ind] CHAR (1)      NULL,
    CONSTRAINT [sml_company_customer_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);


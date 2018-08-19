CREATE TABLE [clos].[sml_individual_customer] (
    [id]                        INT           NOT NULL,
    [create_by]                 NVARCHAR (20) NOT NULL,
    [creation_date]             DATETIME      NOT NULL,
    [last_update_by]            NVARCHAR (20) NOT NULL,
    [last_update_date]          DATETIME      NOT NULL,
    [with_bank_since_date]      DATETIME      NULL,
    [first_credit_granted_date] DATETIME      NULL,
    [salutation_id]             INT           NULL,
    [old_salutation_id]         INT           NULL,
    [gender_id]                 INT           NULL, 
    [marital_status_id]         INT           NULL,
    [date_of_birth]             DATETIME      NULL,
    [no_of_dependents]          INT           NULL,
    [country_of_citizenship] CHAR (2)      NULL,
    [country_of_residency]   CHAR (2)      NULL,
    [pr_status]                 CHAR (1)      NULL,
    [race_id]                   INT           NULL,
    [highest_education_id]      INT           NULL,
    CONSTRAINT [sml_individual_customer_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
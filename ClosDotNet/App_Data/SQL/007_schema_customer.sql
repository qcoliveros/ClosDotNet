CREATE SEQUENCE [clos].[sml_customer_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_customer] (
    [id]                      INT            DEFAULT (NEXT VALUE FOR [clos].[sml_customer_seq]) NOT NULL,
    [version_time]            INT            DEFAULT ((0)) NOT NULL,
    [create_by]               NVARCHAR (20)  NOT NULL,
    [creation_date]           DATETIME       NOT NULL,
    [last_update_by]          NVARCHAR (20)  NOT NULL,
    [last_update_date]        DATETIME       NOT NULL,
    [customer_name]           NVARCHAR (150) NULL,
    [old_customer_name]       NVARCHAR(150) NULL, 
    [cif_number]              NVARCHAR (20)  NULL,
    [id_type_id]              INT            NULL,
    [id_number]               NVARCHAR (50)  NULL,
    [id_country]              CHAR (2)       NULL,
    [customer_type]           CHAR (1)       NOT NULL,
    [backend_ind]             CHAR (1)       NULL,
    [status]                  NVARCHAR (20)  NULL,
    [rm_id]                   INT            NULL,
    [classification_id]       INT            NULL,
    [acc_created_date]        DATETIME       NULL,
    [relationship_since_year] INT            NULL,
    [facility_granted_date]   DATETIME       NULL,
    [hold_mail_ind]           CHAR (1)       NULL,
    CONSTRAINT [sml_customer_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
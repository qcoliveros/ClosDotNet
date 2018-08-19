CREATE SEQUENCE [clos].[sml_customer_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_customer] (
    [id]                 INT            DEFAULT (NEXT VALUE FOR [clos].[sml_customer_seq]) NOT NULL,
    [version_time]       INT            DEFAULT ((0)) NOT NULL,
    [create_by]          NVARCHAR (20)  NOT NULL,
    [creation_date]      DATETIME       NOT NULL,
    [last_update_by]     NVARCHAR (20)  NOT NULL,
    [last_update_date]   DATETIME       NOT NULL,
    [customer_name]      NVARCHAR (150) NULL,
    [host_customer_name] NVARCHAR (150) NULL,
    [cif_number]         NVARCHAR (20)  NULL,
    [id_type_id]         INT            NULL,
    [id_number]          NVARCHAR (50)  NULL,
    [id_country]         CHAR (2)       NULL,
    [id_issued_place]    NVARCHAR (150) NULL,
    [id_issued_date]     DATETIME       NULL,
    [id_expiry_date]     DATETIME       NULL,
    [customer_type]      CHAR (1)       NOT NULL,
    [backend_ind]        CHAR (1)       NULL,
    [status]             NVARCHAR (20)  NULL,
    [rm_id]              INT            NULL,
    CONSTRAINT [sml_customer_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
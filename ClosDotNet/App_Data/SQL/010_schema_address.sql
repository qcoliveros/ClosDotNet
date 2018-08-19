CREATE SEQUENCE [clos].[sml_address_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_address] (
    [id]                    INT            DEFAULT (NEXT VALUE FOR [clos].[sml_address_seq]) NOT NULL,
    [version_time]          INT            NOT NULL,
    [create_by]             NVARCHAR (20)  NOT NULL,
    [creation_date]         DATETIME       NOT NULL,
    [last_update_by]        NVARCHAR (20)  NOT NULL,
    [last_update_date]      DATETIME       NOT NULL,
    [customer_id]           INT            NULL,
    [addr_type]             CHAR (1)       NULL,
    [str_address_format_id] INT            NULL,
    [str_block]             NVARCHAR (7)   NULL,
    [str_street]            NVARCHAR (200) NULL,
    [str_storey]            NVARCHAR (4)   NULL,
    [str_unit]              NVARCHAR (7)   NULL,
    [str_building]          NVARCHAR (45)  NULL,
    [str_state]             NVARCHAR (32)  NULL,
    [str_zip_code]          NVARCHAR (10)  NULL,
    [str_country]           CHAR (2)       NULL,
    [str_box]               NVARCHAR (6)   NULL,
    [unstr_addr_1]          NVARCHAR (200) NULL,
    [unstr_addr_2]          NVARCHAR (85)  NULL,
    [unstr_addr_3]          NVARCHAR (85)  NULL,
    [unstr_addr_4]          NVARCHAR (85)  NULL,
    [res_ownership_id]      INT            NULL,
    [res_type_id]           INT            NULL,
    [res_since_year]        INT            NULL,
    CONSTRAINT [sml_address_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
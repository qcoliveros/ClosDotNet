CREATE SEQUENCE [clos].[code_value_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [code_value] (
    [id]                     INT            DEFAULT (NEXT VALUE FOR [clos].[code_value_seq]) NOT NULL,
    [version_time]           INT            DEFAULT ((0)) NOT NULL,
    [create_by]              NVARCHAR (20)  NOT NULL,
    [creation_date]          DATETIME       NOT NULL,
    [last_update_by]         NVARCHAR (20)  NOT NULL,
    [last_update_date]       DATETIME       NOT NULL,
    [master_id]              INT            NULL,
    [deprecated]             CHAR (1)       NOT NULL,
    [status]                 NVARCHAR (60)  NOT NULL,
    [code]                   NVARCHAR (150) NULL,
    [code_value]             NVARCHAR (255) NULL,
    [code_set_value_id]      INT            NULL,
    [display_order]          INT            NULL,
    [ext_value1]             NVARCHAR (255) NULL,
    [ext_value2]             NVARCHAR (255) NULL,
    [ext_value3]             NVARCHAR (255) NULL,
    [ext_value4]             NVARCHAR (255) NULL,
    [cache_ind]              CHAR (1)       NULL,
    [code_value_parent_id]   INT            NULL,
    [code_value_parent_code] NVARCHAR (255) NULL,
    CONSTRAINT [code_value_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);
CREATE SEQUENCE [clos].[code_set_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[code_set] (
    [id]                   INT            DEFAULT (NEXT VALUE FOR [clos].[code_set_seq]) NOT NULL,
    [version_time]         INT            NOT NULL,
    [create_by]            NVARCHAR (20)  NOT NULL,
    [creation_date]        DATETIME       NOT NULL,
    [last_update_by]       NVARCHAR (20)  NOT NULL,
    [last_update_date]     DATETIME       NOT NULL,
    [master_id]            INT            NULL,
    [deprecated]           CHAR (1)       NOT NULL,
    [status]               NVARCHAR (60)  NOT NULL,
    [code]                 NVARCHAR (150) NOT NULL,
    [name]                 NVARCHAR (765) NULL,
    [maintenance_ind]      CHAR (1)       NOT NULL,
    [code_set_parent_id]   INT            NULL,
    [code_set_parent_code] NVARCHAR (255) NULL,
    CONSTRAINT [code_set_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);



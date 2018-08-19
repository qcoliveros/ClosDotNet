CREATE SEQUENCE [clos].[sml_user_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_user] (
    [id]                   INT            DEFAULT (NEXT VALUE FOR [clos].[sml_user_seq]) NOT NULL,
    [version_time]         INT            NOT NULL,
    [create_by]            NVARCHAR (20)  NOT NULL,
    [creation_date]        DATETIME       NOT NULL,
    [last_update_by]       NVARCHAR (20)  NOT NULL,
    [last_update_date]     DATETIME       NOT NULL,
    [master_id]            INT            NULL,
    [deprecated]           CHAR (1)       NOT NULL,
    [status]               NVARCHAR (60)  NOT NULL,
    [login_id]             NVARCHAR(50)   NOT NULL,
    [full_name]            NVARCHAR(150)  NOT NULL,
    [designation_id]       INT            NOT NULL,    
    CONSTRAINT [sml_user_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE INDEX [idx_user_id] ON [clos].[sml_user] (id);
CREATE INDEX [idx_user_login_id] ON [clos].[sml_user] (login_id);
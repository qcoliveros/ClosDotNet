CREATE SEQUENCE [clos].[sml_task_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_task] (
    [id]                  INT              DEFAULT (NEXT VALUE FOR [clos].[sml_task_seq]) NOT NULL,
    [version_time]        INT              NOT NULL,
    [create_by]           NVARCHAR (20)    NOT NULL,
    [creation_date]       DATETIME         NOT NULL,
    [last_update_by]      NVARCHAR (20)    NOT NULL,
    [last_update_date]    DATETIME         NOT NULL,
    [initiator_id]        INT              NULL,
    [previous_owner_id]   INT              NULL,
    [current_owner_id]    INT              NULL,
    [reference_id]        INT              NULL,
    [status]              NVARCHAR (100)   NULL,
    [workflow_process_id] UNIQUEIDENTIFIER NULL,
    [workflow_definition_id]    UNIQUEIDENTIFIER              NULL,
    [workflow_type]       NVARCHAR (200)   NULL,
    [task_action]         NVARCHAR (100)   NULL,
    [task_status]         NVARCHAR (100)   NULL,
    CONSTRAINT [sml_task_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);

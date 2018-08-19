CREATE SEQUENCE [clos].[sml_cust_call_report_seq] START WITH 1 INCREMENT BY 1;

CREATE TABLE [clos].[sml_cust_call_report] (
    [id]                 INT             DEFAULT (NEXT VALUE FOR [clos].[sml_cust_call_report_seq]) NOT NULL,
    [version_time]       INT             NOT NULL,
    [create_by]          NVARCHAR (20)   NOT NULL,
    [creation_date]      DATETIME        NOT NULL,
    [last_update_by]     NVARCHAR (20)   NOT NULL,
    [last_update_date]   DATETIME        NOT NULL,
    [master_id]          INT             NULL,
    [customer_id]        INT             NULL,
    [ref_no]             NVARCHAR (50)   NULL,
    [previous_call_date] DATETIME        NULL,
    [date_of_call]       DATETIME        NULL,
    [site_visit_date]    DATETIME        NULL,
    [mode_of_contact]    CHAR (1)        NULL,
    [purpose_of_call_id] INT             NULL,
    [purpose]            NVARCHAR (1000) NULL,
    [attendees]          NVARCHAR (1000) NULL,
    [follow_up]          NVARCHAR (1000) NULL,
    [owner_id]           INT             NULL,
    [reviewer_id]        INT             NULL,
    [remarks]            NVARCHAR (1000) NULL,
    CONSTRAINT [sml_cust_call_report_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);


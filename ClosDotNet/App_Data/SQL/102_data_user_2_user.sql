-- rmqco01
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             NULL,
             'N',
             'ACTIVE',
             'rmqco01',
             'RM QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'RM'));


               
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             (SELECT su.id 
                FROM sml_user su
               WHERE su.login_id = 'rmqco01'
               AND su.master_id IS NULL),
             'N',
             'ACTIVE',
             'rmqco01',
             'RM QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'RM'));
               
-- rmtlqco01
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             NULL,
             'N',
             'ACTIVE',
             'rmtlqco01',
             'RMTL QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'RMTL'));
               
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             (SELECT su.id 
                FROM sml_user su
               WHERE su.login_id = 'rmtlqco01'
               AND su.master_id IS NULL),
             'N',
             'ACTIVE',
             'rmtlqco01',
             'RMTL QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'RMTL'));
               
-- caqco01
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             NULL,
             'N',
             'ACTIVE',
             'caqco01',
             'CA QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'CA'));
               
INSERT INTO sml_user (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      master_id,
                      deprecated,
                      status,
                      login_id,
                      full_name,
                      designation_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             (SELECT su.id 
                FROM sml_user su
               WHERE su.login_id = 'caqco01'
               AND su.master_id IS NULL),
             'N',
             'ACTIVE',
             'caqco01',
             'CA QCO',
             (SELECT cv.id
                FROM    code_value cv
                     INNER JOIN
                        code_set cs
                     ON     cv.code_set_value_id = cs.id
                        AND cs.status = 'ACTIVE'
                        AND cs.deprecated = 'N'
                        AND cs.master_id IS NULL
                        AND cv.status = 'ACTIVE'
                        AND cv.deprecated = 'N'
                        AND cv.master_id IS NULL
               WHERE cs.code = 'USER_DESIGNATION' AND cv.code = 'CA'));
-- ID_TYPE
INSERT INTO code_set (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      deprecated,
                      status,
                      code,
                      name,
                      maintenance_ind,
                      master_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             'N',
             'ACTIVE',
             'ID_TYPE',
             'ID Type',
             'N',
             NULL);

INSERT INTO code_set (version_time,
                      create_by,
                      creation_date,
                      last_update_by,
                      last_update_date,
                      deprecated,
                      status,
                      code,
                      name,
                      maintenance_ind,
                      master_id)
     VALUES (0,
             'SYSTEM',
             getdate (),
             'SYSTEM',
             getdate (),
             'N',
             'ACTIVE',
             'ID_TYPE',
             'ID Type',
             'N',
             (SELECT id FROM code_set WHERE code = 'ID_TYPE' AND master_id IS NULL));
             
-- Passport
INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'PP',
               'Passport',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL,
               'I');


INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'PP',
               'Passport',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'PP' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'ID_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)),
               'I');
               
-- IC
INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'IC',
               'Identity Card',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL,
               'I');
               
INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'IC',
               'Identity Card',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'IC' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'ID_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)),
               'I');

-- FIN
INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'FIN',
               'Foreign Identity Number',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL,
               'I');

INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'FIN',
               'Foreign Identity Number',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'FIN' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'ID_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)),
               'I');
               
-- Driver's License
INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'DL',
               'Driver''s License',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL,
               'I');

INSERT INTO code_value (version_time,
                        create_by,
                        creation_date,
                        last_update_by,
                        last_update_date,
                        deprecated,
                        status,
                        code,
                        code_value,
                        code_set_value_id,
                        display_order,
                        master_id,
                        code_value_parent_code)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'DL',
               'Driver''s License',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'ID_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'DL' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'ID_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)),
               'I');
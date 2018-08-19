-- GENDER
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
             'GENDER',
             'Gender',
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
             'GENDER',
             'Gender',
             'N',
             (SELECT id FROM code_set WHERE code = 'GENDER' AND master_id IS NULL));
             
-- Male
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
                        master_id)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'M',
               'Male',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'GENDER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL);


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
                        master_id)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'M',
               'Male',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'GENDER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'M' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'GENDER'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Female
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
                        master_id)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'F',
               'Female',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'GENDER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               NULL);


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
                        master_id)
     VALUES (
               0,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'SYSTEM',
               CURRENT_TIMESTAMP,
               'N',
               'ACTIVE',
               'F',
               'Female',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'GENDER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'F' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'GENDER'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
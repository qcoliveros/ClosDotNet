-- MARITAL_STATUS
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
             'MARITAL_STATUS',
             'Marital Status',
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
             'MARITAL_STATUS',
             'Marital Status',
             'N',
             (SELECT id FROM code_set WHERE code = 'MARITAL_STATUS' AND master_id IS NULL));
             
-- Divorced
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
               'D',
               'Divorced',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
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
               'D',
               'Divorced',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'D' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'MARITAL_STATUS'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Married
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
               'Married',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
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
               'Married',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'M' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'MARITAL_STATUS'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Single
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
               'S',
               'Single',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
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
               'S',
               'Single',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'S' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'MARITAL_STATUS'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Widowed
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
               'W',
               'Widowed',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
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
               'W',
               'Widowed',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'MARITAL_STATUS'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'W' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'MARITAL_STATUS'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
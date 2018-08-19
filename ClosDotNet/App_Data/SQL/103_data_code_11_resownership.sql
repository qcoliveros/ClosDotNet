-- RES_OWNER
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
             'RES_OWNER',
             'Residential Ownership',
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
             'RES_OWNER',
             'Residential Ownership',
             'N',
             (SELECT id FROM code_set WHERE code = 'RES_OWNER' AND master_id IS NULL));
             
-- Tenant in common
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
               '0',
               'Tenant in common',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'RES_OWNER'
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
               '0',
               'Tenant in common',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'RES_OWNER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = '0' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'RES_OWNER'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Joint tenant
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
               '1',
               'Joint tenant',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'RES_OWNER'
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
               '1',
               'Joint tenant',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'RES_OWNER'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = '1' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'RES_OWNER'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
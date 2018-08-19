-- CLASSIFICATION
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
             'CLASSIFICATION',
             'Classification',
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
             'CLASSIFICATION',
             'Classification',
             'N',
             (SELECT id FROM code_set WHERE code = 'CLASSIFICATION' AND master_id IS NULL));
             
-- Classified
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
               'C',
               'Classified',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
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
               'C',
               'Classified',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'C' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'CLASSIFICATION'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Pass
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
               'P',
               'Pass',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
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
               'P',
               'Pass',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'P' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'CLASSIFICATION'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Special Mention
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
               'Special Mention',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
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
               'Special Mention',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'S' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'CLASSIFICATION'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Watchlist
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
               'Watchlist',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
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
               'Watchlist',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'CLASSIFICATION'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'W' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'CLASSIFICATION'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
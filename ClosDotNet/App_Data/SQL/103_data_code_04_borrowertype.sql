-- BORROWER_TYPE
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
             'BORROWER_TYPE',
             'Borrower Type',
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
             'BORROWER_TYPE',
             'Borrower Type',
             'N',
             (SELECT id FROM code_set WHERE code = 'BORROWER_TYPE' AND master_id IS NULL));
             
-- Soc/Assn/MC/Sch
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
               'A',
               'Soc/Assn/MC/Sch',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'A',
               'Soc/Assn/MC/Sch',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'A' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Sole Proprietor
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
               'B',
               'Sole Proprietor',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'B',
               'Sole Proprietor',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'B' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Partnership
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
               'Partnership',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'Partnership',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'C' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Private Ltd Co
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
               'Private Ltd Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'Private Ltd Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'D' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Unlimited Co
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
               'E',
               'Unlimited Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'E',
               'Unlimited Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'E' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
                                      
-- Public Ltd Co
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
               'Public Ltd Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
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
               'Public Ltd Co',
               (SELECT cs.id
                  FROM code_set cs
                 WHERE     cs.code = 'BORROWER_TYPE'
                       AND cs.deprecated = 'N'
                       AND cs.master_id IS NULL),
               99,
               (SELECT id
                  FROM code_value
                 WHERE code = 'F' AND master_id IS NULL
                       AND code_set_value_id =
                              (SELECT cs.id
                                 FROM code_set cs
                                WHERE     cs.code = 'BORROWER_TYPE'
                                      AND cs.deprecated = 'N'
                                      AND cs.master_id IS NULL)));
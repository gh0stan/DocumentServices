BEGIN TRANSACTION;
BEGIN TRY
  
  IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE [Key] = 'InformalDocumentService')
  BEGIN
    INSERT INTO [dbo].[AppSettings] ([Key], [Value], [Description])
    VALUES ('InformalDocumentService', 'http://localhost:5000/api/u/Abonents', 'InformalDocumentService service location.');
  END

  IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE [Key] = 'RabbitMQHost')
  BEGIN
    INSERT INTO [dbo].[AppSettings] ([Key], [Value], [Description])
    VALUES ('RabbitMQHost', 'rabbitmq-clusterip-srv', 'Host address for RabbitMQ.');
  END

  IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE [Key] = 'RabbitMQPort')
  BEGIN
    INSERT INTO [dbo].[AppSettings] ([Key], [Value], [Description])
    VALUES ('RabbitMQPort', '5672', 'Port for RabbitMQ.');
  END

  IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE [Key] = 'RabbitMQAbonentExchange')
  BEGIN
    INSERT INTO [dbo].[AppSettings] ([Key], [Value], [Description])
    VALUES ('RabbitMQAbonentExchange', 'abonent_service', 'Exchange for AbonentService events.');
  END

  COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE();
    ROLLBACK TRANSACTION;
END CATCH;
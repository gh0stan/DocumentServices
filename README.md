# Document exchange on microservices architecture  
Some services and features are in development  
Deploy: Docker with K8S  


AbonentService - service for creating organizations, managing their delivery and legal addresses, etc. (currently only 1 user per org, user service later).  
InformalDocumentService - service for working with informal documents, basically any kind of file upload (for ex. pdf), that can be signed and sent to other companies.  
InvoiceXmlDocumentService - DDD/SQRS service for working with structured xml invoices (storing in db), signing and sending NotificationService - processing events from other services.  
Site - web entry point.  
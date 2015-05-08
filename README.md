# SOA-Architecture
An example of SOA architecture with a little bit of CQRS WCF MSMQ/TCP Autofac TopShelf DataAnnotation

WCF TCP layer with IServiceBehaviour for custom exception and for validation with Data Annotation Command always under transaction (NO EVENT SOURCING PATTERN)

Command Query handler... 
Bus for redirect to Command Handler and Query Handler.
QueryHandler could implement ICachedQueryHandler... a decorator class QueryCacheDecorator intercepted by Autofac can cache the Query result.

Data Access Layer with Dapper

MSMQ WCF Self host reader and writer





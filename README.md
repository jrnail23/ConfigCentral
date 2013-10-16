ConfigCentral
=============
ConfigCentral is an experiment in building a centralized configuration service, based on recommendations provided in Jez Humble & Dave Farley's book, Continuous Delivery (pp 44-46).

## The Technologies ##
So far I'm thinking I'll start out with a RESTful API, and some kind of NoSQL database.  I'm not sure whether a document-centric DB or a key-value store will fit this best, so that's an area of exploration.

## The Model ##
Humble & Farley recommend a model consisting of a set of tuples for each configuration property, depending on three things:

- The application
- The version of the application
- The environment it runs in (i.e., dev, staging, uat, production, etc)

## Use Cases ##
They also provide the following use cases to consider when modeling configuration info:

### Add a new environment ###
- You'd need to be able to specify a new set of values for applications deployed into this new environment.

### Create a new version of the application ###
- This might introduce new configuration settings and get rid of some old ones.
- Ensure that when deploying this new version to production, it can get its new settings, but if you have to roll back to an older version it will use the old ones.

### Promote a new version of your application to another environment ###
- Ensure that any new settings are available in the new environment, and that the appropriate values are set for this new environment.

### Relocate a database server ###
- You should be able to update, very simply, every configuration setting that references this database to make it point to the new one. *This seems to suggest a resource model for some config values, rather than all being dumb strings (although that sounds like a good starting point)*

### Manage environments using virtualization ###
- You should be able to use your virtualization management tool to create a new instance of a particular environment that has all the VMs configured correctly.
- You may want to include this info as part of the configuration settings for the particular version of the application deployed into that environment.

## Resources ##
- [Introducing ESCAPE, by Chris Read](http://blog.chris-read.net/2009/02/13/introducing-escape/)
- [Have we ESCaped Continuous Delivery, by Chris Read](http://blog.chris-read.net/2010/10/07/have-we-escaped-continuous-delivery/)
- [ESCape Google Code site](https://code.google.com/p/escservesconfig/)
- [5 Configuration Management Best Practices, by Chris Read](http://www.infoq.com/articles/5-config-mgmt-best-practices)
- [Environment-aware Configuration with DNS-based Environment Determination, by Andrei Volkov](http://zvolkov.com/clog/2010/06/18?s=DNS+based+Environment+Determination)




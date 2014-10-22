## Use Cases to Consider when Modeling Configuration Info ##
* **Create a new organization**
* **Create a new application**
* **Adding a new environment**: Need to be able to specify a new set of values for apps deployed into the new environment. 
* **Creating a new version of an application**: will often introduce new settings and get rid of old ones.  Ensure that when you deploy the new version to production, it can get its new settings, but if you have to roll back to an older version, it will use the old ones. 
* **Promoting a new version of your app from one environment to another**: ensure that any newly created settings are available in the new environment, and that they are set to the appropriate values. 
* **Relocating a database server**: Should be able to update, very simply, every config setting that references this database, to point to the new one. 
* **Managing environments using virtualization**: You should be able to use your virtualization management tool to create a new instance of a particular environment that has all the VMs configured correctly. You may want to include this information as part of the configuration settings for the particular version of the application deployed into that environment.
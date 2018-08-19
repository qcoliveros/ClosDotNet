# Setup
1. Install VS 2013.

## Oracle
2. Install ODT with [ODAC 12c](http://docs.oracle.com/cd/B28359_01/appdev.111/b28844/installation.htm#TDPNG20000).

2.1. Grant role as follows.
```
GRANT CHANGE NOTIFICATION,
    CREATE JOB,
    CREATE PROCEDURE,
    CREATE PUBLIC SYNONYM,
    CREATE ROLE,
    CREATE SESSION,
    CREATE TABLE,
    CREATE VIEW,
    DROP PUBLIC SYNONYM,
    CONNECT,
    RESOURCE
  TO ASPNET_DB_USER;     
```

3. Set PATH and TNS_ADMIN.
If actual DB and client are in same machine, values as follows:
> Required by Toad and VS.
```
PATH = C:\App\Oracle\Database\user\product\12.1.0\client_1
```
> Required by listener service.
```
TNS_ADMIN = C:\App\Oracle\Database\user\product\11.2.0\dbhome_1\NETWORK\ADMIN
```

## MSSQL
4. Install MSSQL Express.

5. Create Database=CLOSDB and User=clos.

6. Run the *C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe*. Follow the steps with the following connection settings:
```
Server: QCOLIVEROS\SQLEXPRESS
Username: clos
Password: clos
Database: CLOSDB
```

7. Change the connection string for LocalSqlServer in *C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config*:
```
connectionString="Data Source=.\SQLExpress;Initial Catalog=CLOSDB;Persist Security Info=True;User ID=clos;Password=clos"
```     

8. Setup users and roles using webadmin.

8.1. Firefox settings (1 time):

8.1.1. Open up Firefox and type *about:config* in URL.

8.1.2. Find the Filter Type in *network.automatic-ntlm-auth.trusted-uris*.

8.1.3. Double click *network.automatic-ntlm-auth.trusted-uris* and type in *localhost* then hit enter.

8.2. Open cmd and run the following:
```
"C:\Program Files\IIS Express\iisexpress.exe" /path:"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ASP.NETWebAdminFiles" /vpath:"/ASP.NETWebAdminFiles" /port:8082 /clr:4.0 /ntlm
```

8.3. In Firefox, type the following in URL:
```
http://localhost:8082/asp.netwebadminfiles/default.aspx?applicationPhysicalPath=C:\Workspace\Integro\project\dotNet\ClosDotNet\ClosDotNet&applicationUrl=/
```

8.4. Create users and roles.
    
###### Note: Switch between version:
9. To switch between version, click Tools > Options > Database Tools > Data Connections > SQL Server Instance Name. The default for VS2013 is `(LocalDB)\v11.0`, change it to SQL Server 2014 Express to as `(LocalDB)\MSSQLLocalDB`.
     
# VS
1. Create a Project: MVC + Web Api + Web Form.

2. Install the following packages:
    - Common.Logging.Log4Net
      ```
      Install-Package Common.Logging.Log4Net1211 -Source nuget.org
      ```
    - Spring.Data.NHibernate4
      ```
      Install-Package Spring.Data.NHibernate4 -Source nuget.org
      ```
    - Spring.Web.Mvc4 
      ```
      Install-Package Spring.Web.Mvc4 -Source nuget.org
      ```
    - AutoMapper
      ```
      Install-Package AutoMapper -Version 3.3.1 -Source nuget.org
      ```
    - Grid.MVC
      ```
      Install-Package Grid.Mvc -Version 3.0.0 -Source nuget.org
      ```
    - Bootstrap.Datepicker 
      ```
      Install-Package Bootstrap.Datepicker -Source nuget.org
      ```
    - NHibernate.AspNet.Identity
      ```
      Uninstall-Package Microsoft.AspNet.Identity.EntityFramework
      Uninstall-Package EntityFramework
      Install-Package NHibernate.AspNet.Identity -Source nuget.org
      ```
    - foolproof
      ```
      Install-Package foolproof -Source nuget.org
      ```
    - [WorkflowEngine.NET-ProviderForMSSQL](http://workflowenginenet.com/Articles/Item/wfe-alternative-wf)
      ```
      Install-Package WorkflowEngine.NET-ProviderForMSSQL -Source nuget.org
      Install-Package WorkflowEngine.NET-Designer -Source nuget.org  
      ```

3. Add reference to the following:
    - Framework > System.Transactions
    #### For Oracle
    - Extensions > Oracle.DataAccess
        > In VS, select the DLL > Right click it > In the Properties grid, select Copy Local = True

1. Install VS 2013.

(For Oracle)
2. Install ODT with ODAC 12c. 
    http://docs.oracle.com/cd/B28359_01/appdev.111/b28844/installation.htm#TDPNG20000
   Grant Role: 
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
   
3. Set PATH and TNS_ADMIN
    If actual DB and client are in same machine, values as follows:
        PATH = C:\App\Oracle\Database\user\product\12.1.0\client_1
            Required by Toad and VS.
        TNS_ADMIN = C:\App\Oracle\Database\user\product\11.2.0\dbhome_1\NETWORK\ADMIN
            Required by listener service.

(For MSSQL)
4. Install MSSQL Express
5. Create Database=CLOSDB and User=clos
6. Run the C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe. Follow the steps with the following connection settings:
    Server: QCOLIVEROS\SQLEXPRESS
    Username: clos
    Password: clos
    Database: CLOSDB
7. Change the connection string for LocalSqlServer in 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config':
    connectionString="Data Source=.\SQLExpress;Initial Catalog=CLOSDB;Persist Security Info=True;User ID=clos;Password=clos"
     
8. Setup users and roles using webadmin.
    8.1. Firefox settings (1 time):
            - Open up Firefox and type in about:config as the url 
            - In the Filter Type in "network.automatic-ntlm-auth.trusted-uris"
            - Double click "network.automatic-ntlm-auth.trusted-uris" and type in "localhost" and hit enter
    8.2. Open cmd and run the following:
         "C:\Program Files\IIS Express\iisexpress.exe" /path:"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ASP.NETWebAdminFiles" /vpath:"/ASP.NETWebAdminFiles" /port:8082 /clr:4.0 /ntlm
    8.3. In firefox, type the following in URL:
         http://localhost:8082/asp.netwebadminfiles/default.aspx?applicationPhysicalPath=C:\Workspace\Integro\project\dotNet\ClosDotNet\ClosDotNet&applicationUrl=/
    8.4. Create users and roles.
    
Note: Switch between version:
1. Tools > Options > Database Tools > Data Connections > SQL Server Instance Name
    The default for VS2013: (LocalDB)\v11.0
    Change SQL Server 2014 Express to: (LocalDB)\MSSQLLocalDB
     
VS
1. Create a Project: MVC + Web Api + Web Form.
2. Install the following packages:
    - Common.Logging.Log4Net
        Install-Package Common.Logging.Log4Net1211 -Source nuget.org
    - Spring.Data.NHibernate4
        Install-Package Spring.Data.NHibernate4 -Source nuget.org
    - Spring.Web.Mvc4 
        Install-Package Spring.Web.Mvc4 -Source nuget.org
    - AutoMapper
        Install-Package AutoMapper -Version 3.3.1 -Source nuget.org
    - Grid.MVC
        Install-Package Grid.Mvc -Version 3.0.0 -Source nuget.org
    - Bootstrap.Datepicker 
        Install-Package Bootstrap.Datepicker -Source nuget.org
    - NHibernate.AspNet.Identity
        Uninstall-Package Microsoft.AspNet.Identity.EntityFramework
        Uninstall-Package EntityFramework
        Install-Package NHibernate.AspNet.Identity -Source nuget.org
    - foolproof
        Install-Package foolproof -Source nuget.org
    - WorkflowEngine.NET-ProviderForMSSQL
        Install-Package WorkflowEngine.NET-ProviderForMSSQL -Source nuget.org
        Install-Package WorkflowEngine.NET-Designer -Source nuget.org
            See http://workflowenginenet.com/Articles/Item/wfe-alternative-wf  
3. Add reference to the following:
    - Framework > System.Transactions
    (For Oracle)
    - Extensions > Oracle.DataAccess
        In VS, select the DLL > Right click it > In the Properties grid, select Copy Local = True

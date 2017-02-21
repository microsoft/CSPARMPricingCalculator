#App Setup Steps


##1. Introduction
This document explains the steps involved in setting up of an Azure AD Application by a CSP Partner for accessing the ARM APIs to integrate with Azure CSP subscriptions of CSP customers. 
An Azure AD Application authentication can be App only or App + User. This document shows the App + User based authentication option.

##2.	Prerequisites

*	The CSP Partner has already created the Customer and one or more Azure subscriptions for the customer.
*	This steps in this document requires a user in CSP Partner’s directory who has access to create application in the partner’s directory. Also, a user who is an Admin Agent is also required.
*	A computer with Azure Active Directory PowerShell v2 module installed.

##3.	Add an Azure AD Native Application
  1.	Navigate to the Azure Management Portal: https://manage.windowsazure.com Login with user having permissions to create an Azure AD Application.

  2.  Click on ACTIVE DIRECTORY from the menu. Click on the directory from the displayed list.
  ![ad_portal](/Documentation/images/app_setup/image1.png)

  3.	Click on APPLICATIONS. Click on ADD icon to create an Azure AD application.
  ![apps](/Documentation/images/app_setup/image2.png)
  
  4.	Click on the link: Add an application my organization is developing.
  ![whatToDo](/Documentation/images/app_setup/image3.png)
  
  5.	Provide a name for the application. Select the NATIVE CLIENT APPLICATION option
  ![aboutApp](/Documentation/images/app_setup/image4.png)
  
  6.	Provide a name for the application. Select the NATIVE CLIENT APPLICATION option
  ![appInfo](/Documentation/images/app_setup/image5.png)
  
  7.	Click on CONFIGURE option
  ![configure](/Documentation/images/app_setup/image6.png)
  
  8.	Scroll down to the section: permissions to other applications. Click on Add application button.
  ![permissions](/Documentation/images/app_setup/image7.png)
  
  9.  Add the Windows Azure Service Management API app.
  ![permissions_select](/Documentation/images/app_setup/image8.png)
  
  10.	Expand the dropdown for the Windows Azure Service Management API application. Tick the “Access Azure Service Management …” option. Click on SAVE icon to save the changes to the application configuration.
  ![permissions_select_others](/Documentation/images/app_setup/image9.png)
  
  11.	Copy the Client ID for the application. This is the App Id of the Azure AD Application which can be used to fetch the Azure AD Token and used to perform the ARM API operations after the completing the remaining steps in this document.
  ![configure](/Documentation/images/app_setup/image10.png)
  
##4.  Configure Pre-consent for the application
  1.  On a computer having Azure Active Directory PowerShell v2 module installed, Open a new PowerShell session.
  
  2.  Type the following cmdlet to initiate login 
  ```powershell
  Connect-AzureAD
  ```
  ![connect-ad](/Documentation/images/app_setup/image11.jpg)
  
  3.  Login with the user credentials having permissions to obtain information of the created application and also having permissions to add members to Admin Agent groups.
  ![connect-ad](/Documentation/images/app_setup/image12.png)
  
  4.  Type the following cmdlet to view the list of Azure AD groups in the directory
  ```powershell
  Get-AzureADGroup
  ```
  ![connect-adgroup](/Documentation/images/app_setup/image13.png)
  *Copy the ObjectId for the AdminAgents group. This will be used in the subsequent steps.*
    
  5.  Type the following cmdlet to view the ServicePrincipal for the application created earlier.
  ```powershell
  Get-AzureADServicePrincipal -SearchString "Name of the Azure AD Application here"
  ```
  ![connect-adprincipal](/Documentation/images/app_setup/image14.png)
  
  *Copy the ObjectId for the Serviceprincipal. This will be used in the subsequent step.*
  
  6. Run the following cmdlet to configure the pre-consent.
  ```powershell
  Add-AzureADGroupMember -ObjectId “ObjectId of the AdminAgents Group here” -RefObjectId “ObjectId of the ServicePrincipal”
  ```
  
  ![connect-adGroupMember](/Documentation/images/app_setup/image15.png)
  
##5.  Conclusion
   The application created using the previous steps has been configured for pre-consent and can be used for accessing ARM APIs of the CSP Azure subscriptions of the CSP Partner.
  

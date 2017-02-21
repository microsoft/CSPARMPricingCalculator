#Deployment Steps

1. Install Visual Studio 2015

2. Open the CSPARMPricingCalculator.sln file from the code folder

3. From the Solution Explorer pane, double click on "App.config" file in the CSPARMPricingCalculatorUI project in the solution.
![vs](/Documentation/images/deployment/image1.png)

4. Observe the appSettings section. The "value" field for the appSettings needs to be provided and will be configured as per the subsequent steps mentioned below.
![vs](/Documentation/images/deployment/image2.png)

5. Login to Partner Center portal

6. On the dashboard, select "Account Settings" -> Click on "App Management"

7. Under "Native App" section, check if App already created. If not click on "Add new native app"
![vs](/Documentation/images/deployment/image3.png)

8. Copy the values from the "Native App" section on the portal and paste in the appSettings "value" field as per below mapping:
  * "App ID" -> "CSP:PCNativeAppClientId" *
  * "Account ID" -> "CSP:PartnerTenantID" *
![vs](/Documentation/images/deployment/image4.png)
![vs](/Documentation/images/deployment/image5.png)

9. Create a Native App in Azure AD for ARM API access with pre-consent setup. To do so follow the step by step guide provided [here](https://github.com/Microsoft/CSPARMPricingCalculator/blob/master/Documentation/app_setup.md)

10. Copy the value for the "Client Id" of the Azure AD Application created in previous step and paste in the appSettings "value" field for "CSP:ARMNativeAppClientId" in the app.config file.
![vs](/Documentation/images/deployment/image6.png)

11. Switch back to the Partner Center Portal window. On dashboard, Click on "View Customers" to view the list of customers.
![vs](/Documentation/images/deployment/image7.png)

12. Click on the dropdown arrow adjacent to any customer who has Azure subscription provisioned.
![vs](/Documentation/images/deployment/image8.png)

13. Copy the value for "Microsoft ID" field and paste in the appSettings "value" field for "CSP:CustomerTenantId" in the app.config file.
![vs](/Documentation/images/deployment/image9.png)

14. Click on "View subscriptions" to view the list of subscriptions provisioned for the customer. Click on the dropdown arrow adjacent to any active Azure subscription for the customer. 
![vs](/Documentation/images/deployment/image10.png)

15. Copy the value for the "Subscription ID" field and paste in the appSettings "value" field for "CSP: AzureSubscriptionId" in the app.config file.
![vs](/Documentation/images/deployment/image11.png)

16. On the Partner Center dashboard, Click on "Add new user" under the "User Accounts" section.
![vs](/Documentation/images/deployment/image12.png)

17. Provide a name and email for the new user. Click on "Admin Agent" radio button to provide the user Admin Agent role.
![vs](/Documentation/images/deployment/image13.png)

18. Click on "Add" button to create the user. Note the username and temporary password of the new user created.

19. Start an "InPrivate browsing" window in Internet Explorer/Edge or "Incognito" window in Chrome. Navigate to https://partnercenter.microsoft.com and login with the user created earlier. Follow the password change page and change the password. 

20. In the app.config file, provide the value for the username in the appSettings "values" field for "CSP:AdminAgentUserName".

21. Provide the value for password of the user created in the previous steps in the appSettings "values" field for "CSP:AdminAgentPassword". Save the changes to the file.
![vs](/Documentation/images/deployment/image14.png)

22. Please remember to remove the Password value after running the sample code, as it is NOT safe to leave password in the config file in unencrypted format.

23. Click on Build -> Build Solution.
![vs](/Documentation/images/deployment/image15.png)




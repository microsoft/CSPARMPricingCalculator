# CSP – ARM Pricing Calculator 
## Introduction

The tool can be used to estimate the cost of hosting a solution in an Azure CSP subscription. It takes the ARM Template as an input and gives the estimated costs as an output. It has been built as a sample and currently supports Virtual machine and its components including Compute hours, Storage, Network, Public IP and Software costs. 

The tool does the following:

* Fetch the Rate card for Azure CSP
* Read the resources defined in the template
* Map the resources to the appropriate meters and provide cost estimates
* The estimates can then be exported into a csv file.

The idea is to help CSP partners who are performing integration by providing the tool and source code to assist them to incorporate this feature at their end.

## Prerequisites 

The following are the prerequisites for using the CSP ARM Pricing Calculator:

*       The CSP Partner has already created the Customer and one or more Azure subscriptions for the customer. 
*       A computer with Visual Studio 2015 installed.
*       An account that has Admin Agent privileges in the CSP account.
*       An account that has Global Admin privileges will be needed during setup and configure of the Azure AD Application

## Limitations
It has been built as a sample and currently supports Virtual machine and its components including compute hours, storage, network, public ip and software costs. The source code is provided to enable the CSP Partners to understand the steps and logic involved and may choose to build their own.
It does not support advanced scenarios of ARM templates like nested templates, copy index, etc.
It shows the estimates based on CSP Partner Pricing and does not include pricing of hosting any 3rd party services or margins of the CSP Partner/reseller. 
The tool and source code is provided as a sample and no support is provided.

## Alternatives
1. [CSP Pricing Calculator](https://azure.microsoft.com/en-us/pricing/calculator/channel/)
  
  The CSP Pricing calculator is available for CSP Partners on the Partner Center Portal. This can be used for estimating the cost of hosting Azure services in CSP subscriptions.
  However, currently, this cannot be customized and currently only available for CSP Partner users only.  

2. [Public Azure Pricing Calculator](https://azure.microsoft.com/en-in/pricing/calculator/)
  
  The azure pricing calculator is available publicly. However, this shows the prices as per Pay-As-You-Go Subscriptions purchased from Microsoft direct and does not show CSP pricing. Currently, this cannot be customized.  

## Contributions

Please refer to [contributing.md](Documentation/contributing.md)


## License

This project has adopted the [Microsoft Open Source Code of Conduct] (https://opensource.microsoft.com/codeofconduct/). For more information, see the [Code of Conduct FAQ] (https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com] (mailto:opencode@microsoft.com) with any additional questions or comments.

The MIT License (MIT)

Copyright (c) 2016 Microsoft

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


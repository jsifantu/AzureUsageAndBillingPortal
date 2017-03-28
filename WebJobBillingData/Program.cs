﻿//------------------------------------------ START OF LICENSE -----------------------------------------
//Azure Usage Insights Portal
//
//Copyright(c) Microsoft Corporation
//
//All rights reserved.
//
//MIT License
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
//associated documentation files (the ""Software""), to deal in the Software without restriction, 
//including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
//and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
//subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial 
//portions of the Software.
//
//THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
//BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
//NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR 
//OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
//CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------- END OF LICENSE ------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

using Commons;

namespace WebJobBillingData
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("WebJobUsageHistory:Main starting. DateTimeUTC: {0}", DateTime.UtcNow);
            // Get organization ID, which I think corresponds to the tenant domain
            var organizations = AzureResourceManagerUtil.GetUserOrganizations();

            foreach (var organization in organizations) {
                // Get all subscriptions for the specified user
                var subscriptions = AzureResourceManagerUtil.GetUserSubscriptions(organization.Id);
                foreach (var subscription in subscriptions) {
                    DateTime sdt = DateTime.Now.AddYears(-3);
                    DateTime edt = DateTime.Now.AddDays(-1);
                    BillingRequest br = new BillingRequest(subscription.Id, organization.Id,
                                                            sdt, edt);
                    Functions.ProcessQueueMessage(br);
                }
            }
            return;
/**

            JobHostConfiguration config = new JobHostConfiguration();
            config.Queues.BatchSize = 3;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(15);

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
**/
        }
    }
}

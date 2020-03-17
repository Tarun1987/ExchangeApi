using System;
using System.Collections.Generic;
using System.Web.Http;
using WebExchangeApi.Helpers;
using WebExchangeApi.Models;

namespace WebExchangeApi.Controllers
{
    public class HomeController : ApiController
    {
        // GET: /
        public string Get()
        {
            return "Please append email in url";
        }

        // GET: /email@gmail.com
        public IList<DistributionListModel> Get(string email)
        {

            var maxRetryAttempts = 8;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

            Console.WriteLine("Starting application.. ");
            IList<DistributionListModel> distributionList = null;

            RetryHelper.RetryOnException(maxRetryAttempts, pauseBetweenFailures, () =>
            {
                distributionList = new List<DistributionListModel>();
                ExpandDistributionLists(10, email, distributionList);
            });

            Console.WriteLine("Ending application.. ");
            return distributionList;

        }

        private IList<DistributionListModel> ExpandDistributionLists(int loopCount, string emailOf, IList<DistributionListModel> distributionList)
        {
            for (int i = 1; i < loopCount; i++)
            {
                if (i % 3 == 0)
                {
                    ExpandDistributionLists(3, string.Format($"Email - {i}"), distributionList);
                }
                else
                {
                    var newDistribution = new DistributionListModel
                    {
                        Email = string.Format($"Email - {i}"),
                        ChildrenOf = emailOf
                    };

                    distributionList.Add(newDistribution);
                }

            }

            return distributionList;
        }
    }




    //private static void ExpandDistributionLists(ExchangeService service, string Mailbox)
    //{
    //    // Return the expanded group.
    //    ExpandGroupResults myGroupMembers = service.ExpandGroup(Mailbox);
    //    // Display the group members.
    //    foreach (EmailAddress address in myGroupMembers.Members)
    //    {
    //        // Check to see if the mailbox is a public group
    //        if (address.MailboxType == MailboxType.PublicGroup)
    //        {
    //            // Call the function again to expand the contained
    //            // distribution group.
    //            ExpandDistributionLists(service, address.Address);
    //        }
    //        else
    //        {
    //            // Output the address of the mailbox.
    //            Console.WriteLine("Email Address: {0}", address);
    //        }
    //    }
    //}
}

using System.Collections.Generic;
using System.Web.Http;
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
        public DistributionListModel Get(string email)
        {
            var response = new DistributionListModel
            {
                Email = email,
                ChildEmailList = ExpandDistributionLists(11)
            };

            return response;
        }

        private IList<DistributionListModel> ExpandDistributionLists(int loopCount)
        {
            IList<DistributionListModel> distributionList = new List<DistributionListModel>();

            for (int i = 1; i < loopCount; i++)
            {
                var newDistribution = new DistributionListModel { Email = string.Format($"Email - {i}") };
                if (i % 3 == 0)
                {
                    newDistribution.ChildEmailList = ExpandDistributionLists(3);
                }

                distributionList.Add(newDistribution);
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

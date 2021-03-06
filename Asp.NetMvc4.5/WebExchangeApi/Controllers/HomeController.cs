﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using WebExchangeApi.Helpers;
using WebExchangeApi.Models;
using System.Linq;
using System.Configuration;

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
        public IHttpActionResult Get(string email)
        {
            IEnumerable<string> list;
            if (!Request.Headers.TryGetValues("x-token", out list))
                return BadRequest("Un-authorized.");

            var token = list.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(token) || EncryptionDecryptionHelper.Decrypt(token) != ConfigurationManager.AppSettings["matchingKey"])
                return BadRequest("Un-authorized.");

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
            return Ok(distributionList);

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


        // GET: /string
        public string GetToken(string inputStr)
        {
            return EncryptionDecryptionHelper.Encrypt(inputStr);
        }
    }

}

using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace ConsmosDbCollection
{
    class Program
    {
        static string DatabaseName = "maindb";
        static string CollectionName = "employee";
        static DocumentClient dc;

        static string endpoint = "https://employeecosmosdbtest.documents.azure.com:443/";
        static string key = "TBvoCKM8icUTyjtD3XWlJE1hQGhNEUxYP6OVmHb7a3FjpgJ8NCw5EGP2e7BRz0IpgzGzPxsJmEfRJa3KQ2Cvuw==";
        static void Main(string[] args)
        {
            dc = new DocumentClient(new Uri(endpoint), key);

           // add("85055", "maink", "PAT");
         //  add("95065", "rohit", "PAT");
              get();

            Console.ReadLine();

            static void add(string id, string employeename, string jobtitile)
            {

                EmployeeEntity et = new EmployeeEntity(id, employeename, jobtitile);

                var result = dc.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), et).GetAwaiter().GetResult();
            }

            static void get()
            {

                FeedOptions queryOption = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

                IQueryable<EmployeeEntity> query = dc.CreateDocumentQuery<EmployeeEntity>(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOption);

              foreach(var e in query)
                {
                    Console.WriteLine (e);
                }

            }


        }

        public class EmployeeEntity
        {
            public string ID { get; set; }
            public string EmployeeName { get; set; }
            public string JobTitle { get; set; }


            public EmployeeEntity()
            {

            }
            public EmployeeEntity(string id, string employeename, string jobtitile)
            {
                this.ID = id;
                this.EmployeeName = employeename;
                this.JobTitle = jobtitile;
            }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
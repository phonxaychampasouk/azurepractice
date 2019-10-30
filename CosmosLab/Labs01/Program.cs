using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Labs01
{
    public class Program
{        
    private static readonly string _endpointUri = "https://cosmosdblabs.documents.azure.com:443/";
    private static readonly string _primaryKey = "BsWLyXnSLrqwhtU7ICeusDFxreC8RsKAFn0pHAFeHpi1rPLIQ4RWSTEkrbgcXcleJo3sNJJmrd4he7VWyVIbfg==";         

    public static async Task Main(string[] args)
    {
        using (CosmosClient client = new CosmosClient(_endpointUri, _primaryKey))
        {
              Database targetDatabase = client.GetDatabase("EntertainmentDatabase");
                IndexingPolicy indexingPolicy = new IndexingPolicy
 {
     IndexingMode = IndexingMode.Consistent,
     Automatic = true,
     IncludedPaths =
     {
         new IncludedPath
         {
             Path = "/*"
         }
     }
 };
  var containerProperties = new ContainerProperties("CustomCollection", "/type")
 {
     IndexingPolicy = indexingPolicy
 };
  var containerResponse = await targetDatabase.CreateContainerIfNotExistsAsync(containerProperties, 10000);
 var customContainer = containerResponse.Container;
  await Console.Out.WriteLineAsync($"Custom Container Id:\t{customContainer.Id}");
        }    
} 
}
}

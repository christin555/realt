using Dadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtOn
{
   public class SuggestClientDadata
    {
        public SuggestClient api
        {
            get; set;
        }
        public void SetUp()
        {
           
        }

        public IEnumerable<Dadata.Model.Suggestion<Dadata.Model.Address>> SuggestAddress(string query)
        {
            
                var token = "518f96d435704b748e40b3f6a0aa18efea327334";
                this.api = new SuggestClient(token);
                var response = api.SuggestAddress(query);
                return response.suggestions.Take(5);
          
        }
    }
}

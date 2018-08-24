using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace google
{
   public static class Extensions
    {
       public static ErrorResponse getAPiError(this System.Net.WebResponse res)
       {
           return res.readObject<ErrorResponse>();
       }
       public static T readObject<T>(this System.Net.WebResponse res)
       {
          
           return JsonConvert.DeserializeObject<T>(
               new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd());
       }
    }
}

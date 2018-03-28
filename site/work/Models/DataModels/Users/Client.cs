using System.Collections.Generic;
using QFlow.Models.DataModels.Requests;

namespace QFlow.Models.DataModels.Users
{
    public class Client : User
    {
        public ICollection<Request> Requests { get; set; }
    }
}
 
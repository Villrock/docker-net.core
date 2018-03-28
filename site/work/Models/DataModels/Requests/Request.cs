using System;
using System.Collections.Generic;
using QFlow.Models.DataModels.Alerts;
using QFlow.Models.DataModels.Users;

namespace QFlow.Models.DataModels.Requests
{
    public class Request
    {
        public int Id { get; set; }

        /*Created Request Information*/
        public string Title { get; set; }
        public DateTime? DateOfStudy { get; set; }
        public string DurationOfStudy { get; set; }
        public string FullAddress { get; set; }                     // Full address including country 
        public string SpecialInstructions { get; set; }             // A box where customer can add text based information.
        public string StudyInformation { get; set; }                // A box where customer can add text based information. 
        /*End Created Request Information*/
       
        /*Foreing Keys */
        public int? RequestId { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }

        public int RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public DateTime Created { get; set; }
        public string CreatedAsString { get; set; }

        public string RequestNumber { get; set; }

        //Additional colections
        public ICollection<RequestDetail> Details { get; set; }
        public ICollection<AlertNotification> Alerts { get; set; }

        public Request Clone()
        {
            var request = new Request
            {
                Title = Title,
                RequestStatusId = RequestStatusId,

                DateOfStudy = DateOfStudy,
                DurationOfStudy = DurationOfStudy,
                FullAddress = FullAddress,
                SpecialInstructions = SpecialInstructions,
                StudyInformation = StudyInformation,
                RequestNumber = RequestNumber,

                Created = Created,
                CreatedAsString = CreatedAsString,
                ClientId =  ClientId,
                ManagerId = ManagerId
            };
            return request;
        }
    }
}

using System;

namespace QFlow.Models.DataModels.Requests
{
    public class RequestDetail
    {
        public int Id { get; set; }

        //public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        //public int ManufacturerModelId { get; set; }
        public ManufacturerModel ManufacturerModel { get; set; }

        public string ServiceWarrantyOptions { get; set; }
        public string AncillaryItems { get; set; }

        /*Additional Fields - QUOTED*/
        public int? UnitPrice { get; set; }
        public string Currency { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? TrainingDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? EstimatedLeadTime { get; set; }
        public string CalibrationServiceRequirements { get; set; }
        public string AlternativesRecommendation { get; set; }
        public string LocalTechnicalSupport { get; set; }

        public string ApprovalsCertificationRequired { get; set; } // ?? document upload  / upload document control
        public string LinkToBrochure { get; set; }                 // ?? document upload  / upload document control
        /*End Additional Fields */

        /*Additional Fields - PURCHASE ORDER*/
        public DateTime? SivRequiredDate { get; set; }
        public string ContactPersons { get; set; }
        public bool? SiteActivated { get; set; }
        public DateTime? ServiceDueDate { get; set; }
        public DateTime? DeInstallDueDate { get; set; }

        //Shipping and Delivery Details
        public string TrackingNumberOrCourierDetails { get; set; }
        public string NameOfPerson { get; set; }                    //who installation arranged with
        public DateTime? ConfirmedDeliveryDate { get; set; }
        public DateTime? ActualDespatchDate { get; set; }
        public DateTime? ActualInstallationDate { get; set; }
        public string TrainingDocuments { get; set; }               // ?? document upload  / upload document control
        public string ProofOfDelivery { get; set; }                 // ?? document upload  / upload document control
        public string SerialNumber { get; set; }
        public DateTime? EstimateDeliveryDate { get; set; }
        public string InvoiceFile { get; set; }

        /*End Additional Fields */

        /*Additional Fields - INVOICED*/
        public DateTime? IssuedDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? PaymentRunDate { get; set; }
        /*End Additional Fields */

        /*Additional Fields - Paid */
        public DateTime? PaymentReceivedDate { get; set; }
        /*End Additional Fields */

        public int? RequestId { get; set; }
        public Request Request { get; set; }

        public Guid? FolderId { get; set; }

        public RequestDetail Clone()
        {
            var clone =  ( RequestDetail )MemberwiseClone();
            clone.Id = 0;
            clone.RequestId = default(int?);
            clone.Request = null;
            clone.Manufacturer = Manufacturer?.Clone();
            clone.ManufacturerModel = ManufacturerModel?.Clone();
            return clone;
        }
    }
}

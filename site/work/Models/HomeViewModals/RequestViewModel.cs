using System;
using System.Collections.Generic;
using System.Linq;
using QFlow.Models.HomeViewModals;

namespace QFlow.Models.HomeViewModals
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int Status { get; set; }

        public string Title { get; set; }
        public DateTime? DateStudy { get; set; }
        public string DurationStudy { get; set; }
        public string FullAddress { get; set; }
        public string SpecialInstructions { get; set; }
        public string StudyInformation { get; set; }
        public string RequestNumber { get; set; }

        public IEnumerable<RequestDetailViewModel> Details { get; set; }

        public RequestViewModel()
        {
            Details = new List<RequestDetailViewModel>();
        }
    }

    public class RequestDetailViewModel
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string ServiceWarrantyOptions { get; set; }
        public string AncillaryItems { get; set; }

        public string Currency { get; set; }
        public int? Quantity { get; set; }
        public int? UnitPrice { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? TrainingDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? EstimatedLeadTime { get; set; }
        public string CalibrationServiceRequirements { get; set; }
        public string AlternativesRecommendation { get; set; }
        public string LocalTechnicalSupport { get; set; }

        public string ApprovalsCertificationRequired { get; set; } // ?? document upload  / upload document control
        public bool HasApprovalsCertificationRequired { get; set; }
        public string LinkToBrochure { get; set; }                 // ?? document upload  / upload document control
        public bool HasLinkToBrochure { get; set; }              // ?? document upload  / custome field

        public string RequestNumber { get; set; }
        public DateTime? SivRequiredDate { get; set; }
        public string ContactPersons { get; set; }
        public bool? SiteActivated { get; set; }
        public DateTime? ServiceDueDate { get; set; }
        public DateTime? DeInstallDueDate { get; set; }

        public string TrackingNumberOrCourierDetails { get; set; }
        public string NameOfPerson { get; set; }                    //who installation arranged with
        public DateTime? ConfirmedDeliveryDate { get; set; }
        public DateTime? ActualDespatchDate { get; set; }
        public DateTime? ActualInstallationDate { get; set; }
        public string TrainingDocuments { get; set; }               // ?? document upload  / upload document control
        public string ProofOfDelivery { get; set; }                 // ?? document upload  / upload document control
        public bool HasTrainingDocuments { get; set; }
        public bool HasProofOfDelivery { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? EstimateDeliveryDate { get; set; }
        public string InvoiceFile { get; set; }
        public bool HasInvoiceFile { get; set; }

        public DateTime? IssuedDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? PaymentRunDate { get; set; }

        public DateTime? PaymentReceivedDate { get; set; }
        public Guid? FolderId { get; set; }
    }

    public class ClientRequestDetailViewModel
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string ServiceWarrantyOptions { get; set; }
        public string AncillaryItems { get; set; }

        public string Currency { get; set; }
        public int? Quantity { get; set; }
        public int? UnitPrice { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? TrainingDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? EstimatedLeadTime { get; set; }
        public string CalibrationServiceRequirements { get; set; }
        public string AlternativesRecommendation { get; set; }
        public string LocalTechnicalSupport { get; set; }

        public string ApprovalsCertificationRequired { get; set; } // ?? document upload  / upload document control
        public bool HasApprovalsCertificationRequired { get; set; }
        public string LinkToBrochure { get; set; }                 // ?? document upload  / upload document control
        public bool HasLinkToBrochure { get; set; }              // ?? document upload  / custome field

        public string RequestNumber { get; set; }

        public string TrackingNumberOrCourierDetails { get; set; }
        public string TrainingDocuments { get; set; }               // ?? document upload  / upload document control
        public string ProofOfDelivery { get; set; }                 // ?? document upload  / upload document control
        public bool HasTrainingDocuments { get; set; }
        public bool HasProofOfDelivery { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? EstimateDeliveryDate { get; set; }
        public string InvoiceFile { get; set; }
        public bool HasInvoiceFile { get; set; }

        public DateTime? PaymentDueDate { get; set; }
        public DateTime? PaymentRunDate { get; set; }

        public Guid? FolderId { get; set; }
    }
}

public static class EnumerableExtentions
{
    public static IEnumerable<ClientRequestDetailViewModel> ToClientModelList(
        this IEnumerable<RequestDetailViewModel> modelList)
    {
        return modelList.Select(_ => new ClientRequestDetailViewModel()
        {
            Id = _.Id,
            Manufacturer = _.Manufacturer,
            Model = _.Model,
            ServiceWarrantyOptions = _.ServiceWarrantyOptions,
            AncillaryItems = _.AncillaryItems,
            Currency = _.Currency,
            Quantity = _.Quantity,
            UnitPrice = _.UnitPrice,
            ShippingDate = _.ShippingDate,
            TrainingDate = _.TrainingDate,
            InstallationDate = _.InstallationDate,
            EstimatedLeadTime = _.EstimatedLeadTime,
            CalibrationServiceRequirements = _.CalibrationServiceRequirements,
            AlternativesRecommendation = _.AlternativesRecommendation,
            LocalTechnicalSupport = _.LocalTechnicalSupport,
            HasApprovalsCertificationRequired = _.HasApprovalsCertificationRequired,
            ApprovalsCertificationRequired = _.ApprovalsCertificationRequired,
            HasLinkToBrochure = _.HasLinkToBrochure,
            LinkToBrochure = _.LinkToBrochure,
            TrackingNumberOrCourierDetails = _.TrackingNumberOrCourierDetails,
            HasTrainingDocuments = _.HasTrainingDocuments,
            TrainingDocuments = _.TrainingDocuments,
            HasProofOfDelivery = _.HasProofOfDelivery,
            ProofOfDelivery = _.ProofOfDelivery,
            SerialNumber = _.SerialNumber,
            EstimateDeliveryDate = _.EstimateDeliveryDate,
            HasInvoiceFile = _.HasInvoiceFile,
            InvoiceFile = _.InvoiceFile,
            PaymentDueDate = _.PaymentDueDate,
            PaymentRunDate = _.PaymentRunDate,
            FolderId = _.FolderId ?? Guid.NewGuid()
        });
    }
}

namespace QFlow.Models.DataModels.Alerts
{
    public class AlertType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsSendingEmail { get; set; }

        //Names of AlertTypes
        public const string SupplierPaymentDue = "Supplier payment due";
        public const string ConfirmedDeliveryDate = "Confirmed delivery date";
        public const string ServiceDue = "Service due";
        public const string DeInstallDue = "De-install due";
    }
}

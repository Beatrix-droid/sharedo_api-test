using Newtonsoft.Json;
namespace ClientCredentials;    public class AppointmentTaskDetails
    {
        public bool allDayEvent { get; set; }
    }

    public class PaymentRequestAspectData
    {
        public PaymentRequestTask? task { get; set; }
        public AppointmentTaskDetails? appointmentTaskDetails { get; set; }
        public FormBuilder? formBuilder { get; set; }
    }

    public class FormBuilder
    {
        public FormData? formData { get; set; }

    }

    public class FormData
    {
        [JsonProperty(PropertyName="requisition-number")]
        public string requisitionNumber { get; set; }
        public string? anotherExampleField { get; set; }
    }

    public class PaymentRequestModel
    {
        public string paymentRequestId { get; set; }
        public string? parentSharedoId { get; set; }
        public string? supplierOdsId { get; set; }
        public string? rechargeToOdsId { get; set; }
        public string? title { get; set; }
        public string? details { get; set; }
        public string? reference { get; set; }
        public int? paymentRequestSubType { get; set; }
        public string? requestedDate { get; set; }
        public string? paymentDate { get; set; }
        public string? paymentDue { get; set; }
        public string? paymentMethod { get; set; }
        public string? invoiceNumber { get; set; }
        public string? invoiceDateTime { get; set; }
        public string? purchaseOrderNumber { get; set; }
        public string? purchaseOrderDateTime { get; set; }
        public List<TransactionItem>? transactionItems { get; set; }
    }

    public class UpdatePurchaseRequest

    {
        public PaymentRequestModel paymentRequestModel { get; set; }
        public PaymentRequestAspectData? aspectData { get; set; }
    }

    public class PaymentRequestTask
    {
        public string dueDateTime { get; set; }
    }

    public class TransactionItem
    {
        public string id { get; set; }
        public List<string> chartOfAccountsCodeIds { get; set; }
        public string transactionType { get; set; }
        public string? itemDescription { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
        public string? unitOfMeasure { get; set; }
        public int? quantity { get; set; }
        public double? vat { get; set; }
    }


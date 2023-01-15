// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace ClientCredentials{
    public class PaymentRequestInfo
    {
        public object reference { get; set; }
        public Title title { get; set; }
        public object sharedoTypeName { get; set; }
        public string typePath { get; set; }
        public object categoryName { get; set; }
        public object phaseName { get; set; }
        public object phaseNameWithAge { get; set; }
        public string id { get; set; }
    }

    public class AncestorRole
    {
    }

    public class Attr
    {
    }

    public class _Command
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

    public class _Data
    {
        public DateTime paymentRequestDate { get; set; }
        public object paymentDate { get; set; }
        public string paymentMethod { get; set; }
        public bool paymentDetails { get; set; }
        public PaymentAmount paymentAmount { get; set; }
        public object paymentBankDetail { get; set; }
        public object paymentBankSortCode { get; set; }
        public object paymentBankAccountNumber { get; set; }
        public object purchaseOrderNumber { get; set; }
        public object invoiceNumber { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public object dueDate { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public string categoryName { get; set; }
        public object priorityName { get; set; }
        public string sharedoTypeTileColour { get; set; }
        public string typePath { get; set; }
        public bool phaseIsOpen { get; set; }
        public List<object> worklistCount { get; set; }
        public object sharedoTypeName { get; set; }
        public object phaseName { get; set; }
        public PhaseNameWithAge phaseNameWithAge { get; set; }
        public List<object> tags { get; set; }
        public List<object> generalSummary { get; set; }
        public List<object> documentSummary { get; set; }
        public List<object> relatedItems { get; set; }
        public Role role { get; set; }
        public RefRole refRole { get; set; }
        public KeyDate keyDate { get; set; }
        public PaymentRequestInfo ancestor { get; set; }
        public AncestorRole ancestorRole { get; set; }
        public object appointmentTitle { get; set; }
        public object expectedStart { get; set; }
        public object expectedEnd { get; set; }
        public object actualStart { get; set; }
        public object actualEnd { get; set; }
        public bool canUpdateSchedule { get; set; }
        public object scheduleDueDateLinking { get; set; }
        public object keyDateLinkedPhaseSystemName { get; set; }
        public object keyDateLinkedPhaseLinkType { get; set; }
        public object emailFrom { get; set; }
        public object emailRecipientMailbox { get; set; }
        public object emailRecipients { get; set; }
        public Attr attr { get; set; }
    }

    public class KeyDate
    {
    }

    public class Menu
    {
        public string megaMenuContext { get; set; }
        public string megaMenuTypeContext { get; set; }
        public object title { get; set; }
        public string icon { get; set; }
        public bool commandForceNavigateNewWindow { get; set; }
    }

    public class OpenCommand
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

    public class PaymentAmount
    {
        public double value { get; set; }
        public string currencyCode { get; set; }
        public string currencySymbol { get; set; }
    }

    public class PhaseNameWithAge
    {
        public string icon { get; set; }
        public object colour { get; set; }
        public string text { get; set; }
        public object tooltip { get; set; }
    }

    public class RefRole
    {
    }

    public class Role
    {
        public string creator { get; set; }
        public string supplier { get; set; }
    }

    public class Root
    {
        public int resultCount { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Row
    {
        public string id { get; set; }
        public string colour { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public string reference { get; set; }
        public OpenCommand openCommand { get; set; }
        public ViewCommand viewCommand { get; set; }
        public List<Menu> menu { get; set; }
        public object cardViewActions { get; set; }
        public bool enableDragDrop { get; set; }
        public object dragDropBlade { get; set; }
        public _Data data { get; set; }
    }

    public class Title
    {
        public string text { get; set; }
        public _Command command { get; set; }
        public string directUrl { get; set; }
    }

    public class ViewCommand
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

}
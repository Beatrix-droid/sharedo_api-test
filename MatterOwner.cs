namespace ClientCredentials;
    public class MatterOwneAction
    {
        public string name { get; set; }
        public string iconClass { get; set; }
        public MatterOwnerCommand MatterOwnerCommand { get; set; }
    }

    public class AddMatterOwnerCommand
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

    public class AdditionalOdsLocation
    {
        public string id { get; set; }
        public string organisationId { get; set; }
        public object personId { get; set; }
        public string name { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public object addressLine3 { get; set; }
        public string addressLine4 { get; set; }
        public string town { get; set; }
        public object county { get; set; }
        public string countrySystemName { get; set; }
        public object country { get; set; }
        public object countryIsoCode { get; set; }
        public object dxName { get; set; }
        public object dxNumber { get; set; }
        public bool isActive { get; set; }
        public object departmentCode { get; set; }
        public object externalReference { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object geocodeType { get; set; }
        public object bagBoxNumber { get; set; }
        public object companyName { get; set; }
        public string postCode { get; set; }
    }

    public class AllowedParticipantType
    {
        public string systemName { get; set; }
        public string name { get; set; }
        public string iconClass { get; set; }
        public AddMatterOwnerCommand addMatterOwnerCommand { get; set; }
        public string odsEntityTypeSystemName { get; set; }
    }

    public class MatterOwnerCommand
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

    public class ContactDetail
    {
        public bool showOnForm { get; set; }
        public int contactTypeDisplayOrder { get; set; }
        public bool isPrimary { get; set; }
        public bool isPreferredContact { get; set; }
        public List<MatterOwneAction> MatterOwneActions { get; set; }
        public int displayOrder { get; set; }
        public string id { get; set; }
        public string value { get; set; }
        public string contactTypeSystemName { get; set; }
        public string contactTypeName { get; set; }
    }

    public class Location
    {
        public int locationTypeId { get; set; }
        public string locationTypeName { get; set; }
        public string id { get; set; }
        public object organisationId { get; set; }
        public object personId { get; set; }
        public string name { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string addressLine4 { get; set; }
        public string town { get; set; }
        public string county { get; set; }
        public string countrySystemName { get; set; }
        public object country { get; set; }
        public object countryIsoCode { get; set; }
        public object dxName { get; set; }
        public object dxNumber { get; set; }
        public bool isActive { get; set; }
        public object departmentCode { get; set; }
        public object externalReference { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public object geocodeType { get; set; }
        public object bagBoxNumber { get; set; }
        public string companyName { get; set; }
        public string postCode { get; set; }
    }

    public class LocationType
    {
        public int locationTypeId { get; set; }
        public bool isMandatory { get; set; }
    }

    public class MatterOwnerOpenMatterOwnerCommand
    {
        public string invokeType { get; set; }
        public string invoke { get; set; }
        public string config { get; set; }
    }

    public class Organisation
    {
        public string id { get; set; }
        public object parentId { get; set; }
        public object sourceSystem { get; set; }
        public string name { get; set; }
        public object shortName { get; set; }
        public object industrySICCodeId { get; set; }
        public object isVATRegistered { get; set; }
        public object vATCountrySystemName { get; set; }
        public object vATNumber { get; set; }
        public object vATComments { get; set; }
        public object companyNumber { get; set; }
        public object previousName { get; set; }
        public object registeredName { get; set; }
        public object registeredShortName { get; set; }
        public object statusId { get; set; }
        public bool isDefault { get; set; }
        public bool isActive { get; set; }
        public object reference { get; set; }
        public bool shortNameIsGenerated { get; set; }
        public object externalReference { get; set; }
    }

    public class Participant
    {
        public string participantId { get; set; }
        public string participantType { get; set; }
        public string participantTypeIconClass { get; set; }
        public MatterOwnerOpenMatterOwnerCommand MatterOwnerOpenMatterOwnerCommand { get; set; }
        public string odsId { get; set; }
        public string odsName { get; set; }
        public object odsPresenceIdentity { get; set; }
        public string odsEntityType { get; set; }
        public bool hasOdsId { get; set; }
        public bool isUser { get; set; }
        public UserAvailability userAvailability { get; set; }
        public Organisation organisation { get; set; }
        public bool canEditOdsEntity { get; set; }
        public string sharedoId { get; set; }
        public string sharedoTitle { get; set; }
        public List<MatterOwnerRole> MatterOwnerRoles { get; set; }
        public List<MatterOwnerRoleLocationType> MatterOwnerRoleLocationTypes { get; set; }
        public List<Location> locations { get; set; }
        public List<AdditionalOdsLocation> additionalOdsLocations { get; set; }
        public List<ContactDetail> contactDetails { get; set; }
        public List<object> connections { get; set; }
        public List<object> odsEntityAdditionalDetails { get; set; }
        public bool hasConnectionLocations { get; set; }
    }

    public class MatterOwnerRole
    {
        public string systemName { get; set; }
        public string name { get; set; }
        public string iconClass { get; set; }
        public bool isMandatory { get; set; }
        public bool isRecommended { get; set; }
        public bool isCaseTeamMatterOwnerRole { get; set; }
        public bool isSecurityTeamMatterOwnerRole { get; set; }
        public bool isManuallyAssignable { get; set; }
        public bool canBeOrdered { get; set; }
        public bool canHaveMultiple { get; set; }
        public bool canSelectFromOds { get; set; }
        public bool canSelectFromSharedo { get; set; }
        public bool canSelectFromAncestors { get; set; }
        public bool hasAspects { get; set; }
        public string categoryName { get; set; }
        public List<LocationType> locationTypes { get; set; }
        public List<AllowedParticipantType> allowedParticipantTypes { get; set; }
        public string sharedoId { get; set; }
        public string sharedoTypeSystemName { get; set; }
        public string sharedoTypeName { get; set; }
        public object MatterOwnerRoleOrder { get; set; }
    }

    public class MatterOwnerRoleLocationType
    {
        public int id { get; set; }
        public string optionSetName { get; set; }
        public object parentValueId { get; set; }
        public string name { get; set; }
        public object shortName { get; set; }
        public object iconClass { get; set; }
        public bool isActive { get; set; }
        public object meaningCode { get; set; }
        public object migrationId { get; set; }
        public object displayColour { get; set; }
        public bool deleted { get; set; }
        public bool defaultActive { get; set; }
        public int displayOrder { get; set; }
    }

    public class Root
    {
        public List<MatterOwnerRole> MatterOwnerRoles { get; set; }
        public List<Participant> participants { get; set; }
    }

    public class UserAvailability
    {
        public string displayName { get; set; }
        public string icon { get; set; }
        public string css { get; set; }
        public object absenceStartDate { get; set; }
        public object absenceEndDate { get; set; }
        public bool hasAbsenceDates { get; set; }
        public bool autoTaskDelegationActive { get; set; }
        public object autoTaskDelegateeOdsId { get; set; }
        public object autoTaskDelegateeOdsName { get; set; }
    }


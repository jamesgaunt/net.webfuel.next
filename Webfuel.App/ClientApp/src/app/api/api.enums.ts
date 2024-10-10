
export enum QueryOp {
    None = "none",
    Equal = "eq",
    NotEqual = "neq",
    GreaterThan = "gt",
    GreaterThanOrEqual = "gte",
    LessThan = "lt",
    LessThanOrEqual = "lte",
    Contains = "contains",
    StartsWith = "startswith",
    EndsWith = "endswith",
    SQL = "sql",
    And = "and",
    Or = "or",
}

export enum WidgetTypeEnum {
    ProjectSummary = "4f5bed07-af06-40c5-a7ed-ad283e57e503",
    TeamSupport = "54d1db4b-2b37-4d20-9e55-ffe2529446ac",
}

export enum ReportFieldType {
    Unspecified = 0,
    String = 10,
    Number = 20,
    Boolean = 30,
    DateTime = 40,
    Date = 50,
    Reference = 1000,
}

export enum ReportColumnCollection {
    Default = 0,
    Sum = 100,
    Avg = 200,
    Min = 300,
    Max = 400,
    Count = 500,
    List = 1000,
    ListDistinct = 1100,
}

export enum ReportColumnAggregation {
    None = 0,
    Group = 10,
    Sum = 100,
    Avg = 200,
    Min = 300,
    Max = 400,
    Count = 500,
    List = 1000,
    ListDistinct = 1100,
}

export enum ReportColumnTypeIdentifiers {
    Expression = "00000000-0000-0000-0000-000000000001",
}

export enum ReportFilterType {
    String = 10,
    Number = 20,
    Boolean = 30,
    Date = 50,
    Reference = 200,
    Group = 1000,
    Expression = 2000,
}

export enum ReportFilterBooleanCondition {
    True = 10,
    False = 20,
    Any = 99,
}

export enum ReportFilterDateCondition {
    EqualTo = 10,
    LessThan = 20,
    LessThanOrEqualTo = 30,
    GreaterThan = 40,
    GreaterThanOrEqualTo = 50,
    IsSet = 1000,
    IsNotSet = 1001,
}

export enum ReportFilterGroupCondition {
    All = 10,
    Any = 20,
    None = 30,
}

export enum ReportFilterNumberCondition {
    EqualTo = 10,
    LessThan = 20,
    LessThanOrEqualTo = 30,
    GreaterThan = 40,
    GreaterThanOrEqualTo = 50,
    IsSet = 1000,
    IsNotSet = 1001,
}

export enum ReportFilterReferenceCondition {
    OneOf = 10,
    NotOneOf = 20,
    AllOf = 30,
    Only = 40,
    AllOfAndOnly = 41,
    IsSet = 1000,
    IsNotSet = 1001,
}

export enum ReportFilterStringCondition {
    Contains = 10,
    StartsWith = 20,
    EndsWith = 30,
    EqualTo = 40,
    IsEmpty = 1000,
    IsNotEmpty = 1001,
}

export enum ReportFilterTypeIdentifiers {
    Group = "00000000-0000-0000-0000-000000000001",
    Expression = "00000000-0000-0000-0000-000000000002",
}

export enum IsCTUAlreadyInvolvedEnum {
    YesWeHaveAlreadyGotACTUInvolved = "d51d2c40-578a-4797-b728-943b6dd9e87c",
    NoWeHaveNotGotACTUInvolved = "78ccf6a2-e166-4774-9c0c-aedcb57fd789",
}

export enum IsCTUTeamContributionEnum {
    Yes = "3e1f2673-e708-44a8-8768-cbf9fe1afb7c",
    No = "5b18a6b2-1169-4f7c-91b7-e738ea4527c6",
}

export enum IsFellowshipEnum {
    YesThisApplicationIsForAFellowship = "d8f6423c-00da-49df-94aa-24bad595858d",
    NoThisApplicationIsNotForAFellowship = "65f431cb-e66a-4f36-b457-f783266827da",
}

export enum IsInternationalMultiSiteStudyEnum {
    Yes = "744bb5e0-399e-4c3c-a92f-25ffcc4dbd7d",
    No = "853f0502-651a-44d6-99c2-1ee731483c88",
}

export enum IsLeadApplicantNHSEnum {
    YesTheyAre = "616ee238-466d-4f5e-9eab-5b3b48b95bc9",
    NoTheyAreNot = "f9aa774f-616e-481a-9137-e5d55a9c99bd",
}

export enum IsPaidRSSAdviserCoapplicantEnum {
    No = "4f8c3cb5-d96a-4cfe-b04c-35dc8b925b15",
    Yes = "22f78a71-0e8c-4047-82b3-305736b2d3cc",
}

export enum IsPaidRSSAdviserLeadEnum {
    No = "b2a969e5-6dff-4ef4-a629-a3cbd5105275",
    Yes = "05213f9c-e901-4e63-82f8-4b558415f9ad",
}

export enum IsPPIEAndEDIContributionEnum {
    Yes = "547241ba-72d4-4901-bc1e-5fffc9aaadfa",
    No = "c2c2e7b5-f64d-421b-b4c7-de34cebbef53",
}

export enum IsPrePostAwardEnum {
    PreAward = "d8c2fe26-3a35-4a49-b61d-b5abc41611f6",
    PostAward = "6b7d87be-4ba2-4b47-9de5-42672114fcaf",
}

export enum IsQuantativeTeamContributionEnum {
    Yes = "8192a0f7-a0c9-4e57-a36a-42383a3832d9",
    No = "30923282-7f55-48b2-bc34-2a7c822401d9",
}

export enum IsResubmissionEnum {
    YesThisApplicationIsAResubmission = "0cdc79cc-6053-4163-9661-2aff37dacf30",
    NoThisApplicationIsNotAResubmission = "ab294a68-3b87-4809-8923-2b06fa9e2958",
}

export enum IsTeamMembersConsultedEnum {
    YesTeamMembersHaveBeenConsulted = "01340892-ce30-4c07-8ff3-90f5b9898b8c",
    NoTeamMembersHaveNotBeenConsulted = "b5f5f413-e742-494d-a22a-4ecfa1d52ce8",
}

export enum IsYesNoEnum {
    Yes = "3fd6c70b-b87a-45bc-84bc-c71802ef3276",
    No = "9c2dbdff-9020-419a-aa3f-bb976b19de6a",
}

export enum ProfessionalBackgroundEnum {
    AlliedHealthcareProfessional = "9cd4cbde-011e-4b8b-898f-4a23a80d8e1f",
    Doctor = "e031dcc1-a8c7-4a26-9a89-f5246da76596",
    Dentist = "22dc26e7-8685-401e-b8bd-d1d957e27f0d",
    Midwife = "eeed9783-71e4-45b9-82da-0161838d5080",
    Psychologist = "0ee15892-343e-4117-9362-0abd68761bce",
    Nurse = "c2161d6e-c39c-4ee0-9d9d-45126f3ac03a",
    PublicHealthSpecialist = "897dcab9-fda6-403e-aa8d-c3dde2043d57",
    SocialCareSpecialist = "91706bb9-c8ff-4f03-9eb6-b70d6510513c",
    OtherHealthcareProfessional = "3ae2a25d-8993-4bdc-acec-8e345ecec728",
    NotHealthcareProfessional = "db90675f-5225-43bc-87c0-53ba9267c19e",
}

export enum ProjectStatusEnum {
    Active = "6c83e9e4-617b-4386-b087-16a11f6b24af",
    OnHold = "691ee44b-7b01-4af0-b8f3-13f96f00f0ce",
    SubmittedOnHold = "e66a0fdb-8d2e-4f4a-b813-a0a487e8c25d",
    Closed = "ed4845b0-1f4c-4df3-b4ec-46e5ce94c275",
    Discarded = "164fdeee-8d6f-42fa-a23b-fbab0ef3ba93",
}

export enum ReportProviderEnum {
    Project = "b8b6dea3-d6de-4480-a944-e7b0c2827888",
    ProjectSupport = "2af96eb5-e52d-4163-9247-c34e7b170f62",
    ProjectSubmission = "62705a56-e9f2-4473-a966-e745120f795f",
    SupportRequest = "bb41b6b6-ef1a-4982-95d0-9f41f80c91cb",
    User = "96124eea-4434-4669-b377-580bbf85a96d",
    UserActivity = "3fd56f37-05f8-4418-bc57-d79a393e0ee9",
    CustomReport = "a2858ded-2b44-4fed-ab7d-8fb28850a340",
}

export enum ResearcherRoleEnum {
    ChiefInvestigator = "4c6fbfca-3601-4c24-81aa-d372e0d0320e",
    ResearchFellowAssociate = "1c4b0501-821c-4a62-a560-f50b42d55657",
    FellowshipApplicant = "d2791155-7b9e-416f-a169-9ecc7b1aa0cc",
    CoInvestigator = "93a1de9c-ccbe-4bc4-8896-e34e97d217af",
    Administator = "c26a348a-1581-4678-8d69-aa218d11bb36",
    Other = "45013058-6dc8-4a3c-90e4-49a4da46efc8",
}

export enum SubmissionOutcomeEnum {
    Shortlisted = "36969c16-31b1-4c1e-81eb-b4ccf626a98c",
    Rejected = "bdbf02dc-75a7-413c-b8f5-7c610a6695aa",
    NotSubmitted = "4f8dcf2d-75bd-449d-9631-fcecbea1b935",
    Funded = "38dc9caf-820d-4980-8f00-6bc260092a96",
    Unknown = "32b584a9-c539-447b-a542-df3e5283ef3c",
    Withdrawn = "b60f7e48-d744-403e-8fc1-f23445cb40c5",
    NotApplicable = "b785f487-82fa-4149-8c7d-4a615ca90420",
    SubmittedAwaitingOutcome = "0c64df76-2110-436a-96c2-0c2774e8a3b4",
}

export enum SubmissionStageEnum {
    Stage1OrOutline = "18174faf-8787-480e-b0b4-5698a5e59d93",
    Stage2OrInterview = "deecb94c-8284-45c8-b499-1c0c099215b0",
    OneStage = "aa13a2ae-f1dd-4b8b-959e-15bdff000fee",
    NA = "af170296-2c64-4a39-8fb8-6ddd6f8d3137",
    Unknown = "174a713b-aff7-432b-8e7a-eede783a6067",
}

export enum SubmissionStatusEnum {
    Yes = "82a179e1-9a82-4a68-a883-5800bc2000d4",
    ClientDidNotSubmit = "6f8e9238-6b7e-4bd4-97ce-a0584d802ee4",
    DonTKnow = "f01c0b96-7feb-490e-b919-331df4fc47a0",
    NA = "f319f56f-8434-45af-96fb-003bd29774b2",
}

export enum SupportRequestStatusEnum {
    ToBeTriaged = "206b6f9d-feee-4b28-9795-1e1a2b7b887f",
    OnHold = "667ccd51-00f4-4890-9d54-3c925332124d",
    ClosedNotEligibleForNIHRRSS = "1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d",
    ClosedReferredToAnotherNIHRRSS = "3d344c3c-9964-4079-877e-bc3b6864f52d",
    ClosedOutOfRemit = "529aaebf-5e02-4003-825e-265a6271c816",
    ReferredToNIHRRSSExpertTeams = "c22df21d-30ed-49d5-bee0-0f304b74a365",
}

export enum SupportTeamEnum {
    TriageTeam = "4b013d8c-7aad-4100-9586-83054ef15bac",
    CTUTeam = "9d4b32b9-7e7e-42c5-8f08-82de256fbcf0",
    PPIETeam = "662067e9-875c-47f7-8427-26ac98078c20",
    ExpertQuantitativeTeam = "5283e9be-faf3-45f5-83b2-4c5288c090c3",
    ExpertQualitativeTeam = "b1bdcae3-7df9-404a-aad7-a9265392b5aa",
}

export enum WillStudyUseCTUEnum {
    No = "8ba8e39c-8792-4901-9a74-2788e972ac60",
    YesExternalToThisRSS = "4672a5ec-1c83-4a66-b0a8-a797db9e59a9",
    YesInternalToThisRSS = "fc4ddc3d-9b1c-4cb6-970c-94c122ef2fdc",
}

export enum ProjectDiagnosticSeverity {
    Error = 10,
    Warning = 20,
}



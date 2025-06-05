
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

export enum WidgetTaskStatus {
    Deferred = 0,
    Processing = 10,
    Complete = 20,
    Cancelled = 999,
}

export enum WidgetTypeEnum {
    ProjectSummary = "4f5bed07-af06-40c5-a7ed-ad283e57e503",
    TeamSupport = "54d1db4b-2b37-4d20-9e55-ffe2529446ac",
    TriageSummary = "3f9d5f90-87bd-4257-828e-95fa04e55d7a",
    TeamActivity = "cf48d41d-934b-4fc8-b368-52ee87d5e76d",
    LeadAdviserProjects = "866208a8-c266-4e56-ad3e-717491cf0089",
    SupportAdviserProjects = "391677a2-119d-4c18-982b-27769abe42bd",
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

export enum FullOutcomeEnum {
    Successful = "c9dbd5ad-6c78-4773-a3a8-ce30412bb5dc",
    NotFunded = "6a59516c-9472-4c63-9611-f651bdbfcff2",
    Withdrawn = "cd81994e-1bc1-4404-a023-eea57e3fd114",
    Unknown = "2b33d4c2-0340-4a57-9c75-203607fed107",
    NotSubmitted = "f73be486-7d1d-4573-b76c-c0fda7cab4c5",
    NotApplicable = "ba13b285-2429-4952-8b79-fc515eeaccbf",
}

export enum FullSubmissionStatusEnum {
    Yes = "a40cad90-8975-4165-870c-52a4c71b9869",
    No = "1f869a5b-0f34-4e88-8a2a-22d18e0c16f4",
    DonTKnow = "081286d1-6817-4a77-9506-3308bd04eab1",
    NA = "64407686-d81e-482f-8188-5bed6ac7b788",
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

export enum OutlineOutcomeEnum {
    Shortlisted = "e8f599eb-cc36-4e4c-9500-462f0a2213d0",
    NotFunded = "917d8a55-1d9a-4539-ac25-fec36a2ff8a7",
    Withdrawn = "962cb041-bcef-4b5f-8e2e-81da23b7b7be",
    Unknown = "caab14a7-801b-43e0-b9a9-1c08eb86e197",
    NotSubmitted = "b4261645-69f3-4c61-a058-459a7746434a",
    NotApplicable = "7f73e3e4-c886-43a2-8933-cfaa7cdabd9e",
}

export enum OutlineSubmissionStatusEnum {
    Yes = "1c76e5b1-c0d4-4a91-8144-afb582eb2165",
    No = "dcd582e2-3ca3-4705-8eb8-d64953c1d1c1",
    DonTKnow = "7a4b2966-eb01-4765-9615-d873915be554",
    NA = "c9a32676-5e38-42e7-bb31-586cb9e5e1f5",
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
    ClosedNotSubmitted = "c7b88dec-e286-46d6-857e-9b48751e85dc",
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

export enum ResearcherProfessionalBackgroundEnum {
    ClinicalAcademic = "75b769e7-9e9d-42df-b31d-c906d353d4d0",
    Academic = "843db2be-bd3a-4f61-ac48-e1932e55f076",
    NonAcademicClinician = "c3dcef26-d476-41bf-9a9c-89b008addf58",
    NurseMidwifeAHP = "d16a4d18-5309-4fb4-b6f7-7f9bcf56c6a6",
    NHSManager = "e4a7b2a1-1f65-4201-9608-ed282955263c",
    SocialCarePractitioner = "89ffaa61-4d5c-4095-bb5c-36d8eb8248e1",
    PublicHealthPractitioner = "5f329e4a-1881-41b1-886f-9706eea45e20",
    SME = "f10651e2-aedb-41cd-9ee4-750b3d7af17c",
    OtherNHS = "3cfcf7e5-4230-4083-a83a-5723c49beb82",
    LivedExperiencePublicCommunityContributor = "26dbb5f4-d544-48cc-9084-79a7027d13df",
    LocalAuthorityOfficerPolicymaker = "887e71fa-3ba7-49d4-adb1-827ffe0de611",
    Other = "0943bb8e-42b8-4f14-870c-6a6a2bce1800",
    DonTKnow = "d9d6683b-f3e6-439a-86e5-937cb21e3d99",
}

export enum ResearcherRoleEnum {
    ChiefInvestigator = "4c6fbfca-3601-4c24-81aa-d372e0d0320e",
    ResearchFellowAssociate = "1c4b0501-821c-4a62-a560-f50b42d55657",
    FellowshipApplicant = "d2791155-7b9e-416f-a169-9ecc7b1aa0cc",
    CoInvestigator = "93a1de9c-ccbe-4bc4-8896-e34e97d217af",
    Administator = "c26a348a-1581-4678-8d69-aa218d11bb36",
    Other = "45013058-6dc8-4a3c-90e4-49a4da46efc8",
}

export enum RSSHubEnum {
    NewcastleUniversityAndPartners = "02f2c7d3-691f-426d-b444-eb802c562715",
    UniversityOfBirminghamAndPartners = "17aa2cae-1616-4f26-94ff-b8a4617680f0",
    UniversityOfLancasterAndPartners = "d44377e3-2b75-4769-9b08-f24977aed598",
    UniversityOfYorkAndPartners = "003b6f75-efbe-443b-86d3-186f2a3cc584",
    ImperialCollegeLondonAndPartners = "9e4f4c12-022d-4769-abdf-71b40f2936bd",
    UniversityOfSouthamptonAndPartners = "d5678978-ea52-4923-bed9-e4c2fcada10d",
    KingsCollegeLondonAndPartners = "7a2a0314-6d06-4b32-a1eb-3dd96ab0ba6f",
    UniversityOfLeicesterAndPartners = "01cc53bc-088d-4ab6-a0fe-09fcbc164cd0",
    SouthamptonAndPartnersPublicHealthSpecialistCentre = "24f9f0e6-3d95-4c94-899d-f078903a9daa",
    NewcastleAndPartnersPublicHealthSpecialistCentre = "979dc9c7-e454-49ad-974f-f424d5470a80",
    LancasterUniversityAndPartnersSocialCareSpecialistCentre = "282014e1-ba60-4d92-af70-971ab918c04a",
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

export enum ProjectDiagnosticType {
    Enrichment = 10,
    Deferred = 20,
}

export enum ProjectDiagnosticSeverity {
    Error = 10,
    Warning = 20,
}



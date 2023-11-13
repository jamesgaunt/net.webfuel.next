
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
    And = "and",
    Or = "or",
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

export enum IsPPIEAndEDIContributionEnum {
    Yes = "547241ba-72d4-4901-bc1e-5fffc9aaadfa",
    No = "c2c2e7b5-f64d-421b-b4c7-de34cebbef53",
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

export enum ProjectStatusEnum {
    Active = "6c83e9e4-617b-4386-b087-16a11f6b24af",
    Inactive = "15787677-fc83-4071-961a-1da5b5f41018",
    Closed = "ed4845b0-1f4c-4df3-b4ec-46e5ce94c275",
    Discarded = "164fdeee-8d6f-42fa-a23b-fbab0ef3ba93",
}

export enum SubmissionOutcomeEnum {
    Unknown = "32b584a9-c539-447b-a542-df3e5283ef3c",
    Shortlisted = "36969c16-31b1-4c1e-81eb-b4ccf626a98c",
    Rejected = "bdbf02dc-75a7-413c-b8f5-7c610a6695aa",
    Withdrawn = "b60f7e48-d744-403e-8fc1-f23445cb40c5",
    FundedFullStageOnly = "38dc9caf-820d-4980-8f00-6bc260092a96",
    NotApplicable = "b785f487-82fa-4149-8c7d-4a615ca90420",
}

export enum SubmissionStageEnum {
    OutlineStageStage1 = "18174faf-8787-480e-b0b4-5698a5e59d93",
    Stage2 = "deecb94c-8284-45c8-b499-1c0c099215b0",
    OneStageApplication = "aa13a2ae-f1dd-4b8b-959e-15bdff000fee",
    AcceleratorAward = "3e658c3b-11fc-464b-9296-7bf369866bce",
}

export enum SupportRequestStatusEnum {
    ToBeTriaged = "206b6f9d-feee-4b28-9795-1e1a2b7b887f",
    ClosedAsNotEligibleFromNIHRRSS = "1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d",
    ClosedAsReferredToAnotherNIHRRSS = "3d344c3c-9964-4079-877e-bc3b6864f52d",
    ReferredToNIHRRSSExpertTeams = "c22df21d-30ed-49d5-bee0-0f304b74a365",
}

export enum SupportTeamEnum {
    TriageTeam = "4b013d8c-7aad-4100-9586-83054ef15bac",
    CTUTeam = "9d4b32b9-7e7e-42c5-8f08-82de256fbcf0",
    PPIETeam = "662067e9-875c-47f7-8427-26ac98078c20",
    ExpertQuantitativeTeam = "5283e9be-faf3-45f5-83b2-4c5288c090c3",
    ExpertQualitativeTeam = "b1bdcae3-7df9-404a-aad7-a9265392b5aa",
}



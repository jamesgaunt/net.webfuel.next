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

export interface ClientConfiguration {
    userId: string;
    email: string;
    sideMenu: ClientConfigurationMenu;
    settingsMenu: ClientConfigurationMenu;
    staticDataMenu: ClientConfigurationMenu;
    claims: IdentityClaims;
}

export interface ClientConfigurationMenu {
    icon: string;
    name: string;
    action: string;
    children: Array<ClientConfigurationMenu> | null;
}

export interface IdentityClaims {
    developer: boolean;
    canEditUsers: boolean;
    canEditUserGroups: boolean;
    canEditStaticData: boolean;
    canEditReports: boolean;
    canUnlockProjects: boolean;
    canTriageSupportRequests: boolean;
    canAccessConfiguration: boolean;
}

export interface ProblemDetails {
    type: string;
    title: string;
    detail: string;
    status: number;
}

export interface ValidationProblemDetails extends ProblemDetails {
    validationErrors: Array<ValidationError>;
    type: string;
    title: string;
    detail: string;
    status: number;
}

export interface ValidationError {
    property: string;
    message: string;
}

export interface UploadFileStorageEntry {
    fileStorageGroupId: string;
}

export interface ReportColumn {
    id: string;
    fieldId: string;
    fieldName: string;
    fieldType: ReportFieldType;
    multiValued: boolean;
    title: string;
    width: number | null | null;
    bold: boolean;
    expression: string;
    collection: ReportColumnCollection;
}

export interface ReportFilter {
    filterType: ReportFilterType;
    id: string;
    name: string;
    displayName: string;
    description: string;
    editable: boolean;
    conditions: Array<ReportFilterCondition>;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterCondition {
    value: number;
    description: string;
    unary: boolean;
}

export interface ReportFilterBoolean extends ReportFilterField {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterField extends ReportFilter {
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    filterType: ReportFilterType;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    conditions: Array<ReportFilterCondition>;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterDate extends ReportFilterField {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    value: string;
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterExpression extends ReportFilter {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    displayName: string;
    expression: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterGroup extends ReportFilter {
    filterType: ReportFilterType;
    displayName: string;
    conditions: Array<ReportFilterCondition>;
    filters: Array<ReportFilter>;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterNumber extends ReportFilterField {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    value: number;
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterReference extends ReportFilterField {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    value: Array<string>;
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportFilterString extends ReportFilterField {
    filterType: ReportFilterType;
    conditions: Array<ReportFilterCondition>;
    value: string;
    fieldId: string;
    fieldName: string;
    multiValued: boolean;
    displayName: string;
    id: string;
    name: string;
    description: string;
    editable: boolean;
    condition: number;
    conditionDescription: string;
}

export interface ReportDesign {
    reportProviderId: string;
    columns: Array<ReportColumn>;
    latestColumnId: string;
    filters: Array<ReportFilter>;
    latestFilterId: string;
}

export interface ReportField {
    id: string;
    name: string;
    fieldType: ReportFieldType;
    multiValued: boolean;
    filterable: boolean;
}

export interface IReportAccessor {
}

export interface DashboardModel {
    supportTeamMetrics: Array<DashboardMetric>;
    projectMetrics: Array<DashboardMetric>;
}

export interface DashboardMetric {
    name: string;
    count: number | null | null;
    icon: string;
    cta: string;
    routerLink: string;
    routerParams: string;
    backgroundColor: string;
}

export interface EmailTemplate {
    id: string;
    name: string;
    sendTo: string;
    sendCc: string;
    sendBcc: string;
    sentBy: string;
    replyTo: string;
    subject: string;
    htmlTemplate: string;
}

export interface CreateEmailTemplate {
    name: string;
}

export interface UpdateEmailTemplate {
    id: string;
    name: string;
    sendTo: string;
    sendCc: string;
    sendBcc: string;
    sentBy: string;
    replyTo: string;
    subject: string;
    htmlTemplate: string;
}

export interface QueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface QueryEmailTemplate extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryFilter {
    field?: string;
    op: string;
    value?: any | null;
    filters?: Array<QueryFilter> | null;
}

export interface QuerySort {
    field: string;
    direction: number;
}

export interface Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface FileStorageEntry {
    id: string;
    fileName: string;
    sizeBytes: number;
    uploadedAt: string | null | null;
    description: string;
    fileStorageGroupId: string;
    uploadedByUserId: string | null | null;
}

export interface QueryFileStorageEntry extends Query {
    fileStorageGroupId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface Heartbeat {
    id: string;
    name: string;
    live: boolean;
    sortOrder: number;
    providerName: string;
    providerParameter: string;
    minTime: string;
    maxTime: string;
    schedule: string;
    nextExecutionScheduledAt: string | null | null;
    schedulerExceptionMessage: string;
    logSuccessfulExecutions: boolean;
    lastExecutionAt: string | null | null;
    lastExecutionMessage: string;
    lastExecutionSuccess: boolean;
    lastExecutionMicroseconds: number;
    lastExecutionMetadataJson: string;
    recentExecutionSuccessCount: number;
    recentExecutionFailureCount: number;
    recentExecutionMicrosecondsAverage: number;
}

export interface CreateHeartbeat {
    name: string;
}

export interface UpdateHeartbeat {
    id: string;
    name: string;
    live: boolean;
    logSuccessfulExecutions: boolean;
    providerName: string;
    providerParameter: string;
    minTime: string;
    maxTime: string;
    schedule: string;
}

export interface QueryHeartbeat extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface PingResponse {
    timestamp: string;
}

export interface IStaticDataModel {
    ageRange: Array<AgeRange>;
    applicationStage: Array<ApplicationStage>;
    disability: Array<Disability>;
    ethnicity: Array<Ethnicity>;
    fundingBody: Array<FundingBody>;
    fundingCallType: Array<FundingCallType>;
    fundingStream: Array<FundingStream>;
    gender: Array<Gender>;
    howDidYouFindUs: Array<HowDidYouFindUs>;
    isCTUAlreadyInvolved: Array<IsCTUAlreadyInvolved>;
    isCTUTeamContribution: Array<IsCTUTeamContribution>;
    isFellowship: Array<IsFellowship>;
    isInternationalMultiSiteStudy: Array<IsInternationalMultiSiteStudy>;
    isLeadApplicantNHS: Array<IsLeadApplicantNHS>;
    isPPIEAndEDIContribution: Array<IsPPIEAndEDIContribution>;
    isQuantativeTeamContribution: Array<IsQuantativeTeamContribution>;
    isResubmission: Array<IsResubmission>;
    isTeamMembersConsulted: Array<IsTeamMembersConsulted>;
    projectStatus: Array<ProjectStatus>;
    reportProvider: Array<ReportProvider>;
    researcherOrganisationType: Array<ResearcherOrganisationType>;
    researcherRole: Array<ResearcherRole>;
    researchMethodology: Array<ResearchMethodology>;
    site: Array<Site>;
    submissionOutcome: Array<SubmissionOutcome>;
    submissionStage: Array<SubmissionStage>;
    supportProvided: Array<SupportProvided>;
    supportRequestStatus: Array<SupportRequestStatus>;
    supportTeam: Array<SupportTeam>;
    title: Array<Title>;
    userDiscipline: Array<UserDiscipline>;
    workActivity: Array<WorkActivity>;
    loadedAt: string;
}

export interface AgeRange extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface ApplicationStage extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface Disability extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface Ethnicity extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface FundingBody extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface FundingCallType extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
}

export interface FundingStream extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface Gender extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface HowDidYouFindUs extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface IsCTUAlreadyInvolved extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    freeText: boolean;
}

export interface IsCTUTeamContribution extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsFellowship extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsInternationalMultiSiteStudy extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsLeadApplicantNHS extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsPPIEAndEDIContribution extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsQuantativeTeamContribution extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsResubmission extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IsTeamMembersConsulted extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface ProjectStatus extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    locked: boolean;
    discarded: boolean;
}

export interface ReportProvider extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
}

export interface ResearcherOrganisationType extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface ResearcherRole extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface ResearchMethodology extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface Site extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface SubmissionOutcome extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SubmissionStage extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SupportProvided extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SupportRequestStatus extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SupportTeam extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
}

export interface Title extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface UserDiscipline extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface WorkActivity extends IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface Project {
    id: string;
    number: number;
    prefixedNumber: string;
    supportRequestId: string | null | null;
    closureDate: string | null | null;
    submittedFundingStreamFreeText: string;
    submittedFundingStreamName: string;
    locked: boolean;
    discarded: boolean;
    projectStartDate: string | null | null;
    recruitmentTarget: number | null | null;
    numberOfProjectSites: number | null | null;
    dateOfRequest: string;
    title: string;
    applicationStageFreeText: string;
    proposedFundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    howDidYouFindUsFreeText: string;
    whoElseIsOnTheStudyTeam: string;
    isCTUAlreadyInvolvedFreeText: string;
    teamContactTitle: string;
    teamContactFirstName: string;
    teamContactLastName: string;
    teamContactEmail: string;
    teamContactRoleFreeText: string;
    teamContactMailingPermission: boolean;
    teamContactPrivacyStatementRead: boolean;
    leadApplicantTitle: string;
    leadApplicantFirstName: string;
    leadApplicantLastName: string;
    leadApplicantEmail: string;
    leadApplicantJobRole: string;
    leadApplicantOrganisation: string;
    leadApplicantDepartment: string;
    leadApplicantAddressLine1: string;
    leadApplicantAddressLine2: string;
    leadApplicantAddressTown: string;
    leadApplicantAddressCounty: string;
    leadApplicantAddressCountry: string;
    leadApplicantAddressPostcode: string;
    leadApplicantORCID: string;
    searchTeamContactFullName: string;
    createdAt: string;
    fileStorageGroupId: string;
    leadAdviserUserId: string | null | null;
    submittedFundingStreamId: string | null | null;
    statusId: string;
    isInternationalMultiSiteStudyId: string | null | null;
    isFellowshipId: string | null | null;
    applicationStageId: string | null | null;
    proposedFundingCallTypeId: string | null | null;
    proposedFundingStreamId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    howDidYouFindUsId: string | null | null;
    isCTUAlreadyInvolvedId: string | null | null;
    teamContactRoleId: string | null | null;
    leadApplicantOrganisationTypeId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    leadApplicantAgeRangeId: string | null | null;
    leadApplicantGenderId: string | null | null;
    leadApplicantEthnicityId: string | null | null;
}

export interface UpdateProject {
    id: string;
    statusId: string;
    submittedFundingStreamId: string | null | null;
    submittedFundingStreamFreeText: string;
    submittedFundingStreamName: string;
    leadAdviserUserId: string | null | null;
    projectStartDate: string | null | null;
    recruitmentTarget: number | null | null;
    numberOfProjectSites: number | null | null;
    isInternationalMultiSiteStudyId: string | null | null;
}

export interface UpdateProjectStatus {
    id: string;
    statusId: string;
}

export interface QueryProject extends Query {
    number: string;
    title: string;
    fromDate: string | null | null;
    toDate: string | null | null;
    statusId: string | null | null;
    leadAdviserUserId: string | null | null;
    proposedFundingStreamId: string | null | null;
    teamContactName: string;
    requestedSupportTeamId: string | null | null;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface ReportStep {
    taskId: string;
    stage: string;
    stageCount: number;
    stageTotal: number;
    complete: boolean;
    metrics: ReportBuilderMetrics;
}

export interface ReportBuilderMetrics {
    loadSteps: number;
    loadItems: number;
    loadAvgMicroseconds: number;
    loadAvgItems: number;
    loadMicroseconds: number;
    loadMinMicroseconds: number;
    loadMaxMicroseconds: number;
    renderSteps: number;
    renderItems: number;
    renderMicroseconds: number;
    renderAvgMicroseconds: number;
    renderAvgItems: number;
    renderMinMicroseconds: number;
    renderMaxMicroseconds: number;
    generationSteps: number;
    generationItems: number;
    generationMicroseconds: number;
    generationAvgMicroseconds: number;
    generationAvgItems: number;
    generationMinMicroseconds: number;
    generationMaxMicroseconds: number;
}

export interface ProjectChangeLog {
    id: string;
    message: string;
    createdAt: string;
    projectId: string;
    createdByUserId: string | null | null;
}

export interface QueryProjectChangeLog extends Query {
    projectId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface ProjectSubmission {
    id: string;
    nihrReference: string;
    submissionDate: string;
    fundingAmountOnSubmission: number;
    projectId: string;
    submissionStageId: string;
    submissionOutcomeId: string | null | null;
}

export interface CreateProjectSubmission {
    projectId: string;
    submissionDate: string;
    nihrReference: string;
    submissionStageId: string;
    submissionOutcomeId: string | null | null;
    fundingAmountOnSubmission: number;
}

export interface UpdateProjectSubmission {
    id: string;
    submissionDate: string;
    nihrReference: string;
    submissionStageId: string;
    submissionOutcomeId: string | null | null;
    fundingAmountOnSubmission: number;
}

export interface QueryProjectSubmission extends Query {
    projectId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface ProjectSupport {
    id: string;
    date: string;
    description: string;
    workTimeInHours: number;
    teamIds: Array<string>;
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    projectId: string;
}

export interface CreateProjectSupport {
    projectId: string;
    date: string | null | null;
    teamIds: Array<string>;
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    description: string;
    workTimeInHours: number;
}

export interface UpdateProjectSupport {
    id: string;
    date: string;
    teamIds: Array<string>;
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    description: string;
    workTimeInHours: number;
}

export interface QueryProjectSupport extends Query {
    projectId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface ProjectTeamSupport {
    id: string;
    projectLabel: string;
    createdNotes: string;
    createdAt: string;
    completedNotes: string;
    completedAt: string | null | null;
    projectId: string;
    supportTeamId: string;
    createdByUserId: string;
    completedByUserId: string | null | null;
}

export interface CreateProjectTeamSupport {
    projectId: string;
    supportTeamId: string;
    createdNotes: string;
}

export interface UpdateProjectTeamSupport {
    id: string;
    createdNotes: string;
    completedNotes: string;
}

export interface CompleteProjectTeamSupport {
    id: string;
    completedNotes: string;
}

export interface QueryProjectTeamSupport extends Query {
    projectId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface Report {
    id: string;
    name: string;
    description: string;
    design: ReportDesign;
    isPublic: boolean;
    createdAt: string;
    reportProviderId: string;
    ownerUserId: string;
    reportGroupId: string;
}

export interface CreateReport {
    name: string;
    reportGroupId: string;
    reportProviderId: string;
}

export interface CopyReport {
    id: string;
    name: string;
}

export interface UpdateReport {
    id: string;
    name: string;
    description: string;
    isPublic: boolean;
    reportGroupId: string;
    design: ReportDesign;
}

export interface RunReport {
    reportId: string;
    arguments: Array<ReportArgument> | null;
}

export interface ReportArgument {
    name: string;
    filterId: string;
    fieldId: string;
    reportProviderId: string;
    fieldType: ReportFieldType;
    filterType: ReportFilterType;
    condition: number;
    conditions: Array<ReportFilterCondition>;
    guidsValue: Array<string> | null;
    doubleValue: number | null | null;
    stringValue: string | null;
    dateValue: string | null | null;
}

export interface QueryReport extends Query {
    name: string;
    reportGroupId: string | null | null;
    ownReportsOnly: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface InsertReportColumn {
    reportProviderId: string;
    design: ReportDesign;
    fieldIds: Array<string>;
}

export interface UpdateReportColumn {
    reportProviderId: string;
    design: ReportDesign;
    id: string;
    title: string;
    width: number | null | null;
    bold: boolean;
}

export interface DeleteReportColumn {
    reportProviderId: string;
    design: ReportDesign;
    id: string;
}

export interface InsertReportFilter {
    reportProviderId: string;
    design: ReportDesign;
    fieldId: string;
    parentId: string | null | null;
}

export interface UpdateReportFilter {
    reportProviderId: string;
    design: ReportDesign;
    filter: ReportFilter;
}

export interface DeleteReportFilter {
    reportProviderId: string;
    design: ReportDesign;
    id: string;
}

export interface ReportSchema {
    reportProviderId: string;
    fields: Array<ReportField>;
}

export interface ReportMapEntity {
    id: string;
    name: string;
}

export interface LookupReferenceField {
    reportProviderId: string;
    fieldId: string;
    query: Query;
}

export interface ReportGroup {
    id: string;
    name: string;
    sortOrder: number;
}

export interface CreateReportGroup {
    name: string;
}

export interface UpdateReportGroup {
    id: string;
    name: string;
}

export interface SortReportGroup {
    ids: Array<string>;
}

export interface QueryReportGroup extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface SupportRequest {
    id: string;
    number: number;
    prefixedNumber: string;
    dateOfRequest: string;
    title: string;
    applicationStageFreeText: string;
    proposedFundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    howDidYouFindUsFreeText: string;
    whoElseIsOnTheStudyTeam: string;
    isCTUAlreadyInvolvedFreeText: string;
    teamContactTitle: string;
    teamContactFirstName: string;
    teamContactLastName: string;
    teamContactEmail: string;
    teamContactRoleFreeText: string;
    teamContactMailingPermission: boolean;
    teamContactPrivacyStatementRead: boolean;
    leadApplicantTitle: string;
    leadApplicantFirstName: string;
    leadApplicantLastName: string;
    leadApplicantEmail: string;
    leadApplicantJobRole: string;
    leadApplicantOrganisation: string;
    leadApplicantDepartment: string;
    leadApplicantAddressLine1: string;
    leadApplicantAddressLine2: string;
    leadApplicantAddressTown: string;
    leadApplicantAddressCounty: string;
    leadApplicantAddressCountry: string;
    leadApplicantAddressPostcode: string;
    leadApplicantORCID: string;
    projectId: string | null | null;
    createdAt: string;
    fileStorageGroupId: string;
    statusId: string;
    isFellowshipId: string | null | null;
    applicationStageId: string | null | null;
    proposedFundingCallTypeId: string | null | null;
    proposedFundingStreamId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    howDidYouFindUsId: string | null | null;
    isCTUAlreadyInvolvedId: string | null | null;
    teamContactRoleId: string | null | null;
    leadApplicantOrganisationTypeId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    leadApplicantAgeRangeId: string | null | null;
    leadApplicantGenderId: string | null | null;
    leadApplicantEthnicityId: string | null | null;
}

export interface CreateSupportRequest {
    fileStorageGroupId: string | null | null;
    title: string;
    proposedFundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    isFellowshipId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    applicationStageId: string | null | null;
    applicationStageFreeText: string;
    proposedFundingStreamId: string | null | null;
    proposedFundingCallTypeId: string | null | null;
    howDidYouFindUsId: string | null | null;
    howDidYouFindUsFreeText: string;
    whoElseIsOnTheStudyTeam: string;
    isCTUAlreadyInvolvedId: string | null | null;
    isCTUAlreadyInvolvedFreeText: string;
    teamContactTitle: string;
    teamContactFirstName: string;
    teamContactLastName: string;
    teamContactEmail: string;
    teamContactRoleId: string | null | null;
    teamContactRoleFreeText: string;
    teamContactMailingPermission: boolean;
    teamContactPrivacyStatementRead: boolean;
    leadApplicantTitle: string;
    leadApplicantFirstName: string;
    leadApplicantLastName: string;
    leadApplicantEmail: string;
    leadApplicantJobRole: string;
    leadApplicantOrganisationTypeId: string | null | null;
    leadApplicantOrganisation: string;
    leadApplicantDepartment: string;
    leadApplicantAddressLine1: string;
    leadApplicantAddressLine2: string;
    leadApplicantAddressTown: string;
    leadApplicantAddressCounty: string;
    leadApplicantAddressCountry: string;
    leadApplicantAddressPostcode: string;
    leadApplicantORCID: string;
    isLeadApplicantNHSId: string | null | null;
    leadApplicantAgeRangeId: string | null | null;
    leadApplicantGenderId: string | null | null;
    leadApplicantEthnicityId: string | null | null;
}

export interface UpdateSupportRequest {
    id: string;
    title: string;
    proposedFundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    isFellowshipId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    applicationStageId: string | null | null;
    applicationStageFreeText: string;
    proposedFundingStreamId: string | null | null;
    proposedFundingCallTypeId: string | null | null;
    howDidYouFindUsId: string | null | null;
    howDidYouFindUsFreeText: string;
    whoElseIsOnTheStudyTeam: string;
    isCTUAlreadyInvolvedId: string | null | null;
    isCTUAlreadyInvolvedFreeText: string;
}

export interface UpdateSupportRequestResearcher {
    id: string;
    teamContactTitle: string;
    teamContactFirstName: string;
    teamContactLastName: string;
    teamContactEmail: string;
    teamContactRoleId: string | null | null;
    teamContactRoleFreeText: string;
    teamContactMailingPermission: boolean;
    teamContactPrivacyStatementRead: boolean;
    leadApplicantTitle: string;
    leadApplicantFirstName: string;
    leadApplicantLastName: string;
    leadApplicantEmail: string;
    leadApplicantJobRole: string;
    leadApplicantOrganisationTypeId: string | null | null;
    leadApplicantOrganisation: string;
    leadApplicantDepartment: string;
    leadApplicantAddressLine1: string;
    leadApplicantAddressLine2: string;
    leadApplicantAddressTown: string;
    leadApplicantAddressCounty: string;
    leadApplicantAddressCountry: string;
    leadApplicantAddressPostcode: string;
    leadApplicantORCID: string;
    isLeadApplicantNHSId: string | null | null;
    leadApplicantAgeRangeId: string | null | null;
    leadApplicantGenderId: string | null | null;
    leadApplicantEthnicityId: string | null | null;
}

export interface UpdateSupportRequestStatus {
    id: string;
    statusId: string;
    supportProvidedIds: Array<string>;
    description: string;
    workTimeInHours: number | null | null;
}

export interface QuerySupportRequest extends Query {
    number: string;
    title: string;
    fromDate: string | null | null;
    toDate: string | null | null;
    statusId: string | null | null;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface SupportRequestChangeLog {
    id: string;
    message: string;
    createdAt: string;
    supportRequestId: string;
    createdByUserId: string | null | null;
}

export interface QuerySupportRequestChangeLog extends Query {
    supportRequestId: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface InsertSupportTeamUser {
    supportTeamId: string;
    userId: string;
}

export interface DeleteSupportTeamUser {
    supportTeamId: string;
    userId: string;
}

export interface SupportTeamUser {
    id: string;
    userId: string;
    supportTeamId: string;
}

export interface QuerySupportTeamUser extends Query {
    userId: string | null | null;
    supportTeamId: string | null | null;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface UserActivity {
    isProjectActivity: boolean;
    id: string;
    date: string;
    description: string;
    workTimeInHours: number;
    projectPrefixedNumber: string;
    projectSupportProvidedIds: Array<string>;
    userId: string;
    workActivityId: string | null | null;
    projectId: string | null | null;
    projectSupportId: string | null | null;
}

export interface CreateUserActivity {
    date: string;
    workActivityId: string;
    workTimeInHours: number;
    description: string;
}

export interface UpdateUserActivity {
    id: string;
    date: string;
    workActivityId: string;
    workTimeInHours: number;
    description: string;
}

export interface QueryUserActivity extends Query {
    userId: string | null | null;
    fromDate: string | null | null;
    toDate: string | null | null;
    description: string;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface User {
    id: string;
    email: string;
    developer: boolean;
    title: string;
    firstName: string;
    lastName: string;
    fullName: string;
    rssJobTitle: string;
    universityJobTitle: string;
    professionalBackground: string;
    specialisation: string;
    disciplineIds: Array<string>;
    disciplineFreeText: string;
    startDateForRSS: string | null | null;
    endDateForRSS: string | null | null;
    fullTimeEquivalentForRSS: number | null | null;
    hidden: boolean;
    disabled: boolean;
    lastLoginAt: string | null | null;
    createdAt: string;
    siteId: string | null | null;
    userGroupId: string;
}

export interface CreateUser {
    email: string;
    title: string;
    firstName: string;
    lastName: string;
    userGroupId: string;
}

export interface UpdateUser {
    id: string;
    email: string;
    title: string;
    firstName: string;
    lastName: string;
    userGroupId: string;
    rssJobTitle: string;
    universityJobTitle: string;
    professionalBackground: string;
    specialisation: string;
    disciplineIds: Array<string>;
    disciplineFreeText: string;
    startDateForRSS: string | null | null;
    endDateForRSS: string | null | null;
    fullTimeEquivalentForRSS: number | null | null;
    siteId: string | null | null;
    disabled: boolean;
    hidden: boolean;
}

export interface QueryUser extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface UserGroup {
    id: string;
    name: string;
    claims: UserGroupClaims;
}

export interface UserGroupClaims {
    canEditUsers: boolean;
    canEditUserGroups: boolean;
    canEditStaticData: boolean;
    canEditReports: boolean;
    canUnlockProjects: boolean;
    canTriageSupportRequests: boolean;
}

export interface CreateUserGroup {
    name: string;
}

export interface UpdateUserGroup {
    id: string;
    name: string;
}

export interface UpdateUserGroupClaims {
    id: string;
    canEditUsers: boolean;
    canEditUserGroups: boolean;
    canEditStaticData: boolean;
    canEditReports: boolean;
    canUnlockProjects: boolean;
    canTriageSupportRequests: boolean;
}

export interface QueryUserGroup extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface StringResult {
    value: string;
}

export interface LoginUser {
    email: string;
    password: string;
}

export interface ChangeUserPassword {
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}

export interface UpdateUserPassword {
    userId: string;
    newPassword: string;
}

export interface SendUserPasswordResetEmail {
    email: string;
}

export interface ResetUserPassword {
    userId: string;
    passwordResetToken: string;
    newPassword: string;
    confirmNewPassword: string;
}

export interface UserLogin {
    id: string;
    userId: string;
    email: string;
    ipAddress: string;
    successful: boolean;
    reason: string;
    createdAt: string;
}

export interface QueryUserLogin extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateAgeRange {
    name: string;
    default: boolean;
}

export interface UpdateAgeRange {
    id: string;
    name: string;
    default: boolean;
}

export interface SortAgeRange {
    ids: Array<string>;
}

export interface QueryAgeRange extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateApplicationStage {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateApplicationStage {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortApplicationStage {
    ids: Array<string>;
}

export interface QueryApplicationStage extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateDisability {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateDisability {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortDisability {
    ids: Array<string>;
}

export interface QueryDisability extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateEthnicity {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateEthnicity {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortEthnicity {
    ids: Array<string>;
}

export interface QueryEthnicity extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateFundingBody {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateFundingBody {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortFundingBody {
    ids: Array<string>;
}

export interface QueryFundingBody extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateFundingCallType {
    name: string;
    default: boolean;
    hidden: boolean;
}

export interface UpdateFundingCallType {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
}

export interface SortFundingCallType {
    ids: Array<string>;
}

export interface QueryFundingCallType extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateFundingStream {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateFundingStream {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortFundingStream {
    ids: Array<string>;
}

export interface QueryFundingStream extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateGender {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateGender {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortGender {
    ids: Array<string>;
}

export interface QueryGender extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateHowDidYouFindUs {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateHowDidYouFindUs {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortHowDidYouFindUs {
    ids: Array<string>;
}

export interface QueryHowDidYouFindUs extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsCTUAlreadyInvolved extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsCTUTeamContribution extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsFellowship extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsInternationalMultiSiteStudy extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsLeadApplicantNHS extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsPPIEAndEDIContribution extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsQuantativeTeamContribution extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsResubmission extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryIsTeamMembersConsulted extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryProjectStatus extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryReportProvider extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateResearcherOrganisationType {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateResearcherOrganisationType {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortResearcherOrganisationType {
    ids: Array<string>;
}

export interface QueryResearcherOrganisationType extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QueryResearcherRole extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateResearchMethodology {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateResearchMethodology {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortResearchMethodology {
    ids: Array<string>;
}

export interface QueryResearchMethodology extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateSite {
    name: string;
    default: boolean;
}

export interface UpdateSite {
    id: string;
    name: string;
    default: boolean;
}

export interface SortSite {
    ids: Array<string>;
}

export interface QuerySite extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QuerySubmissionOutcome extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QuerySubmissionStage extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateSupportProvided {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateSupportProvided {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortSupportProvided {
    ids: Array<string>;
}

export interface QuerySupportProvided extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QuerySupportRequestStatus extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface QuerySupportTeam extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateTitle {
    name: string;
    default: boolean;
}

export interface UpdateTitle {
    id: string;
    name: string;
    default: boolean;
}

export interface SortTitle {
    ids: Array<string>;
}

export interface QueryTitle extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateUserDiscipline {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateUserDiscipline {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortUserDiscipline {
    ids: Array<string>;
}

export interface QueryUserDiscipline extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface CreateWorkActivity {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateWorkActivity {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortWorkActivity {
    ids: Array<string>;
}

export interface QueryWorkActivity extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}



export enum ReportFieldType {
    Unspecified = 0,
    String = 10,
    Numeric = 20,
    Boolean = 30,
    DateTime = 40,
    Date = 50,
    Reference = 1000,
    ReferenceList = 1010,
}

export interface ClientConfiguration {
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
    title: string;
    fieldId: string;
}

export interface ReportDesign {
    columns: Array<ReportColumn>;
}

export interface DashboardModel {
    openTeamSupport: Array<ProjectTeamSupport>;
}

export interface ProjectTeamSupport {
    id: string;
    projectPrefixedNumber: string;
    createdNotes: string;
    createdAt: string;
    completedNotes: string;
    completedAt: string | null | null;
    projectId: string;
    supportTeamId: string;
    createdByUserId: string;
    completedByUserId: string | null | null;
}

export interface QueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
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

export interface ApplicationStage extends IStaticData, IStaticDataWithFreeText {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface IStaticDataWithFreeText {
    id: string;
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

export interface FundingBody extends IStaticData, IStaticDataWithFreeText {
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

export interface FundingStream extends IStaticData, IStaticDataWithFreeText {
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

export interface ResearcherOrganisationType extends IStaticData, IStaticDataWithFreeText {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface ResearcherRole extends IStaticData, IStaticDataWithFreeText {
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
    createdAt: string;
    fileStorageGroupId: string;
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
    fundingStreamId: string | null | null;
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
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

export interface ReportProgress {
    taskId: string;
    progressPercentage: number;
    complete: boolean;
}

export interface ProjectExportRequest {
    number: string;
    title: string;
    fromDate: string | null | null;
    toDate: string | null | null;
    statusId: string | null | null;
    fundingStreamId: string | null | null;
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
}

export interface UpdateProjectSupport {
    id: string;
    date: string;
    teamIds: Array<string>;
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    description: string;
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
    design: ReportDesign;
    sortOrder: number;
    ownerUserId: string;
    reportGroupId: string;
    reportProviderId: string;
}

export interface CreateReport {
    name: string;
    reportGroupId: string;
    reportProviderId: string;
}

export interface UpdateReport {
    id: string;
    name: string;
    design: ReportDesign;
}

export interface QueryReport extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface IReportSchema {
    fields: Array<IReportField>;
}

export interface IReportField {
    id: string;
    name: string;
    fieldType: ReportFieldType;
    referenceType: string;
    nullable: boolean;
    default: boolean;
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
    workTimeInHours: number | null | null;
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
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface User {
    fullName: string;
    id: string;
    email: string;
    developer: boolean;
    title: string;
    firstName: string;
    lastName: string;
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

export interface CreateResearcherRole {
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface UpdateResearcherRole {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface SortResearcherRole {
    ids: Array<string>;
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



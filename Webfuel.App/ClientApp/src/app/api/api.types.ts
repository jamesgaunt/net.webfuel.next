export interface ClientConfiguration {
    email: string;
    sideMenu: ClientConfigurationMenu;
    settingsMenu: ClientConfigurationMenu;
    staticDataMenu: ClientConfigurationMenu;
}

export interface ClientConfigurationMenu {
    icon: string;
    name: string;
    action: string;
    children: Array<ClientConfigurationMenu> | null;
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

export interface IStaticDataModel {
    applicationStage: Array<ApplicationStage>;
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
    researchMethodology: Array<ResearchMethodology>;
    site: Array<Site>;
    submissionOutcome: Array<SubmissionOutcome>;
    submissionStage: Array<SubmissionStage>;
    supportProvided: Array<SupportProvided>;
    supportRequestStatus: Array<SupportRequestStatus>;
    title: Array<Title>;
    userDiscipline: Array<UserDiscipline>;
    workActivity: Array<WorkActivity>;
    loadedAt: string;
}

export interface ApplicationStage extends IStaticData, IStaticDataWithFreeText {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
}

export interface IStaticData {
    id: string;
    name: string;
    sortOrder: number;
    default: boolean;
}

export interface IStaticDataWithFreeText {
    id: string;
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
    closureDate: string | null | null;
    nihrrssMemberCollaboratorIds: Array<string>;
    submittedFundingStreamFreeText: string;
    submittedFundingStreamName: string;
    dateOfRequest: string;
    title: string;
    briefDescription: string;
    fundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    supportRequested: string;
    projectStartDate: string | null | null;
    recruitmentTarget: number | null | null;
    numberOfProjectSites: number | null | null;
    createdAt: string;
    statusId: string;
    isQuantativeTeamContributionId: string | null | null;
    isCTUTeamContributionId: string | null | null;
    isPPIEAndEDIContributionId: string | null | null;
    submittedFundingStreamId: string | null | null;
    supportRequestId: string | null | null;
    applicationStageId: string | null | null;
    fundingStreamId: string | null | null;
    fundingCallTypeId: string | null | null;
    isFellowshipId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    howDidYouFindUsId: string | null | null;
    isInternationalMultiSiteStudyId: string | null | null;
}

export interface CreateProject {
    title: string;
}

export interface UpdateProject {
    id: string;
    isQuantativeTeamContributionId: string | null | null;
    isCTUTeamContributionId: string | null | null;
    isPPIEAndEDIContributionId: string | null | null;
    submittedFundingStreamId: string | null | null;
    submittedFundingStreamFreeText: string;
    submittedFundingStreamName: string;
    projectStartDate: string | null | null;
    recruitmentTarget: number | null | null;
    numberOfProjectSites: number | null | null;
    isInternationalMultiSiteStudyId: string | null | null;
}

export interface QueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
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
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    description: string;
    projectId: string;
}

export interface CreateProjectSupport {
    projectId: string;
    date: string | null | null;
    adviserIds: Array<string>;
    supportProvidedIds: Array<string>;
    description: string;
}

export interface UpdateProjectSupport {
    id: string;
    date: string;
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

export interface Researcher {
    id: string;
    email: string;
    createdAt: string;
}

export interface CreateResearcher {
    email: string;
}

export interface UpdateResearcher {
    id: string;
    email: string;
}

export interface QueryResearcher extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface SupportRequest {
    id: string;
    title: string;
    dateOfRequest: string;
    fundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    createdAt: string;
    isFellowshipId: string | null | null;
    applicationStageId: string | null | null;
    fundingCallTypeId: string | null | null;
    fundingStreamId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    howDidYouFindUsId: string | null | null;
    statusId: string;
    projectId: string | null | null;
}

export interface CreateSupportRequest {
    title: string;
    fundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    isFellowshipId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    applicationStageId: string | null | null;
    fundingStreamId: string | null | null;
    fundingCallTypeId: string | null | null;
    howDidYouFindUsId: string | null | null;
}

export interface UpdateSupportRequest {
    id: string;
    title: string;
    fundingStreamName: string;
    targetSubmissionDate: string | null | null;
    experienceOfResearchAwards: string;
    briefDescription: string;
    supportRequested: string;
    isFellowshipId: string | null | null;
    isTeamMembersConsultedId: string | null | null;
    isResubmissionId: string | null | null;
    isLeadApplicantNHSId: string | null | null;
    applicationStageId: string | null | null;
    fundingStreamId: string | null | null;
    fundingCallTypeId: string | null | null;
    howDidYouFindUsId: string | null | null;
}

export interface TriageSupportRequest {
    id: string;
    statusId: string;
}

export interface QuerySupportRequest extends Query {
    skip: number;
    take: number;
    projection?: Array<string>;
    filters?: Array<QueryFilter>;
    sort?: Array<QuerySort>;
    search?: string;
}

export interface UserActivity {
    id: string;
    date: string;
    description: string;
    userId: string;
    workActivityId: string;
}

export interface CreateUserActivity {
    date: string;
    workActivityId: string;
    description: string;
}

export interface UpdateUserActivity {
    id: string;
    date: string;
    workActivityId: string;
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
}

export interface CreateUserGroup {
    name: string;
}

export interface UpdateUserGroup {
    id: string;
    name: string;
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

export interface SendUserPasswordResetEmail {
    email: string;
}

export interface ResetUserPassword {
    userId: string;
    passwordResetToken: string;
    newPassword: string;
    confirmNewPassword: string;
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



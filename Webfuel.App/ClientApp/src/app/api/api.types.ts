export interface ClientConfiguration {
    email: string;
    sideMenu: ClientConfigurationMenu;
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
    projectStatus: Array<ProjectStatus>;
    researchMethodology: Array<ResearchMethodology>;
    submissionStage: Array<SubmissionStage>;
    supportRequestStatus: Array<SupportRequestStatus>;
    title: Array<Title>;
    loadedAt: string;
}

export interface ApplicationStage extends IStaticData {
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
    freeText: boolean;
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

export interface SubmissionStage extends IStaticData {
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
    hidden: boolean;
}

export interface Project {
    id: string;
    linkId: string;
    title: string;
    fundingBodyId: string | null | null;
    researchMethodologyId: string | null | null;
}

export interface CreateProject {
    title: string;
}

export interface UpdateProject {
    id: string;
    title: string;
    fundingBodyId: string | null | null;
    researchMethodologyId: string | null | null;
}

export interface QueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface QueryProject extends Query {
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

export interface Researcher {
    id: string;
    email: string;
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

export interface User {
    id: string;
    email: string;
    developer: boolean;
    firstName: string;
    lastName: string;
    phone: string;
    hidden: boolean;
    disabled: boolean;
    lastLoginAt: string | null | null;
    createdAt: string;
    updatedAt: string;
    userGroupId: string;
}

export interface CreateUser {
    email: string;
    userGroupId: string;
}

export interface UpdateUser {
    id: string;
    email: string;
    userGroupId: string;
}

export interface QueryUser extends Query {
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
    freeText: boolean;
}

export interface UpdateFundingCallType {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
    freeText: boolean;
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

export interface QuerySubmissionStage extends Query {
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
    hidden: boolean;
}

export interface UpdateTitle {
    id: string;
    name: string;
    default: boolean;
    hidden: boolean;
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



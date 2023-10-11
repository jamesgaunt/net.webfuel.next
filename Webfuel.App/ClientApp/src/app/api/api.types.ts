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
    fundingBody: any;
    fundingStream: any;
    gender: any;
    researchMethodology: any;
    title: any;
    loadedAt: string;
}

export interface FundingBody extends IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    hint: string;
}

export interface IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    hidden: boolean;
    default: boolean;
    hint: string;
}

export interface FundingStream extends IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    hint: string;
}

export interface Gender extends IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    hint: string;
}

export interface ResearchMethodology extends IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    hint: string;
}

export interface Title extends IStaticData {
    id: string;
    name: string;
    code: string;
    sortOrder: number;
    default: boolean;
    hidden: boolean;
    hint: string;
}

export interface Project {
    id: string;
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

export interface User {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    developer: boolean;
    birthday: string;
    createdAt: string;
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

export interface CreateFundingBody {
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
}

export interface UpdateFundingBody {
    id: string;
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
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

export interface CreateFundingStream {
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
}

export interface UpdateFundingStream {
    id: string;
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
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
    code: string;
    hidden: boolean;
    default: boolean;
}

export interface UpdateGender {
    id: string;
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
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

export interface CreateResearchMethodology {
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
}

export interface UpdateResearchMethodology {
    id: string;
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
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

export interface CreateTitle {
    name: string;
    code: string;
    hidden: boolean;
    default: boolean;
}

export interface UpdateTitle {
    id: string;
    name: string;
    code: string;
    hidden: boolean;
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



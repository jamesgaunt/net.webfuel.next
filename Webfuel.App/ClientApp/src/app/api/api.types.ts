export enum ErrorType {
    UnknownError = 0,
    ValidationError = 1,
    NotAuthorizedError = 2,
    NotAuthenticatedError = 3,
    DatabaseError = 4,
}

export interface IError {
    errorType: ErrorType;
    message: string;
    validationErrors: Array<IValidationError>;
}

export interface IValidationError {
    property: string;
    message: string;
}

export interface IIdentityClaims {
    developer: boolean;
    canAccessUsers: boolean;
}

export interface IIdentityToken {
    user: IIdentityUser;
    claims: IIdentityClaims;
    validity: IIdentityValidity;
    signature: string;
}

export interface IIdentityUser {
    id: string;
    email: string;
}

export interface IIdentityValidity {
    validUntil: any;
    validFromIPAddress: string;
}

export interface ITenant {
    id: string;
    name: string;
    live: boolean;
}

export interface ICreateTenant {
    name: string;
}

export interface IUpdateTenant {
    id: string;
    name: string;
    live: boolean;
}

export interface IQueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface IQueryTenant extends IQuery {
    search: string;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface IQuerySort {
    field: string;
    direction: number;
}

export interface IQuery {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface ITenantDomain {
    id: string;
    domain: string;
    redirectTo: string;
    tenantId: string;
}

export interface ICreateTenantDomain {
    tenantId: string;
    domain: string;
    redirectTo: string;
}

export interface IUpdateTenantDomain {
    id: string;
    domain: string;
    redirectTo: string;
}

export interface IQueryTenantDomain extends IQuery {
    tenantId: string;
    search: string;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface IUser {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    developer: boolean;
}

export interface ICreateUser {
    email: string;
}

export interface IUpdateUser {
    id: string;
    email: string;
}

export interface IUserListView {
    id: string;
    email: string;
}

export interface IQueryUserListView extends IQuery {
    search: string;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface ILoginUser {
    email: string;
    password: string;
}



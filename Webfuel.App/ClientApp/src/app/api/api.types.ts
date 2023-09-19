export interface INotAuthorizedError extends IError {
    errorType: string;
    message: string;
}

export interface IError {
    errorType: string;
    message: string;
}

export interface IValidationError extends IError {
    errors: Array<IValidationErrorProperty>;
    errorType: string;
    message: string;
}

export interface IValidationErrorProperty {
    propertyName: string;
    errorMessage: string;
}

export interface IIdentity {
    id: string;
    email: string;
}

export interface IIdentityClaims {
    isDeveloper: boolean;
    canAccessUsers: boolean;
    someStringClaim: string;
}

export interface IIdentityToken {
    identity: IIdentity;
    claims: IIdentityClaims;
    validity: IIdentityValidity;
    signature: string;
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



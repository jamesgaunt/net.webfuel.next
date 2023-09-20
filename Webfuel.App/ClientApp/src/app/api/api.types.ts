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

export interface IQueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
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

export interface IQuerySort {
    field: string;
    direction: number;
}

export interface IQuery {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface ILoginUser {
    email: string;
    password: string;
}

export interface IUserGroup {
    id: string;
    name: string;
}

export interface ICreateUserGroup {
    name: string;
}

export interface IUpdateUserGroup {
    id: string;
    name: string;
}

export interface IUserGroupListView {
    id: string;
    name: string;
}

export interface IQueryUserGroupListView extends IQuery {
    search: string;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}



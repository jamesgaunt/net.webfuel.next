export enum ErrorResonseType {
    UnknownError = 0,
    ValidationError = 1,
    NotAuthorizedError = 2,
    NotAuthenticatedError = 3,
    DatabaseError = 4,
}

export interface ErrorResponse {
    errorType: ErrorResonseType;
    message: string;
    validationErrors: Array<ValidationError>;
}

export interface ValidationError {
    property: string;
    message: string;
}

export interface IdentityClaims {
    developer: boolean;
    canAccessUsers: boolean;
}

export interface IdentityToken {
    user: IdentityUser;
    claims: IdentityClaims;
    validity: IdentityValidity;
    signature: string;
}

export interface IdentityUser {
    id: string;
    email: string;
}

export interface IdentityValidity {
    validUntil: string;
    validFromIPAddress: string;
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

export interface QueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface QueryUser extends Query {
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



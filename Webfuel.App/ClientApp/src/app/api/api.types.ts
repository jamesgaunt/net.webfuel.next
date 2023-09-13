export interface IEventLog {
    id: string;
    level: number;
    message: string;
    source: string;
    detail: string;
    context: string;
    entityId: string | null | null;
    tenantId: string | null | null;
    identityId: string;
    ipAddress: string;
    exception: string;
    requestUrl: string;
    requestMethod: string;
    requestHeaders: string;
}

export interface IQueryResult<T> {
    items: Array<T>;
    totalCount: number;
}

export interface IQuery {
    projection: Array<string>;
    search: string | null;
    where: string | null;
    filters: Array<IQueryFilter>;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface IQueryFilter {
    field: string;
    op: string;
    value: any | null;
    filters: Array<IQueryFilter> | null;
}

export interface IQuerySort {
    field: string;
    direction: number;
}



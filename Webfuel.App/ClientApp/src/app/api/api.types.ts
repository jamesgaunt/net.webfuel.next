export interface IWidget {
    id: string;
    name: string;
    age: number;
}

export interface IRepositoryQueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface IRepositoryQuery {
    projection: Array<string>;
    filters: Array<IRepositoryQueryFilter>;
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}

export interface IRepositoryQueryFilter {
    field: string;
    op: string;
    value: any | null;
    filters: Array<IRepositoryQueryFilter> | null;
}

export interface IQuerySort {
    field: string;
    direction: number;
}

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

export interface IQueryResult<TItem, TQuery> {
    items: Array<TItem>;
    totalCount: number;
    query: TQuery;
}

export interface IQuery {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
}



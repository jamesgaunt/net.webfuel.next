export interface IWidget {
    id: string;
    name: string;
    age: number;
    shippingDate: any;
}

export interface IQueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface ISimpleQuery extends IQuery<any> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: any | null;
}

export interface IQuerySort {
    field: string;
    direction: number;
}

export interface IQuery<TFilter> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: TFilter | null;
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

export interface ISearchFilter {
    search: string;
}

export interface ISearchQuery extends IQuery<ISearchFilter> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: ISearchFilter | null;
}



export interface IQuery<TFilter> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: TFilter | null;
}

export interface IQuerySort {
    field: string;
    direction: number;
}

export interface ISimpleQuery extends IQuery<any> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: any | null;
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

export interface IQueryResult<TItem> {
    items: Array<TItem>;
    totalCount: number;
}

export interface IQueryWidgetCommand extends ISearchQuery {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: ISearchFilter | null;
}

export interface IWidget {
    id: string;
    name: string;
    age: number;
    shippingDate: string;
    createdAt: string;
    updatedAt: string;
}

export interface ICreateWidgetCommand {
    name: string;
    age: number;
}

export interface IUpdateWidgetCommand {
    id: string;
    name: string;
    age: number;
}

export interface IDeleteWidgetCommand {
    id: string;
}



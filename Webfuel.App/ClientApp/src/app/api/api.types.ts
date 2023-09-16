export enum DayOfWeek {
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
}

export interface IValidationError extends IError {
    errors: Array<IValidationErrorProperty>;
    errorType: string;
}

export interface IValidationErrorProperty {
    propertyName: string;
    errorMessage: string;
}

export interface IError {
    errorType: string;
}

export interface IWidget {
    id: string;
    name: string;
    age: number;
    shippingDate: string;
    nullableInt: number | null | null;
    nullableString: string | null;
    dayOfWeek: DayOfWeek;
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

export interface IQuerySort {
    field: string;
    direction: number;
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

export interface IQuery<TFilter> {
    sort: Array<IQuerySort>;
    skip: number;
    take: number;
    filter: TFilter | null;
}



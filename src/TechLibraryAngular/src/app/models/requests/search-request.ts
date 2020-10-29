import { CategorySearchResponse } from '../responses/category-search-response';

export class SearchRequest {

searchString: string;
categories: CategorySearchResponse[];
page: number;
recordCount: number;
pages: number;
recordsPerPage: number;
newSearch: boolean;
}

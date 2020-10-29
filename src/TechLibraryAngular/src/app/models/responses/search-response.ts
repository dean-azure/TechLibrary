import { BookSearchResponse } from './book-search-response';
import { CategorySearchResponse } from './category-search-response';

export class SearchResponse {
  searchString: string;
  page: number;
  pages: number;
  pageNumbers: number[];
  recordCount: number;
  recordsPerPage: number;
  categories: CategorySearchResponse[];
  books: BookSearchResponse[];

}

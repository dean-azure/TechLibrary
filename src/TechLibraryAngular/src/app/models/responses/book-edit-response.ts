import { Category } from './categories';

export class BookEditResponse {
  id: number;
  title: string;
  isbn: string;
  publishedDate: string;
  thumbnailUrl: string;
  shortDescr: string;
  longDescr: string;
  categories: Category[];

  readonly = true;
}

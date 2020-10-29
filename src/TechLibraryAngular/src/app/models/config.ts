
import { HttpHeaders } from '@angular/common/http';

export class Config {
  baseServiceURL: string;
  booksServiceURL: string;

  constructor(url: string) {
    console.log('Config constructor ' + url);
    this.baseServiceURL = url;
    this.booksServiceURL = url + 'books/';
  }

}

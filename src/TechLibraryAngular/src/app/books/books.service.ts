import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SearchResponse } from '../models/responses/search-response';
import { BookEditResponse } from '../models/responses/book-edit-response';
import { environment } from '../../environments/environment';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { HttpHeaders } from '@angular/common/http';
import { SearchRequest } from '../models/requests/search-request';
import { BookSearchResponse } from '../models/responses/book-search-response';

@Injectable({
  providedIn: 'root'
})
export class BooksService {



  constructor(private http: HttpClient)  {
  }

  getBooks(): Observable<SearchResponse> {

    console.log('getBooks ' + environment.config.booksServiceURL);

    return this.http
      .get<SearchResponse>(environment.config.booksServiceURL);

  }

  getBook(id: string): Observable<BookEditResponse> {
    const url = environment.config.booksServiceURL + id;
    console.log('getBook ' + url);

    return this.http.get<BookEditResponse>(url);

  }

  putBooks(searchRequest: SearchRequest): Observable<SearchResponse> {
    const url = environment.config.booksServiceURL;
    console.log('put search ' + url);

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    return this.http.put<SearchResponse>(url, searchRequest, httpOptions);


  }


}

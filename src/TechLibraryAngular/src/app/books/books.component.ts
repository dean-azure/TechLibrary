import { Component, OnInit } from '@angular/core';
import { SearchRequest } from '../models/requests/search-request';
import { SearchResponse } from '../models/responses/search-response';
import { BooksService } from './books.service';

@Component({
  selector: 'techlib-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  constructor(
    private booksService: BooksService
  ) { }

  rootUrl: string;
  searchResponse: SearchResponse;
  errorMessage: string;

  ngOnInit() : void {
    console.log('BooksComponent ngOnInit');

    this.booksService.getBooks().subscribe(
      result => {this.searchResponse = result;
      },
      error => this.errorMessage = error.toString()
    );

  }

  navigate(pageNumber: number): void {
    if(pageNumber === this.searchResponse.page) {
      return;
    }
    const searchRequst = new SearchRequest();
    searchRequst.categories = this.searchResponse.categories;
    searchRequst.page = pageNumber;
    searchRequst.pages = this.searchResponse.pages;
    searchRequst.recordCount = this.searchResponse.recordCount;
    searchRequst.recordsPerPage = this.searchResponse.recordsPerPage;
    searchRequst.searchString = this.searchResponse.searchString;
    searchRequst.newSearch = false;

    this.booksService.putBooks(searchRequst).subscribe(
      result => {this.searchResponse = result;
      },
      error => this.errorMessage = error.toString()
    );

  }


}

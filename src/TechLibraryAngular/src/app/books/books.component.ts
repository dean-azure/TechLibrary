import { Component, OnInit } from '@angular/core';
import { SearchRequest } from '../models/requests/search-request';
import { CategorySearchResponse } from '../models/responses/category-search-response';
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

  categoryFilter(categoryId: number) {
    console.log('categoryFilter');
    //console.log(category);
    //category.selected = !category.selected;

    let cat = this.searchResponse.categories.filter(u => u.id === categoryId)[0];
    cat.selected = !cat.selected;
    console.log(cat);
    //cat = this.searchResponse.categories.filter(u => u.id === 2)[0];
    //console.log(cat);
    console.log(this.searchResponse.categories.filter(u => u.selected === true));

    const searchRequst = new SearchRequest();
    searchRequst.categories = this.searchResponse.categories.filter(u => u.selected === true);
    searchRequst.page = this.searchResponse.page;
    searchRequst.pages = this.searchResponse.pages;
    searchRequst.recordCount = this.searchResponse.recordCount;
    searchRequst.recordsPerPage = this.searchResponse.recordsPerPage;
    searchRequst.searchString = this.searchResponse.searchString;
    searchRequst.newSearch = true;

    console.log(searchRequst.categories);
    cat = searchRequst.categories.filter(u => u.id === 1)[0];
    //console.log(cat);

    this.booksService.putBooks(searchRequst).subscribe(
      result => {this.searchResponse = result;
      },
      error => this.errorMessage = error.toString()
    );

  }

  navigate(pageNumber: number): void {
    console.log('navigate');
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

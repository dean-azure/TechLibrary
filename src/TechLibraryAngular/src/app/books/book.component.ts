import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookEditResponse } from '../models/responses/book-edit-response';
import { BooksService } from './books.service';

@Component({
  selector: 'techlib-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  constructor(
    private booksService: BooksService,
    private route: ActivatedRoute
    ) { }

  bookEditResponse: BookEditResponse;
  errorMessage: string;

  ngOnInit(): void {
    const paramid = this.route.snapshot.paramMap.get('id').toString();

    console.log('book component ngOnInit');

    this.booksService.getBook(paramid).subscribe(
      result => {this.bookEditResponse = result;
      },
      error => this.errorMessage = error.toString()
    );

  }

}

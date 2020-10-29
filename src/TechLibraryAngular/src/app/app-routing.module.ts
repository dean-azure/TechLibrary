import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BooksComponent } from './books/books.component';
import { BookComponent } from './books/book.component';
import { CategoriesComponent } from './books/categories.component';

const routes: Routes = [
  { path: 'books', component: BooksComponent },
  { path: 'books/:id', component: BookComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: '',  redirectTo: '/books', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

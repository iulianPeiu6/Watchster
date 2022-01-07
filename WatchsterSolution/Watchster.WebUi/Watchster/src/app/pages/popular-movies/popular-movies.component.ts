import { Component, NgModule, OnInit, ViewChild } from '@angular/core';
import 'devextreme/data/odata/store';
import { AuthService, Movie, MovieService } from '../../shared/services';
import ArrayStore from 'devextreme/data/array_store';
import { DxButtonModule, DxDataGridModule, DxFormModule, DxLoadIndicatorModule, DxLoadPanelModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  templateUrl: 'popular-movies.component.html'
})

export class PopularMoviesComponent implements OnInit {
  dataSource: ArrayStore;
  movies: Movie[];
  loadingVisible = false;

  constructor(private movieService: MovieService) {
    this.loadingVisible = true;
    this.dataSource =  new ArrayStore({
      key: ["id"],
      data: new Array<Movie>()
    });
    this.movies = new Array<Movie>();
  }
  async ngOnInit() {
    this.loadingVisible = true;
    this.movies = await this.movieService.getMostPopularMovies();
    console.log(this.movies);

    this.dataSource =  new ArrayStore({
      key: ["id"],
      data: this.movies
    });
    this.loadingVisible = false;
  }
}

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    DxLoadIndicatorModule,
    DxLoadPanelModule, 
    DxDataGridModule,
    CommonModule,
    RouterModule,
    DxFormModule,
    DxButtonModule,
    DxLoadIndicatorModule],
  exports: [PopularMoviesComponent],
  declarations: [PopularMoviesComponent],
  providers: [],
})
export class PopularMoviesModule { }
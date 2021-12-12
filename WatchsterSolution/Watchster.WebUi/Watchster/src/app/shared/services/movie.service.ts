import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export class Movie {
  constructor(
    public id: string,
    public tmDbId: number, 
    public title: string, 
    public releaseDate: Date, 
    public genres: string[],
    public averageRating: number,
    public overview: string) { 
  }
}

export class GetMoviesResponse {
  constructor(
    public totalPages: number,
    public movies: Movie[]){
  }
}

@Injectable()
export class MovieService {
  constructor(private http: HttpClient) { }
  
  async getMovies() {
    const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: 1 } })
        .toPromise()

    let allMovies = response.movies;

    for (let indexPage = 2; indexPage <= response.totalPages; indexPage++) {
      const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: indexPage } })
        .toPromise();

      allMovies = allMovies.concat(response.movies);
    }

    return allMovies;
  }
}
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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

export class MovieWrapper {
  constructor(public movie: Movie, public errorMessage: string){
  }
}

export class GetMoviesResponse {
  constructor(
    public totalPages: number,
    public movies: Movie[]){
  }
}

export class AddRatingResponse {
  constructor(public message: string) {}
}

@Injectable()
export class MovieService {
  constructor(private http: HttpClient) { }
  
  async getMovies() {
    const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: 1 } })
        .toPromise()

    let allMovies = response.movies;

    // for (let indexPage = 2; indexPage <= response.totalPages; indexPage++) {
    //   const response = await this.http
    //     .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: indexPage } })
    //     .toPromise();

    //   allMovies = allMovies.concat(response.movies);
    // }

    return allMovies;
  }

  async getMovie(id: string) {
    const response = await this.http
    .get<MovieWrapper>('/api/1/Movie/GetMovie', { params: { id: id } })
    .toPromise()
    return response;
  }

  async addRating(userId: string, idMovie: string, rating: number) {
    var response = await this.http
    .post<AddRatingResponse>('/api/1/Movie/AddRating', { userId: userId, movieId: idMovie, rating: rating  })
    .toPromise().then( function (response) {
      return new AddRatingResponse("Rating Added");
    }).catch((error: HttpErrorResponse) => {
      return new AddRatingResponse(error.error.message);
    });
    return response;
  }
}
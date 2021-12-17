import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

export class Movie {
  constructor(
    public id: string,
    public tMDbId: number, 
    public title: string, 
    public releaseDate: Date, 
    public genres: string[],
    public averageRating: number,
    public posterUrl: string,
    public popularity: number,
    public tmDbVoteAverage: number,
    public overview: string,
    public score: number) { 
  }
}

export class GetMovieResponse {
  constructor(public movie: Movie, public errorMessage: string){
  }
}

export class GetRecommendationsResponse {
  constructor(public recommendations: Movie[]){
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
  
  async getMovies(): Promise<Movie[]> {
    const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: 1 } })
        .toPromise();

    console.log(response);
    let allMovies = response.movies;

    for (let indexPage = 2; indexPage <= response.totalPages; indexPage++) {
      const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: indexPage } })
        .toPromise();

      console.log(response);
      allMovies = allMovies.concat(response.movies);
    }

    return allMovies;
  }

  async getMovieRecommendations(userId: string): Promise<Movie[]> {
    const response = await this.http
        .get<GetRecommendationsResponse>('/api/1/Movie/GetRecommendations', { params: { userId: userId } })
        .toPromise();

    console.log(response);
    return response.recommendations;
  }

  async getMovie(id: string) {
    const response = await this.http
      .get<GetMovieResponse>('/api/1/Movie/GetMovie', { params: { id: id } })
      .toPromise();

    console.log(response);
    return response.movie;
  }

  async addRating(userId: string, idMovie: string, rating: number) {
    var response = await this.http
    .post<AddRatingResponse>('/api/1/Movie/AddRating', { userId: userId, movieId: idMovie, rating: rating  })
    .toPromise().then( function (response) {
      return new AddRatingResponse("Rating Added");
    }).catch((error: HttpErrorResponse) => {
      return new AddRatingResponse(error.error.message);
    });
    console.log(response);
    return response;
  }
}
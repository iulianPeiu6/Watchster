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

export class GetRatingResponse {
  constructor(public rating: number) {}
}

@Injectable()
export class MovieService {
  tatalPages: number | undefined;
  lastPage: number = 2;
  constructor(private http: HttpClient) { }
  
  async getMovies(): Promise<Movie[]> {
    const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: 1 } })
        .toPromise();

    console.log(response);
    let movies = response.movies;
    this.tatalPages = response.totalPages;

    // for (let indexPage = 2; indexPage <= response.totalPages; indexPage++) {
    //   const response = await this.http
    //     .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: indexPage } })
    //     .toPromise();

    //   console.log(response);
    //   allMovies = allMovies.concat(response.movies);
    // }

    return movies;
  }

  async getNextMovies(): Promise<Movie[]> {
      if (this.lastPage > this.tatalPages!) {
        return new Array<Movie>()
      }

      const response = await this.http
        .get<GetMoviesResponse>('/api/1/Movie/GetFromPage', { params: { page: this.lastPage } })
        .toPromise();

      this.lastPage++;

      console.log(response);
      
      return response.movies;
  }

  async getMovieRecommendations(userId: string): Promise<Movie[]> {
    const response = await this.http
        .get<GetRecommendationsResponse>('/api/1/Movie/GetRecommendations', { params: { userId: userId } })
        .toPromise();

    console.log(response);
    return response.recommendations;
  }

  async getMostPopularMovies(): Promise<Movie[]> {
    const response = await this.http
        .get<Movie[]>('/api/1/Movie/GetMostPopular', { })
        .toPromise();

    console.log(response);
    return response;
  }

  async getNewMovies(): Promise<Movie[]> {
    const response = await this.http
        .get<Movie[]>('/api/1/Movie/GetLatestReleased', { })
        .toPromise();

    console.log(response);
    return response;
  }

  async getMovie(id: string) {
    const response = await this.http
      .get<GetMovieResponse>('/api/1/Movie/GetMovie', { params: { id: id } })
      .toPromise();

    console.log(response);
    return response.movie;
  }

  async getRating(userId: string, idMovie: string) {
    var response = await this.http
    .get<GetRatingResponse>('/api/1/Movie/GetRating', { params: { userId: userId, movieId: idMovie } })
    .toPromise();
    console.log(response);
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
    console.log(response);
    return response;
  }
}
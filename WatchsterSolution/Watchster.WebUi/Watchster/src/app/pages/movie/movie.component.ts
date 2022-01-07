import { Component, NgModule, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Movie, MovieService, GetMovieResponse } from "../../shared/services";
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import { AuthService, serverMessages } from "../../shared/services";
import notify from 'devextreme/ui/notify';
import { NgbRatingModule } from "@ng-bootstrap/ng-bootstrap";
import { RatingConfig, RatingModule } from 'ngx-bootstrap/rating';
import { DxButtonModule, DxResponsiveBoxModule } from "devextreme-angular";

@Component({
    selector: 'app-root',
    templateUrl: 'movie.component.html',
    styleUrls: ['./movie.component.scss']
})

export class MovieComponent implements OnInit {
    formData: any = {};
    loading = false;
    movieId: string;
    movie: Movie | undefined;
    loadingVisible = false;
    errorNotify: string = 'error';
    successNotify: string = 'success';
    colCountByScreen!: object;
    movieOverview!: string;
    moviePosterUrl!: string;
    userRating = 0;
    movieRatedByCurrentUser = false;

    constructor(private movieService: MovieService, private router: Router, private authService:  AuthService) {
        this.movieId = this.router.url.split('/')[2];
        this.colCountByScreen = {
            xs: 1,
            sm: 1,
            md: 2,
            lg: 2
          };
    }

    async onRateChange(rating :number) {        
        let userId = this.authService.userLogingDetails?.user.id!
        var response = await this.movieService.addRating(userId ,this.movieId, rating);
        switch(response.message) {
            case serverMessages.RatingNotInRange: {notify(serverMessages.RatingNotInRange, this.errorNotify, 2000); break;}
            case serverMessages.UserNotFound: {notify(serverMessages.UserNotFound, this.errorNotify, 2000); break;}
            case serverMessages.MovieNotFound: {notify(serverMessages.MovieNotFound, this.errorNotify, 2000); break;}
            case serverMessages.MovieAlreadyRated: {notify(serverMessages.MovieAlreadyRated, this.errorNotify, 2000); break;}
            default: { notify(response.message, this.successNotify, 2000); this.movieRatedByCurrentUser = true; }
        } 
    } 

    async ngOnInit() {
        let userId = this.authService.userLogingDetails?.user.id!
        this.loadingVisible = true;
        this.movie = await this.movieService.getMovie(this.movieId);
        this.userRating = await (await this.movieService.getRating(userId, this.movieId)).rating;
        this.loadingVisible = false;
        if (this.userRating != 0) {
            this.movieRatedByCurrentUser = true;
            console.log(this.movieRatedByCurrentUser);
        }
        this.movieOverview = this.movie.overview;
        this.moviePosterUrl = this.movie.posterUrl;
        console.log(this.movie);
    }
}

@NgModule({
    imports: [
        DxFormModule,
        DxLoadIndicatorModule,
        DxButtonModule,
        RatingModule,
        NgbRatingModule,
        DxResponsiveBoxModule
        ],
    exports: [MovieComponent],
    declarations: [MovieComponent],
    providers: [RatingConfig],
    
  })
  export class MovieModule { }
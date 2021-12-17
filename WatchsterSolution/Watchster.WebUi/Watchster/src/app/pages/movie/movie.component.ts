import { Component, NgModule, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Movie, MovieService, GetMovieResponse } from "../../shared/services";
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import { AuthService, serverMessages } from "../../shared/services";
import notify from 'devextreme/ui/notify';


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
    starRating : number = 0;
    errorNotify: string = 'error';
    successNotify: string = 'success';
    colCountByScreen!: object;
    movieOverview!: string;
    constructor(private movieService: MovieService, private router: Router, private authService:  AuthService) {
        this.movieId = this.router.url.split('/')[2];
        this.colCountByScreen = {
            xs: 1,
            sm: 1,
            md: 2,
            lg: 2
          };
    }
    

    addRating = async () => {
        var response = await this.movieService.addRating(this.authService._user == undefined ? "" : this.authService._user.user.id ,this.movieId, this.starRating);
        switch(response.message) {
            case serverMessages.RatingNotInRange: {notify(serverMessages.RatingNotInRange, this.errorNotify, 2000); break;}
            case serverMessages.UserNotFound: {notify(serverMessages.UserNotFound, this.errorNotify, 2000); break;}
            case serverMessages.MovieNotFound: {notify(serverMessages.MovieNotFound, this.errorNotify, 2000); break;}
            case serverMessages.MovieAlreadyRated: {notify(serverMessages.MovieAlreadyRated, this.errorNotify, 2000); break;}
            default: notify(response.message, this.successNotify, 2000);
        } 
    }

    async ngOnInit() {
        this.loadingVisible = true;
        this.movie = await this.movieService.getMovie(this.movieId);
        this.loadingVisible = false;
        this.movieOverview = this.movie.overview
        console.log(this.movie)
    }
}

@NgModule({
    imports: [
        DxFormModule,
        DxLoadIndicatorModule],
    exports: [MovieComponent],
    declarations: [MovieComponent],
    providers: [],
    
  })
  export class MovieModule { }
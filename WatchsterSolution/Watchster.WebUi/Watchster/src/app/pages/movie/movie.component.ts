import { Component, NgModule, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { MovieService } from "src/app/shared/services";

@Component({
    templateUrl: 'movie.component.html'
})
  
export class MovieComponent implements OnInit {
    movieId: string;
    constructor(private movieService: MovieService, private router: Router) {
        this.movieId = this.router.url.split('/')[2];
        console.log(this.movieId);
    }

    async ngOnInit() {
        // ToDo: Load Movie Details 
    }
}

@NgModule({
    imports: [],
    exports: [MovieComponent],
    declarations: [MovieComponent],
    providers: [],
  })
  export class MovieModule { }
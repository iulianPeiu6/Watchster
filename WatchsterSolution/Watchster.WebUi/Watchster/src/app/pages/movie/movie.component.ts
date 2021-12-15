import { Component, NgModule, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Movie, MovieService } from "src/app/shared/services";

@Component({
    templateUrl: 'movie.component.html'
})
  
export class MovieComponent implements OnInit {
    movieId: string | null;
    movie: Movie | undefined;
    loadingVisible = false;
    constructor(private movieService: MovieService, private router: Router, private activatedRoute: ActivatedRoute) {
        this.movieId = this.activatedRoute.snapshot.queryParamMap.get('id');
        console.log(this.movieId);

    }

    async ngOnInit() {
        /*this.loadingVisible = true;
        this.movie = await this.movieService.getMovie(id);
    
        this.dataSource =  new ArrayStore({
          key: ["tmDbId"],
          data: this.movies
        });
        this.loadingVisible = false;
        */
    }
}

@NgModule({
    imports: [],
    exports: [MovieComponent],
    declarations: [MovieComponent],
    providers: [],
  })
  export class MovieModule { }
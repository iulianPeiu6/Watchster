import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent, ResetPasswordFormComponent, CreateAccountFormComponent, ChangePasswordFormComponent } from './shared/components';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { MoviesComponent } from './pages/movies/movies.component';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { MovieComponent } from './pages/movie/movie.component';
import { MovieRecommendationsComponent } from './pages/movie-recommendations/movie-recommendations.component';
import { PopularMoviesComponent } from './pages/popular-movies/popular-movies.component';
import { NewMoviesComponent } from './pages/new-movies/new-movies.component';

const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'reset-password',
    component: ResetPasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'create-account',
    component: CreateAccountFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangePasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'movies',
    component: MoviesComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'movie/:id',
    component: MovieComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'recommendations',
    component: MovieRecommendationsComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'popular-movies',
    component: PopularMoviesComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'new-movies',
    component: NewMoviesComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true }), DxDataGridModule, DxFormModule],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [HomeComponent, ProfileComponent]
})
export class AppRoutingModule { }

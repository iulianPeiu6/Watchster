import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { SideNavOuterToolbarModule, SideNavInnerToolbarModule, SingleCardModule } from './layouts';
import { FooterModule, ResetPasswordFormModule, CreateAccountFormModule, ChangePasswordFormModule, LoginFormModule } from './shared/components';
import { AuthService, ScreenService, AppInfoService, MovieService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import { MoviesModule } from './pages/movies/movies.component';
import { MovieModule } from './pages/movie/movie.component';
import { MovieRecommendationsModule } from './pages/movie-recommendations/movie-recommendations.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RatingModule, RatingConfig } from 'ngx-bootstrap/rating';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
    MoviesModule,
    MovieModule,
    MovieRecommendationsModule,
    NgbModule,
    BrowserAnimationsModule
  ],
  providers: [AuthService, 
    ScreenService, 
    AppInfoService, 
    MovieService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { throwError } from 'rxjs';

export interface IUser {
  id: string,
  email: string,
  isSubscribed: boolean,
  registrationDate: string
}
export class serverMessages {
  public static UserNotFound : string = "User does not exist in database";
  public static MovieNotFound : string = "Movie does not exist in database";
  public static RatingNotInRange : string = "Rating value is not between 0 and 10";
  public static MovieAlreadyRated : string = "Current Movie was already rated by the user";
}


export class LoginResponse {
  constructor(public user: IUser, public jwtToken: string, public errorMessage: string) { }
}

export class MessageResponse {
  constructor(public message: string) { }
}

const defaultPath = '/';
const resetLinkDomain = location.origin + "/change-password";

@Injectable()
export class AuthService {
  public _user: LoginResponse | undefined;
  get loggedIn(): boolean {
    return this._user == undefined ? false : true;
  }

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router, private http: HttpClient) { }

  async logIn(email: string, password: string) {

    try {
      // Send request
      const response = await this.http
        .post<LoginResponse>('/api/1/User/Authenticate', { email: email, password: password })
        .toPromise()

      if(this._user == undefined){
        this._user = response;
      }
      this.router.navigate(['/home']);

      return {
        isOk: true,
        message: "Authenticated Successfully!"
      };
    }
    catch {
      return {
        isOk: false,
        message: "Authentication failed"
      };
    }
  }

  async getUser() {
    try {
      // Send request

      return {
        isOk: true,
        data: this._user
      };
    }
    catch {
      return {
        isOk: false,
        data: null
      };
    }
  }

  async createAccount(email: string, password: string) {
    try {
      // Send request
      const response = await this.http
        .post<LoginResponse>('/api/1/User/Register', { email: email, password: password, isSubscribed: true })
        .toPromise()

      if(response.errorMessage == "Invalid Data")
      {
        return {
          isOk: false,
          message: "Failed to create account"
        };
      }

      if(this._user == undefined){
          this._user = response;
        }

      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to create account"
      };
    }
  }

  async changePassword(password: string, recoveryCode: string) {
    try {
      // Send request
      const response = await this.http
        .patch<MessageResponse>('/api/1/User/ChangeNewPassword', { code: recoveryCode, password: password} )
        .toPromise()

      if(response.message == "The given code does not exist")
        throw throwError("not found");
      
      return {
        isOk: true
      };
    }
    catch (e){
      console.log(e);
      return {
        isOk: false,
        message: "Failed to change password"
      }
    };
  }

  async verifyPassword(recoveryCode: string) {
    try {
      const response = await this.http
        .post<MessageResponse>('/api/1/User/VerifyPasswordCode', { code: recoveryCode} )
        .toPromise()

      if (response.message == "The given code does not exist")
        throw throwError("not found");

      return {
        isOk: true
      };
    }
    catch (e){
      return {
        isOk: false,
        message: "Failed to access change password page"
      }
    };
  }

  async resetPassword(email: string) {
    try {
      // Send request
      const response = await this.http
        .patch<MessageResponse>('/api/1/User/SendEmailChangePassword', { email: email, endpoint: resetLinkDomain} )
        .toPromise()

      console.log(response);

      return {
        isOk: true,
        message: "Email was sent!"
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to reset password"
      };
    }
  }

  async logOut() {
    this._user = undefined;
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'create-account',
      'change-password/:recoveryCode'
    ].includes(route.routeConfig?.path || defaultPath);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
    }

    return isLoggedIn || isAuthForm;
  }
}

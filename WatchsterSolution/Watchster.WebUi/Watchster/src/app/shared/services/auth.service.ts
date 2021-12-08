import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HttpClient } from "@angular/common/http";

export interface IUser {
  id: string;
  email: string;
  token: string;
  isSubscribed: boolean;
  avatarUrl?: string
}

const defaultPath = '/';
const defaultUser = {
  email: 'sandra@example.com',
  avatarUrl: 'https://js.devexpress.com/Demos/WidgetsGallery/JSDemos/images/employees/06.png',
  id: '1',
  isSubscribed: true,
  token: '1'
};

@Injectable()
export class AuthService {
  private _user: IUser | null = defaultUser;
  private token: any;

  get loggedIn(): boolean {
    return !!this._user;
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
        .post<IUser>('/api/1/User/Authenticate', { email: email, password: password })
        .toPromise()
      
        if(this._user){
          this._user.email = response.email;
          this._user.avatarUrl = response.avatarUrl;
          this._user.id = response.id;
          this._user.isSubscribed = response.isSubscribed;
          this._user.token = response.token;
          }

      //save user details
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
        .post<IUser>('/api/1/User/Register', { email: email, password: password, isSubscribed: true })
        .toPromise()

      if(this._user){
      this._user.email = response.email;
      this._user.avatarUrl = response.avatarUrl;
      this._user.id = response.id;
      this._user.isSubscribed = response.isSubscribed;
      this._user.token = response.token;
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

  async changePassword(email: string, recoveryCode: string) {
    try {
      // Send request
      console.log(email, recoveryCode);

      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to change password"
      }
    };
  }

  async resetPassword(email: string) {
    try {
      // Send request
      console.log(email);

      return {
        isOk: true
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
    this._user = null;
    this.token = null;
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

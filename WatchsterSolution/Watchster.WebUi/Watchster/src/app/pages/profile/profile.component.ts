import { Component, OnInit } from '@angular/core';
import { AuthService, IUser } from 'src/app/shared/services';

@Component({
  templateUrl: 'profile.component.html',
  styleUrls: [ './profile.component.scss' ]
})

export class ProfileComponent implements OnInit {
  user!: IUser | null;
  colCountByScreen: object;
  subscriptions: Array<string>;

  constructor(private authService: AuthService) {
    this.subscriptions = ["Subscribe", "Unsubscribe"]
    this.colCountByScreen = {
      xs: 1,
      sm: 1,
      md: 2,
      lg: 2
    };
  }
  ngOnInit(): void {
    this.authService.getUser().then((e) => this.user = e.data)
  }
}

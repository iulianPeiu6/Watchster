import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme/ui/validation_rules';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../../services';


@Component({
  selector: 'app-change-passsword-form',
  templateUrl: './change-password-form.component.html'
})
export class ChangePasswordFormComponent implements OnInit {
  loading = false;
  formData: any = {};
  recoveryCode: string = '';

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) { 
  }

  async ngOnInit() {
    this.recoveryCode = this.router.url.split('/')[2];
    const result = await this.authService.verifyPassword(this.recoveryCode);
    if(!result.isOk)
      this.router.navigate(['/login-form']);
  }

  async onSubmit(e: Event) {
    e.preventDefault();
    const { password } = this.formData;
    this.loading = true;
    const result = await this.authService.changePassword(password, this.recoveryCode);
    this.loading = false;

    if (result.isOk) {
      notify("Password successfully changed!", 'success', 2500);
      await this.delay(2500);
      this.router.navigate(['/login-form']);
    } else {
      notify(result.message, 'error', 2000);
    }
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
}

  confirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.password;
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule
  ],
  declarations: [ ChangePasswordFormComponent ],
  exports: [ ChangePasswordFormComponent ]
})
export class ChangePasswordFormModule { }

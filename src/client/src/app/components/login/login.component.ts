import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  model: any = {
    // password: 'Default@password1',
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  login() {
    this.spinner.show();

    this.authService.login(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.router.navigate(['/home']);
        this.alertify.success('Welcome!');
      },
      error: (error) => {
        this.spinner.hide();

        if (error.status === 400 && error.error.errors) {
          const errorObject = error.error.errors;

          for (const fieldName in errorObject) {
            if (errorObject.hasOwnProperty(fieldName)) {
              const fieldErrors = errorObject[fieldName];
              if (fieldErrors && fieldErrors.length > 0) {
                for (const errorMessage of fieldErrors) {
                  this.alertify.error(errorMessage);
                }
              }
            }
          }
        }
      },
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ForgotPasswordService } from 'src/app/services/forgot-password.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  model: any = {};
  email: any;

  constructor(
    private route: ActivatedRoute,
    private forgotPasswordService: ForgotPasswordService,
    private alertify: AlertifyService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.route.paramMap.subscribe({
      next: params => {
        this.spinner.hide();
        this.email = params.get('email');
        this.model = { email: this.email }
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  onFormSubmit() {
    this.spinner.show();

    this.forgotPasswordService.resetPassword(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Password has been reset successfully');
        this.router.navigate(['/']);
      },
      error: error => {
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

      }
    })
  }
}

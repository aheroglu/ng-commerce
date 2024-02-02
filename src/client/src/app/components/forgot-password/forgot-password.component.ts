import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ForgotPasswordService } from 'src/app/services/forgot-password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  model: any = {};

  constructor(
    private forgotPasswordService: ForgotPasswordService,
    private alertify: AlertifyService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  onFormSubmit() {
    this.spinner.show();

    this.forgotPasswordService.forgotPassword(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Email sent. Check your inbox');
        this.router.navigate(['/']);
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

}

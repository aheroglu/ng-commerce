import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { NewsletterService } from 'src/app/services/newsletter.service';

@Component({
  selector: 'app-newsletter',
  templateUrl: './newsletter.component.html',
  styleUrls: ['./newsletter.component.css'],
})
export class NewsletterComponent {
  model: any = {};

  constructor(
    private newsletterService: NewsletterService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  submitForm(): void {
    this.spinner.show();

    this.newsletterService.submitForm(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Successfully subscribed');
        this.model = {};
      },
      error: (error) => {
        this.spinner.hide();

        if (error.status === 400) {
          this.alertify.error(error.error);
        }

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

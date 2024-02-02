import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-for-admins',
  templateUrl: './for-admins.component.html',
  styleUrls: ['./for-admins.component.css']
})
export class ForAdminsComponent {
  model: any = { for: 'Admins' };

  constructor(private mailService: MailService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  onFormSubmit() {
    this.spinner.show();

    this.mailService.sendForAdmins(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Mail has been sent successfully for all admins');
        this.router.navigate(['/mail/mails']);
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

}

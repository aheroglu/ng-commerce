import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-for-members',
  templateUrl: './for-members.component.html',
  styleUrls: ['./for-members.component.css']
})
export class ForMembersComponent {
  model: any = { for: 'Members' };

  constructor(private mailService: MailService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  onFormSubmit() {
    this.spinner.show();

    this.mailService.sendForMembers(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Mail has been sent successfully for all members');
        this.router.navigate(['/mail/mails']);
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }
}

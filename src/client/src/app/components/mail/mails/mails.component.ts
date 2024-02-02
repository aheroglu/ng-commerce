import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Mail } from 'src/app/models/mail';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-mails',
  templateUrl: './mails.component.html',
  styleUrls: ['./mails.component.css']
})
export class MailsComponent implements OnInit {
  mails: Mail[];
  p: number = 1;

  constructor(
    private mailService: MailService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getMails();
  }

  getMails() {
    this.spinner.show();

    this.mailService.getMails().subscribe({
      next: mails => {
        this.spinner.hide();
        this.mails = mails;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

}

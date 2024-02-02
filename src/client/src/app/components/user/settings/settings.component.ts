import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { NewsletterService } from 'src/app/services/newsletter.service';
import { ProfileService } from 'src/app/services/profile.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  userId: string;
  email: string;
  isSubscribed: boolean;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private newsletterService: NewsletterService,
    private alertify: AlertifyService,
    private profileService: ProfileService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.userId = localStorage.getItem('userid');
    this.email = localStorage.getItem('email');

    if (this.userId && this.email) {
      this.spinner.hide();
      this.checkSubscription();
    }

  }

  checkSubscription() {
    this.spinner.show();

    this.newsletterService.checkSubcription(this.email).subscribe({
      next: response => {
        this.spinner.hide();
        this.isSubscribed = response;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.message);
      }
    });
  }

  subscribeNewsletter() {
    this.spinner.show();
    const model = { email: this.email };

    this.newsletterService.submitForm(model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Successfully subscribed');
        this.checkSubscription();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    })
  }

  unsubscribeNewsletter() {
    this.spinner.show();

    this.newsletterService.cancelSubscription(this.email).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Successfully unsubscribed');
        this.checkSubscription();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    })
  }

  deleteAccount() {
    this.spinner.show();

    this.profileService.deleteAccount(this.email).subscribe({
      next: () => {
        this.spinner.hide();
        this.router.navigate(['/']);
        this.authService.signOut();
        this.alertify.success('Your account has been deleted successfully');
      }
    })
  }

}

import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { User } from 'src/app/models/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  email: string;
  userInformation: User;
  userPassword: any = {};

  constructor(
    private profileService: ProfileService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.email = localStorage.getItem('email');

    if (this.email) {
      this.profileService.getUserProfile(this.email).subscribe({
        next: (information) => {
          this.spinner.hide();
          this.userInformation = information;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.message);
        }
      });
    }
  }

  updateUserInformation() {
    this.spinner.show();

    this.profileService.updateUserInformation(this.userInformation).subscribe({
      next: (userInformation) => {
        this.spinner.hide();
        this.alertify.success('User information has been updated');
        this.userInformation = userInformation;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  updateUserPassword() {
    this.spinner.show();

    if (this.userPassword.newPassword != this.userPassword.confirmPassword) {
      this.spinner.hide();
      this.alertify.error('Passwords not match!');
      return;
    }

    const model = {
      email: this.email,
      currentPassword: this.userPassword.currentPassword,
      newPassword: this.userPassword.newPassword,
    };

    this.profileService.updateUserPassword(model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('User password has been updated');
        this.userPassword = {};
      },
      error: (error) => {
        this.spinner.hide();
        console.log(error.error);
      },
    });
  }
}

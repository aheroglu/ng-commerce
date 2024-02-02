import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admins',
  templateUrl: './admins.component.html',
  styleUrls: ['./admins.component.css'],
})
export class AdminsComponent implements OnInit {
  admins: User[];
  currentAdminId: number;
  p: number = 1;

  constructor(
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService
  ) { }

  ngOnInit(): void {
    this.currentAdminId = parseInt(localStorage.getItem('userid'));
    this.getAdmins();
  }

  getAdmins() {
    this.spinner.show();

    this.userService.getAdmins(this.currentAdminId).subscribe({
      next: admins => {
        this.spinner.hide();
        this.admins = admins;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }
}

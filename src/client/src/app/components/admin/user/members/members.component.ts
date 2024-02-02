import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProfileService } from 'src/app/services/profile.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
})
export class MembersComponent implements OnInit {
  members: User[];
  p: number = 1;

  constructor(private userService: UserService,
    private alertify: AlertifyService,
    private profileService: ProfileService,
    private spinner: NgxSpinnerService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.getMembers();
  }

  getMembers() {
    this.spinner.show();

    this.userService.getMembers().subscribe({
      next: members => {
        this.spinner.hide();
        this.members = members;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  deleteMember(email: string): void {
    this.spinner.show();

    this.profileService.deleteAccount(email).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Member has been deleted successfully');
        this.router.navigate(['/admin/members']);
        this.getMembers();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }
}

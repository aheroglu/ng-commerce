import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-admin',
  templateUrl: './add-admin.component.html',
  styleUrls: ['./add-admin.component.css']
})
export class AddAdminComponent {
  model: any = {};

  constructor(private userService: UserService, private alertify: AlertifyService, private router: Router) { }

  onFormSubmit() {
    this.userService.addAdmin(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.alertify.success('Admin has been created successfully');
        this.router.navigate(['/admin/admins']);
      },
      error: error => {
        this.alertify.error(error.message);
        console.log(error.message);
      }
    });
  }

}

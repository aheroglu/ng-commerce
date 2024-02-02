import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../services/alertify.service';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  canActivate(): boolean {
    if (this.authService.isAdmin()) {
      return true;
    } else {
      this.alertify.error('You are not an admin!');
      this.router.navigate(['/home']);
      return false;
    }
  }
}

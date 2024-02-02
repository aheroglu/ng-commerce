import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../services/alertify.service';

@Injectable({
  providedIn: 'root',
})
export class GuestGuard {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  canActivate(): boolean {
    if (!this.authService.loggedIn()) {
      return true;
    } else {
      this.alertify.error('Already logged in!');
      this.router.navigate(['/home']);
      return false;
    }
  }
}

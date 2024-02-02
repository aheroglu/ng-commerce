import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, map, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  userRole: string;

  constructor(private http: HttpClient) {}

  signUp(model: any) {
    return this.http.post(environment.apiUrl + 'auth/signup', model).pipe(
      map((response: any) => {
        const result = response;
        if (result) {
          this.login(model).subscribe(() => {
            console.log('Logged In');
          });
        }
      }),
      catchError((error) => {
        console.error('Kayıt işlemi başarısız: ', error);
        return throwError(error); // Hata yeniden fırlatılıyor.
      })
    );
  }

  login(model: any) {
    if (
      localStorage.getItem('token') ||
      localStorage.getItem('role') ||
      localStorage.getItem('email')
    ) {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('email');
    }

    return this.http.post(environment.apiUrl + 'auth/login', model).pipe(
      map((response: any) => {
        const result = response;
        if (result) {
          localStorage.setItem('token', result.token);
          this.decodedToken = this.jwtHelper.decodeToken(result.token);
          localStorage.setItem('userid', this.decodedToken.nameid);
          localStorage.setItem('role', this.decodedToken.role);
          localStorage.setItem('email', this.decodedToken.email);
        }
      })
    );
  }

  getUserId(): string {
    return localStorage.getItem('userid');
  }

  isAdmin() {
    const role = localStorage.getItem('role');
    if (role == 'Admin') {
      return role;
    } else {
      return false;
    }
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  signOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
    localStorage.removeItem('cart');
    localStorage.removeItem('cartTotal');
    localStorage.removeItem('userid');
    this.userRole = null;
  }
}

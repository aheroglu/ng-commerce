import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ForgotPasswordService {

  constructor(private http: HttpClient) { }

  forgotPassword(model: any): Observable<any> {
    return this.http.post<any>(environment.apiUrl + 'auth/ForgotPassword', model);
  }

  resetPassword(model: any): Observable<any> {
    return this.http.post<any>(environment.apiUrl + 'auth/ResetPassword', model);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private http: HttpClient) { }

  getUserProfile(email: string): Observable<User> {
    return this.http.get<User>(
      environment.apiUrl + 'profile/GetUserInformation/' + email
    );
  }

  updateUserInformation(model: any): Observable<User> {
    return this.http.put<User>(
      environment.apiUrl + 'profile/UpdateUserInformation',
      model
    );
  }

  updateUserPassword(model: any) {
    return this.http.put(
      environment.apiUrl + 'profile/UpdateUserPassword',
      model
    );
  }

  deleteAccount(email: string): Observable<any> {
    return this.http.delete(environment.apiUrl + 'profile/' + email);
  }
}

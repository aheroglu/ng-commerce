import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) { }

  getAdmins(currentAdminId: number): Observable<User[]> {
    return this.http.get<User[]>(environment.apiUrl + 'users/GetAllAdmins/' + currentAdminId);
  }

  getMembers(): Observable<User[]> {
    return this.http.get<User[]>(environment.apiUrl + 'users/GetAllMembers');
  }

  addAdmin(model: any) {
    return this.http.post(environment.apiUrl + 'users/AddAdmin', model);
  }

}

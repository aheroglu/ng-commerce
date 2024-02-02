import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Mail } from '../models/mail';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(private http: HttpClient) { }

  getMails(): Observable<Mail[]> {
    return this.http.get<Mail[]>(environment.apiUrl + 'mails');
  }

  getMailsByMembership(membership: string): Observable<Mail[]> {
    return this.http.get<Mail[]>(environment.apiUrl + 'mails/MailsByMembership/' + membership);
  }

  sendForSubscribers(model: any): Observable<Mail> {
    return this.http.post<Mail>(environment.apiUrl + 'mails/SendForSubscribers', model);
  }

  sendForMembers(model: any): Observable<Mail> {
    return this.http.post<Mail>(environment.apiUrl + 'mails/SendForMembers', model);
  }

  sendForAdmins(model: any): Observable<Mail> {
    return this.http.post<Mail>(environment.apiUrl + 'mails/SendForAdmins', model);
  }

}

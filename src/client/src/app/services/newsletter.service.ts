import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Newsletter } from '../models/newsletter';

@Injectable({
  providedIn: 'root',
})
export class NewsletterService {
  constructor(private http: HttpClient) { }

  submitForm(model: any): Observable<Newsletter> {
    return this.http.post<Newsletter>(
      environment.apiUrl + 'newsletters',
      model
    );
  }

  checkSubcription(email: string) {
    return this.http.get<boolean>(environment.apiUrl + 'newsletters/CheckSubscription/' + email);
  }

  cancelSubscription(email: string): Observable<Newsletter> {
    return this.http.delete<Newsletter>(environment.apiUrl + 'newsletters/' + email);
  }
}

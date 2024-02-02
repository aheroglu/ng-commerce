import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from '../models/city';
import { environment } from 'src/environments/environment';
import { District } from '../models/district';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private http: HttpClient) {}

  getCities(): Observable<City[]> {
    return this.http.get<City[]>(environment.apiUrl + 'location/getcities');
  }

  getDistricts(): Observable<District[]> {
    return this.http.get<District[]>(
      environment.apiUrl + 'location/getdistricts'
    );
  }

  getDistrictsByCity(cityId: number): Observable<District[]> {
    return this.http.get<District[]>(
      environment.apiUrl + 'location/getdistricts/' + cityId
    );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Favourite } from '../models/favourite';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class FavouriteService {
  constructor(private http: HttpClient) {}

  getFavouritesByUser(appUserId: string): Observable<Favourite[]> {
    return this.http.get<Favourite[]>(
      environment.apiUrl + 'favourites/getfavouritesbyuser/' + appUserId
    );
  }

  addFavourite(favourite: any): Observable<Favourite> {
    return this.http.post<Favourite>(
      environment.apiUrl + 'favourites',
      favourite
    );
  }

  removeFavourite(id: number): Observable<Favourite> {
    return this.http.delete<Favourite>(environment.apiUrl + 'favourites/' + id);
  }
}

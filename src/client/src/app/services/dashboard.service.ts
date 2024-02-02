import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../models/order';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }

  countOfProducts() {
    return this.http.get(environment.apiUrl + 'dashboard/CountOfProducts');
  }

  countOfMembers() {
    return this.http.get(environment.apiUrl + 'dashboard/CountOfMembers');
  }

  countOfOrders() {
    return this.http.get(environment.apiUrl + 'dashboard/CountOfOrders');
  }

  countOfSubscribers() {
    return this.http.get(environment.apiUrl + 'dashboard/CountOfSubscribers');
  }

  lastFiveOrder(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.apiUrl + 'dashboard/LastFiveOrder');
  }

  topSellerProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + 'dashboard/TopSellerProducts');
  }

  topSellerProductsForHome(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + 'dashboard/TopSellerProductsForHome');
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompleteOrder } from '../models/complete-order';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { OrderItem } from 'sequelize';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.apiUrl + 'orders');
  }

  getOrdersByUser(userId: number): Observable<Order[]> {
    return this.http.get<Order[]>(
      environment.apiUrl + 'orders/GetOrdersByUser/' + userId
    );
  }

  getOrderDetailsByOrder(orderNo: number): Observable<Order> {
    return this.http.get<Order>(
      environment.apiUrl + 'orders/GetOrderDetailsByOrder/' + orderNo
    );
  }

  createOrder(orderData: any) {
    return this.http.post(environment.apiUrl + 'orders/CreateOrder', orderData);
  }
}

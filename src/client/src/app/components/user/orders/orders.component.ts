import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order';
import { AlertifyService } from 'src/app/services/alertify.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  orders: Order[];
  userId: number;
  p: number = 1;

  constructor(
    private orderService: OrderService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('userid'));

    if (this.userId) {
      this.getOrders();
    }
  }

  getOrders() {
    if (this.userId) {
      this.spinner.show();

      this.orderService.getOrdersByUser(this.userId).subscribe({
        next: orders => {
          this.spinner.hide();
          this.orders = orders;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }
}

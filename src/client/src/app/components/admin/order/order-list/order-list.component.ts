import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order';
import { AlertifyService } from 'src/app/services/alertify.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css'],
})
export class OrderListComponent implements OnInit {
  orders: Order[];
  p: number = 1;

  constructor(
    private orderService: OrderService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService
  ) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.spinner.show();

    this.orderService.getOrders().subscribe({
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

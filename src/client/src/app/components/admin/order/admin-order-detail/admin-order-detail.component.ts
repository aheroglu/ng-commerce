import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order';
import { AlertifyService } from 'src/app/services/alertify.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-admin-order-detail',
  templateUrl: './admin-order-detail.component.html',
  styleUrls: ['./admin-order-detail.component.css'],
})
export class AdminOrderDetailComponent implements OnInit {
  orderNo: any;
  order: Order;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.orderNo = params.get('orderNo');

        if (this.orderNo) {
          this.getOrder();
        }
      },
    });
  }

  getOrder() {
    if (this.orderNo) {
      this.spinner.show();
      this.orderService.getOrderDetailsByOrder(this.orderNo).subscribe({
        next: order => {
          this.spinner.hide();
          this.order = order;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }
}

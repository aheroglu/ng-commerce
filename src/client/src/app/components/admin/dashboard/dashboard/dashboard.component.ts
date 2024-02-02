import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order';
import { AlertifyService } from 'src/app/services/alertify.service';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  orders$: Observable<Order[]>;
  countOfProducts: any;
  countOfMembers: any;
  countOfOrders: any;
  countOfSubscribers: any;
  topSellerProducts: any;

  constructor(
    private dashboardService: DashboardService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getValues();
  }

  getValues() {
    this.spinner.show();

    this.dashboardService.countOfProducts().subscribe({
      next: countOfProducts => {
        this.countOfProducts = countOfProducts;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    });

    this.dashboardService.countOfMembers().subscribe({
      next: countOfMembers => {
        this.countOfMembers = countOfMembers;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    });

    this.dashboardService.countOfOrders().subscribe({
      next: countOfOrders => {
        this.countOfOrders = countOfOrders;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    });

    this.dashboardService.countOfSubscribers().subscribe({
      next: countOfSubscribers => {
        this.countOfSubscribers = countOfSubscribers;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    });

    this.dashboardService.topSellerProducts().subscribe({
      next: topSellerProducts => {
        this.topSellerProducts = topSellerProducts;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    })

    this.orders$ = this.dashboardService.lastFiveOrder();

    this.spinner.hide();
  }

}

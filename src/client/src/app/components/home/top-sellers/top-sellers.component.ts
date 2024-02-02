import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-top-sellers',
  templateUrl: './top-sellers.component.html',
  styleUrls: ['./top-sellers.component.css']
})
export class TopSellersComponent implements OnInit {
  products: Product[];

  constructor(
    private dashboardService: DashboardService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.spinner.show();

    this.dashboardService.topSellerProductsForHome().subscribe({
      next: products => {
        this.spinner.hide();
        this.products = products;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

}

import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-admin-product-list',
  templateUrl: './admin-product-list.component.html',
  styleUrls: ['./admin-product-list.component.css'],
})
export class AdminProductListComponent implements OnInit {
  products: Product[];
  p: number = 1;

  constructor(private productService: ProductService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.spinner.show();

    this.productService.getProducts().subscribe({
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

  deleteProduct(id: number): void {
    this.spinner.show();

    this.productService.deleteProduct(id).subscribe({
      next: () => {
        this.spinner.hide();
        this.getProducts();
        this.alertify.success('Product has been deleted successfully!');
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    })
  }
}

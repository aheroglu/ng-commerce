import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products-in-category',
  templateUrl: './products-in-category.component.html',
  styleUrls: ['./products-in-category.component.css'],
})
export class ProductsInCategoryComponent implements OnInit {
  urlHandle: string;
  products: Product[];
  p: number = 1;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.route.paramMap.subscribe({
      next: (params) => {
        this.urlHandle = params.get('urlHandle');

        if (this.urlHandle) {
          this.getProducts();
        } else {
          this.spinner.hide();
        }
      },
    });
  }

  getProducts() {
    this.spinner.show();

    if (this.urlHandle) {
      this.productService.getProductsByCategory(this.urlHandle).subscribe({
        next: products => {
          this.spinner.hide();
          this.products = products;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      })
    } else {
      this.spinner.hide();
    }
  }
}

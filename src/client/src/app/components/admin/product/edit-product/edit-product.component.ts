import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent implements OnInit {
  urlHandle: string;
  product: Product;
  categories: Category[];

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private categoryService: CategoryService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.urlHandle = params.get('urlHandle');

        if (this.urlHandle) {
          this.getProduct();
          this.getCategories();
        }
      },
    });
  }

  getCategories() {
    this.spinner.show();

    this.categoryService.getCategories().subscribe({
      next: categories => {
        this.spinner.hide();
        this.categories = categories;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  getProduct() {
    if (this.urlHandle) {
      this.spinner.show();

      this.productService.getProduct(this.urlHandle).subscribe({
        next: product => {
          this.spinner.hide();
          this.product = product;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }

  updateUrlHandle(): void {
    this.product.urlHandle = this.product.name
      .toLowerCase()
      .replace(new RegExp(' ', 'g'), '-');
  }

  onFormSubmit(): void {
    this.spinner.show();

    this.productService.updateProduct(this.product.id, this.product).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Product has been updated');
      },
      error: (error) => {
        this.spinner.hide();
        this.alertify.error(error.error);
      },
    });
  }
}

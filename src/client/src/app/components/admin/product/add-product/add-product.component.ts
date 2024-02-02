import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Category } from 'src/app/models/category';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  categories: Category[];
  model: any = {};

  constructor(
    private categoryService: CategoryService,
    private alertify: AlertifyService,
    private productService: ProductService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        this.alertify.error(error.error);
      },
    });
  }

  updateUrlHandle(): void {
    this.model.urlHandle = this.model.name
      .toLowerCase()
      .replace(new RegExp(' ', 'g'), '-');
  }

  onFormSubmit() {
    this.spinner.show();

    this.productService.addProduct(this.model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Product has been added successfully');
        this.router.navigate(['/admin/products']);
      },
      error: error => {
        this.spinner.hide();

        if (error.status === 400 && error.error.errors) {
          const errorObject = error.error.errors;

          for (const fieldName in errorObject) {
            if (errorObject.hasOwnProperty(fieldName)) {
              const fieldErrors = errorObject[fieldName];
              if (fieldErrors && fieldErrors.length > 0) {
                for (const errorMessage of fieldErrors) {
                  this.alertify.error(errorMessage);
                }
              }
            }
          }
        }
      }
    })
  }
}

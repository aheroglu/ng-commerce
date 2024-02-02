import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product-image',
  templateUrl: './add-product-image.component.html',
  styleUrls: ['./add-product-image.component.css']
})
export class AddProductImageComponent implements OnInit {
  urlHandle: string;
  product: Product;
  imageUrl: string;
  productId: string;

  constructor(private route: ActivatedRoute,
    private productService: ProductService,
    private alertify: AlertifyService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: params => {
        this.urlHandle = params.get('urlHandle');
        this.productId = params.get('id');

        if (this.urlHandle) {
          this.getProduct();
        }
      }
    })
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

  onFormSubmit() {
    this.spinner.show();

    const model = {
      productId: this.product.id,
      imageUrl: this.imageUrl
    };

    this.productService.addProductImage(model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Product image has been added successfully');
        this.router.navigate(['/admin/products']);
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

}

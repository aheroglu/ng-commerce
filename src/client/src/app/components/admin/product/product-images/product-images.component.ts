import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { ProductImage } from 'src/app/models/product-image';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ProductImageService } from 'src/app/services/product-image.service';

@Component({
  selector: 'app-product-images',
  templateUrl: './product-images.component.html',
  styleUrls: ['./product-images.component.css']
})
export class ProductImagesComponent implements OnInit {
  productImages: ProductImage[];
  urlHandle: string;

  constructor(private route: ActivatedRoute,
    private productImageService: ProductImageService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: params => {
        this.urlHandle = params.get('urlHandle');
      }
    });

    if (this.urlHandle) {
      this.getProductImages();
    }
  }

  getProductImages() {
    if (this.urlHandle) {
      this.spinner.show();

      this.productImageService.getImages(this.urlHandle).subscribe({
        next: productImages => {
          this.spinner.hide();
          this.productImages = productImages;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }

  deleteImage(productImage: ProductImage): void {
    this.spinner.show();

    this.productImageService.deleteImage(productImage.id).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Image has been deleted successfully!');
        this.getProductImages();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    })
  }

}

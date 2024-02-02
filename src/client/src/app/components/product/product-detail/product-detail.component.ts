import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Product } from 'src/app/models/product';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { FavouriteService } from 'src/app/services/favourite.service';
import { ProductService } from 'src/app/services/product.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent implements OnInit {
  urlHandle: string;
  product: Product;
  relatedProducts: Product[];
  reviews: Review[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private cartService: CartService,
    private reviewService: ReviewService,
    private alertify: AlertifyService,
    private favouriteService: FavouriteService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.urlHandle = params.get('urlHandle');

        if (this.urlHandle) {
          this.getProduct();
          this.getReviews();
        }
      },
    });
  }

  getProduct() {
    if (this.urlHandle) {
      this.spinner.show();

      this.productService.getProduct(this.urlHandle).subscribe({
        next: product => {
          this.spinner.hide();
          this.product = product;
          this.getReviews();
          this.getRelatedProducts();
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }

  getReviews() {
    if (this.urlHandle) {
      this.spinner.show();

      this.reviewService.getProductReviews(this.urlHandle).subscribe({
        next: reviews => {
          this.spinner.hide();
          this.reviews = reviews;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }

  getRelatedProducts() {
    this.spinner.show();

    this.productService.getRelatedProductsByCategory(this.product.category.urlHandle).subscribe({
      next: products => {
        this.spinner.hide();
        this.relatedProducts = products;
        return;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  addToCart(product: Product) {
    this.spinner.show();

    if (!this.authService.loggedIn()) {
      this.spinner.hide();
      this.alertify.error('Please login');
      this.router.navigate(['/login']);
      return;
    }

    if (this.authService.isAdmin()) {
      this.spinner.hide();
      this.alertify.error('Only members can add product to cart!');
      return;
    }

    this.cartService.addToCart(product);
    this.alertify.success('Product added to cart!');
    this.spinner.hide();
  }

  addToFavourites(product: Product) {
    this.spinner.show();

    if (!this.authService.loggedIn()) {
      this.spinner.hide();
      this.alertify.error('Please login');
      this.router.navigate(['/login']);
      return;
    }

    if (this.authService.isAdmin()) {
      this.spinner.hide();
      this.alertify.error('Only members can add product to favourites!');
      return;
    }

    const favourite = {
      productId: product.id,
      appUserId: localStorage.getItem('userid'),
    };

    this.favouriteService.addFavourite(favourite).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Product added to favourites!');
      },
      error: (error) => {
        this.spinner.hide();
        this.alertify.error(error.error);
      },
    });
  }
}

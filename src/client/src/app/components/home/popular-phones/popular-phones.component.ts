import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { FavouriteService } from 'src/app/services/favourite.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-popular-phones',
  templateUrl: './popular-phones.component.html',
  styleUrls: ['./popular-phones.component.css'],
})
export class PopularPhonesComponent implements OnInit {
  products: Product[];

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private favouriteService: FavouriteService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.spinner.show();

    this.productService.popularMobilePhones().subscribe({
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

  addToCart(product: Product): void {
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
    this.spinner.hide();
    this.alertify.success('Product added to cart!');
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

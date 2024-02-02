import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, map, of } from 'rxjs';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { CategoryService } from 'src/app/services/category.service';
import { FavouriteService } from 'src/app/services/favourite.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  categories$: Observable<Category[]>;
  products: Product[];
  searchTerm: string = '';
  selectedSortOption: string = 'Featured';
  selectedCategory: Category | null = null;
  p: number = 1;

  constructor(
    private categoryService: CategoryService,
    private productService: ProductService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    private favouriteService: FavouriteService,
    private cartService: CartService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();
    this.getProducts();
  }

  getProducts() {
    this.selectedCategory = null;
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

  filterByCategory(category: Category) {
    this.searchTerm = '';
    this.selectedCategory = category;
    this.spinner.show();

    this.productService.getProductsByCategory(category.urlHandle).subscribe({
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

  onSortOptionChange() {
    this.spinner.show();

    if (this.selectedSortOption === 'LowToHigh') {
      this.productService.sortProductsByPrice('LowToHigh').subscribe({
        next: (products) => {
          this.spinner.hide();
          this.products = products;
          this.searchTerm = '';
        },
      });
    } else if (this.selectedSortOption === 'HighToLow') {
      this.productService.sortProductsByPrice('HighToLow').subscribe({
        next: (products) => {
          this.spinner.hide();
          this.products = products;
          this.searchTerm = '';
        },
      });
    } else if (this.selectedSortOption === 'Featured') {
      this.spinner.hide();
      this.getProducts();
      this.searchTerm = '';
    }
  }

  onSearchTermChange() {
    if (this.searchTerm.trim() === '') {
      this.getProducts();
      return;
    }

    this.products = this.products.filter((product) =>
      product.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

}

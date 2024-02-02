import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { CartItem } from 'src/app/models/cart-item';
import { City } from 'src/app/models/city';
import { District } from 'src/app/models/district';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CartService } from 'src/app/services/cart.service';
import { LocationService } from 'src/app/services/location.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css'],
})
export class CheckoutComponent implements OnInit {
  cartItems: CartItem[];
  total: number = 0;
  cities: City[];
  districts: District[];
  cityId: number;
  order: any = {};

  constructor(
    private router: Router,
    private alertify: AlertifyService,
    private cartService: CartService,
    private locationService: LocationService,
    private orderService: OrderService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.cartItems = JSON.parse(localStorage.getItem('cart')) || [];
    this.getCities();
    this.getDistricts();

    if (this.cartItems.length == 0 || undefined) {
      this.alertify.error('There is no products in your cart!');
      this.router.navigate(['/cart']);
    }
  }

  getCities() {
    this.spinner.show();

    this.locationService.getCities().subscribe({
      next: cities => {
        this.spinner.hide();
        this.cities = cities;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  getDistricts() {
    this.spinner.show();

    this.locationService.getDistricts().subscribe({
      next: districts => {
        this.spinner.hide();
        this.districts = districts;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  getNumberOfProducts(): number {
    return this.cartService.getNumberOfProducts();
  }

  calculateProducts() {
    let total = 0;

    for (const item of this.cartItems) {
      const price = item.product.price;
      const quantity = item.quantity;

      total += price * quantity;
    }

    return total;
  }

  calculateTotal() {
    this.total = this.calculateProducts();

    if (this.total < 50) {
      this.total += 50;
      return this.total;
    } else {
      return this.total;
    }
  }

  onCityChange(event: any) {
    const selectedCityId = event.target.value;
    this.cityId = selectedCityId;
    this.locationService.getDistrictsByCity(selectedCityId).subscribe({
      next: districts => {
        this.districts = districts;
      },
      error: error => {
        this.alertify.error(error.error);
      }
    });
  }

  onSubmit() {
    this.spinner.show();

    const cartItemsFromLocalStorage = JSON.parse(localStorage.getItem('cart'));
    const cartItems = (cartItemsFromLocalStorage as any[]).map((item) => ({
      ProductId: item.product.id,
      Quantity: item.quantity,
    }));
    let cartTotal = parseFloat(localStorage.getItem('cartTotal')) || 0;
    if (cartTotal < 50) {
      cartTotal += 50;
    }

    const orderData = {
      appUserId: localStorage.getItem('userid'),
      email: localStorage.getItem('email'),
      address: this.order.address,
      addressTitle: this.order.addressTitle,
      zipCode: this.order.zipCode,
      total: cartTotal,
      cityId: this.order.city,
      districtId: this.order.district,
      cartItems: cartItems,
    };

    this.orderService.createOrder(orderData).subscribe({
      next: () => {
        this.spinner.hide();
        this.cartService.clearCart();
        this.alertify.success('Order completed successfully!');
        this.router.navigate(['/home']);
      },
      error: (error) => {
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
      },
    });
  }
}

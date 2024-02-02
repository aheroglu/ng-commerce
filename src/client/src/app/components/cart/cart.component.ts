import { Component, OnInit } from '@angular/core';
import { CartItem } from 'src/app/models/cart-item';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  cartItems: CartItem[];
  isFreeShipping: boolean;
  total: number = 0;
  cartTotal: number;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.cartItems = JSON.parse(localStorage.getItem('cart')) || [];
    this.cartTotal = parseFloat(localStorage.getItem('cartTotal')) || 0;
    this.calculateTotal();
  }

  increaseQuantity(productId: number, price: number) {
    const productInCart = this.cartItems.find((p) => p.product.id == productId);
    productInCart.quantity++;
    localStorage.setItem('cart', JSON.stringify(this.cartItems));

    this.cartTotal += price;
    localStorage.setItem('cartTotal', this.cartTotal.toString());

    this.calculateTotal();
  }

  decreaseQuantity(productId: number, price: number) {
    const productInCart = this.cartItems.find((p) => p.product.id == productId);
    if (productInCart.quantity > 1) {
      productInCart.quantity--;
      localStorage.setItem('cart', JSON.stringify(this.cartItems));

      this.cartTotal -= price;
      localStorage.setItem('cartTotal', this.cartTotal.toString());

      this.calculateTotal();
    }
  }

  removeProduct(productId: number) {
    const index = this.cartItems.findIndex((p) => p.product.id == productId);
    const removedItem = this.cartItems.splice(index, 1)[0];

    if (removedItem) {
      const removedItemTotal = removedItem.product.price * removedItem.quantity;
      this.cartTotal -= removedItemTotal;
      localStorage.setItem('cartTotal', this.cartTotal.toString());

      localStorage.setItem('cart', JSON.stringify(this.cartItems));
      this.alertify.warning('Product removed from cart');

      this.calculateTotal();
    }
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
      this.isFreeShipping = false;
      this.total += 50;
      return this.total;
    } else {
      this.isFreeShipping = true;
      return this.total;
    }
  }
}

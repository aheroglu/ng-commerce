import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { CartItem } from '../models/cart-item';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private authService: AuthService) {}

  addToCart(product: Product): void {
    let cartItems = JSON.parse(localStorage.getItem('cart')) || [];

    let existingItem: CartItem = cartItems.find(
      (item: CartItem) => item.product.name === product.name
    );
    if (existingItem) {
      existingItem.quantity++;
    } else {
      cartItems.push({
        product,
        quantity: 1,
      });
    }

    let cartTotal = parseFloat(localStorage.getItem('cartTotal')) || 0;
    cartTotal += product.price;

    localStorage.setItem('cart', JSON.stringify(cartItems));
    localStorage.setItem('cartTotal', cartTotal.toString());
  }

  getNumberOfProducts(): number {
    const cartData = JSON.parse(localStorage.getItem('cart')) || [];
    return cartData.length;
  }

  clearCart(): void {
    localStorage.removeItem('cart');
    localStorage.removeItem('cartTotal');
  }
}

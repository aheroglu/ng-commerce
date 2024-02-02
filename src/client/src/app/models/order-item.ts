import { Order } from './order';
import { Product } from './product';

export interface OrderItem {
  id: number;
  quantity: number;
  order: Order;
  product: Product;
  createdAt: Date;
}

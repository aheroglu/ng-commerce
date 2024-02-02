import { Order } from './order';

export interface CompleteOrder {
  order: Order;
  quantity: number;
  userId: number;
}

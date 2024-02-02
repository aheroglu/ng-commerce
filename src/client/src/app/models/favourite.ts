import { Product } from './product';

export interface Favourite {
  id: number;
  product: Product;
  appUserId: number;
  createdAt: Date;
}

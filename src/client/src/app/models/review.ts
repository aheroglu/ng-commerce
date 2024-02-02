import { Product } from './product';

export interface Review {
  id: number;
  content: string;
  rating: number;
  createdAt: Date;
  product: Product;
  appUserId: number;
  user: string;
}

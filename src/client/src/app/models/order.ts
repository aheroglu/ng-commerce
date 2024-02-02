import { City } from './city';
import { District } from './district';
import { OrderItem } from './order-item';
import { User } from './user';

export interface Order {
  id: number;
  orderNo: number;
  addressTitle: string;
  address: string;
  zipCode: string;
  total: number;
  city: City;
  district: District;
  appUserId: number;
  user: string;
  orderItems: OrderItem[];
  createdAt: Date;
  appUser: User;
}

import { Category } from './category';
import { ProductImage } from './product-image';

export interface Product {
  id: number;
  name: string;
  brand: string;
  model: string;
  urlHandle: string;
  shortDescription: string;
  description: string;
  imageUrl: string;
  price: number;
  stockQuantity: number;
  createdAt: Date;
  category: Category;
  categoryId: number;
  productImages: ProductImage[];
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { environment } from 'src/environments/environment';
import { ProductImage } from '../models/product-image';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + 'products');
  }

  getProduct(urlHandle: string): Observable<Product> {
    return this.http.get<Product>(environment.apiUrl + 'products/' + urlHandle);
  }

  getProductsByCategory(urlHandle: string): Observable<Product[]> {
    return this.http.get<Product[]>(
      environment.apiUrl + 'products/GetProductByCategory/' + urlHandle
    );
  }

  getRelatedProductsByCategory(urlHandle: string): Observable<Product[]> {
    return this.http.get<Product[]>(
      environment.apiUrl + 'products/GetRelatedProductsByCategory/' + urlHandle
    );
  }

  sortProductsByPrice(
    sortOrder: string,
    categoryId?: number
  ): Observable<Product[]> {
    let requestUrl =
      environment.apiUrl + 'products/SortProductsByPrice/' + sortOrder;

    if (categoryId) {
      requestUrl += '/' + categoryId;
    }

    return this.http.get<Product[]>(requestUrl);
  }

  popularMobilePhones(): Observable<Product[]> {
    return this.http.get<Product[]>(
      environment.apiUrl + 'products/PopularMobilePhones'
    );
  }

  popularLaptops(): Observable<Product[]> {
    return this.http.get<Product[]>(
      environment.apiUrl + 'products/PopularLaptops'
    );
  }

  updateProduct(id: number, model: any): Observable<Product> {
    return this.http.put<Product>(environment.apiUrl + 'products/' + id, model);
  }

  addProduct(model: any): Observable<Product> {
    return this.http.post<Product>(environment.apiUrl + 'products', model);
  }

  addProductImage(model: any): Observable<ProductImage> {
    return this.http.post<ProductImage>(environment.apiUrl + 'products/AddProductImage/', model);
  }

  deleteProduct(id: number): Observable<Product> {
    return this.http.delete<Product>(environment.apiUrl + 'products/' + id);
  }
}

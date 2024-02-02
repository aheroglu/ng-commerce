import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductImage } from '../models/product-image';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductImageService {

  constructor(private http: HttpClient) { }

  getImages(urlHandle: string): Observable<ProductImage[]> {
    return this.http.get<ProductImage[]>(environment.apiUrl + 'productImages/GetImagesByProduct/' + urlHandle);
  }

  deleteImage(id: number): Observable<ProductImage> {
    return this.http.delete<ProductImage>(environment.apiUrl + 'productImages/' + id);
  }
}

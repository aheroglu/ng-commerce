import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(environment.apiUrl + 'categories');
  }

  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(environment.apiUrl + 'categories/' + id);
  }

  getCategoryByUrlHandle(urlHandle: string): Observable<Category> {
    return this.http.get<Category>(
      environment.apiUrl + 'categories/GetCategoryByUrlHandle/' + urlHandle
    );
  }

  deleteCategory(id: number): Observable<Category> {
    return this.http.delete<Category>(environment.apiUrl + 'categories/' + id);
  }

  editCategory(id: number, category: Category): Observable<Category> {
    return this.http.put<Category>(
      environment.apiUrl + 'categories/' + id,
      category
    );
  }

  addCategory(model: any): Observable<Category> {
    return this.http.post<Category>(environment.apiUrl + 'categories', model);
  }
}

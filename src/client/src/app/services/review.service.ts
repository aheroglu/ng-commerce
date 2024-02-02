import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../models/review';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private http: HttpClient) {}

  getReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(environment.apiUrl + 'reviews');
  }

  getReviewById(id: number): Observable<Review> {
    return this.http.get<Review>(environment.apiUrl + 'reviews/GetReviewById/' + id);
  }

  getProductReviews(urlHandle: string): Observable<Review[]> {
    return this.http.get<Review[]>(
      environment.apiUrl + 'reviews/GetReviewByUrlHandle/' + urlHandle
    );
  }

  addReview(review: any) {
    return this.http.post(environment.apiUrl + 'reviews', review);
  }

  getReviewsByUser(userId: number): Observable<Review[]> {
    return this.http.get<Review[]>(
      environment.apiUrl + 'reviews/GetReviewsByUser/' + userId
    );
  }

  getReviewByUser(
    userId: number,
    productUrlHandle: string
  ): Observable<Review> {
    return this.http.get<Review>(
      environment.apiUrl +
        'reviews/GetReviewByUser/' +
        userId +
        '/' +
        productUrlHandle
    );
  }

  updateReview(model: any): Observable<Review> {
    return this.http.put<Review>(environment.apiUrl + 'reviews', model);
  }

  deleteReview(id: number): Observable<Review> {
    return this.http.delete<Review>(environment.apiUrl + 'reviews/' + id);
  }
}

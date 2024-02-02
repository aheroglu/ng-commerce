import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css'],
})
export class ReviewsComponent implements OnInit {
  reviews: Review[];
  p: number = 1;

  constructor(
    private reviewService: ReviewService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getReviews();
  }

  getReviews() {
    this.spinner.show();

    this.reviewService.getReviews().subscribe({
      next: reviews => {
        this.spinner.hide();
        this.reviews = reviews;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  deleteReview(review: Review): void {
    this.spinner.show();

    this.reviewService.deleteReview(review.id).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Review has been deleted');
        this.getReviews();
      },
      error: (error) => {
        this.spinner.hide();
        this.alertify.error(error.error);
      },
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-detail',
  templateUrl: './review-detail.component.html',
  styleUrls: ['./review-detail.component.css'],
})
export class ReviewDetailComponent implements OnInit {
  review: Review;
  reviewId: number;

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.reviewId = parseInt(params.get('id'));

        if (this.reviewId) {
          this.getReview();
        }
      },
    });
  }

  getReview() {
    if (this.reviewId) {
      this.spinner.show();

      this.reviewService.getReviewById(this.reviewId).subscribe({
        next: review => {
          this.spinner.hide();
          this.review = review;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
    }
  }
}

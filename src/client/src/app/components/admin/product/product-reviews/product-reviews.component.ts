import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-product-reviews',
  templateUrl: './product-reviews.component.html',
  styleUrls: ['./product-reviews.component.css']
})
export class ProductReviewsComponent implements OnInit {
  reviews: Review[];
  urlHandle: string;

  constructor(private route: ActivatedRoute,
    private reviewService: ReviewService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: params => {
        this.urlHandle = params.get('urlHandle');
      }
    });

    if (this.urlHandle) {
      this.getReviews();
    }
  }

  getReviews() {
    if (this.urlHandle) {
      this.spinner.show();

      this.reviewService.getProductReviews(this.urlHandle).subscribe({
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
  }

  deleteReview(review: Review): void {
    this.spinner.show();

    this.reviewService.deleteReview(review.id).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Review has been deleted successfully!');
        this.getReviews();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    })
  }

}

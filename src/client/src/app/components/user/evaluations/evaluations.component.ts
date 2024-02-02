import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-evaluations',
  templateUrl: './evaluations.component.html',
  styleUrls: ['./evaluations.component.css'],
})
export class EvaluationsComponent implements OnInit {
  evaluations: Review[];
  userId: number;
  evaluatedProduct?: Review | null;
  evaluate: any = {};
  p: number = 1;

  constructor(
    private reviewService: ReviewService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('userid'));
    this.getEvaluations();
  }

  getEvaluations() {
    this.spinner.show();

    this.reviewService.getReviewsByUser(this.userId).subscribe({
      next: evaluations => {
        this.spinner.hide();
        this.evaluations = evaluations;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  deleteEvalaute(review: Review) {
    this.spinner.show();

    this.reviewService.deleteReview(review.id).subscribe({
      next: () => {
        this.spinner.hide();
        this.getEvaluations();
        this.alertify.success('Evaluate has been deleted successfully');
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  getEvaluate(product: Product) {
    this.spinner.show();

    this.reviewService
      .getReviewByUser(this.userId, product.urlHandle).subscribe({
        next: (review) => {
          this.spinner.hide();
          this.evaluatedProduct = review;
          this.evaluate = this.evaluatedProduct;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      });
  }

  updateEvaluate() {
    this.spinner.show();

    const model = {
      content: this.evaluate.content,
      rating: this.evaluate.rating,
      productId: this.evaluate.product.id,
      appUserId: this.userId,
    };

    this.reviewService.updateReview(model).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Evaluate has been updated');
        this.getEvaluations();
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }
}

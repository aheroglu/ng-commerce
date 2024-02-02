import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order';
import { Product } from 'src/app/models/product';
import { Review } from 'src/app/models/review';
import { AlertifyService } from 'src/app/services/alertify.service';
import { OrderService } from 'src/app/services/order.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css'],
})
export class OrderDetailComponent implements OnInit {
  orderNo: any;
  order: Order;
  evaluatedProduct?: Product;
  evaluate?: any = {};
  getEvaluate$: Observable<Review> | null;
  alreadyEvaluated: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService,
    private reviewService: ReviewService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.orderNo = params.get('orderNo');

        if (this.orderNo) {
          this.getOrder();
        }
      },
    });
  }

  getOrder() {
    if (this.orderNo) {
      this.spinner.show();

      this.orderService.getOrderDetailsByOrder(this.orderNo).subscribe({
        next: order => {
          this.spinner.hide();
          this.order = order;
        },
        error: error => {
          this.spinner.hide();
          this.alertify.error(error.error);
        }
      })
    }
  }

  productForEvaluate(product: Product) {
    this.spinner.show();
    this.evaluatedProduct = null;
    this.evaluate = {};
    this.alreadyEvaluated = false;

    const userId = parseInt(localStorage.getItem('userid'));
    this.reviewService.getReviewByUser(userId, product.urlHandle).subscribe({
      next: (review) => {
        this.spinner.hide();
        this.alertify.warning('This product has been evaluated before');
        this.alreadyEvaluated = true;
        this.evaluatedProduct = product;
        this.evaluate = review ? review : {};
      },
      error: (error) => {
        this.spinner.hide();
        this.evaluatedProduct = product;
      },
    });
  }

  onEvaluate(): void {
    this.spinner.show();

    const evaluate = {
      content: this.evaluate.content,
      rating: this.evaluate.rating,
      productId: this.evaluatedProduct.id,
      appUserId: localStorage.getItem('userid'),
    };

    this.reviewService.addReview(evaluate).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Thanks for evaluate');
      },
      error: (error) => {
        this.spinner.hide();

        if (error.status === 400 && error.error.errors) {
          const errorObject = error.error.errors;

          for (const fieldName in errorObject) {
            if (errorObject.hasOwnProperty(fieldName)) {
              const fieldErrors = errorObject[fieldName];
              if (fieldErrors && fieldErrors.length > 0) {
                for (const errorMessage of fieldErrors) {
                  this.alertify.error(errorMessage);
                }
              }
            }
          }
        }
      },
    });
  }
}

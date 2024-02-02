import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css'],
})
export class EditCategoryComponent implements OnInit {
  urlHandle: string;
  category: Category;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private alertify: AlertifyService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.route.paramMap.subscribe({
      next: (params) => {
        this.urlHandle = params.get('urlHandle');

        if (this.urlHandle) {
          this.spinner.hide();
          this.categoryService
            .getCategoryByUrlHandle(this.urlHandle)
            .subscribe({
              next: (category) => {
                this.category = category;
              },
              error: (error) => {
                this.router.navigate(['/404']);
              },
            });
        } else {
          this.spinner.hide();
        }
      },
    });
  }

  editCategory() {
    this.spinner.show();

    this.categoryService
      .editCategory(this.category.id, this.category)
      .subscribe({
        next: () => {
          this.spinner.hide();
          this.alertify.success('Category has been updated');
        },
        error: (error) => {
          this.spinner.hide();
          this.alertify.error(error.error);
        },
      });
  }

  updateUrlHandle(): void {
    this.category.urlHandle = this.category.name
      .toLowerCase()
      .replace(new RegExp(' ', 'g'), '-');
  }
}

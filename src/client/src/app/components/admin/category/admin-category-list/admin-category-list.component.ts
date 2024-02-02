import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Category } from 'src/app/models/category';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-admin-category-list',
  templateUrl: './admin-category-list.component.html',
  styleUrls: ['./admin-category-list.component.css'],
})
export class AdminCategoryListComponent implements OnInit {
  categories: Category[];
  p: number = 1;

  constructor(
    private categoryService: CategoryService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getCategories();
  }

  getCategories() {
    this.spinner.show();

    this.categoryService.getCategories().subscribe({
      next: categories => {
        this.spinner.hide();
        this.categories = categories;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  deleteCategory(category: Category): void {
    this.spinner.show();

    this.categoryService.deleteCategory(category.id).subscribe({
      next: () => {
        this.spinner.hide();
        this.alertify.success('Category has been deleted');
        this.getCategories();
      },
      error: (error) => {
        this.spinner.hide();
        this.alertify.error(error.error);
      },
    });
  }
}

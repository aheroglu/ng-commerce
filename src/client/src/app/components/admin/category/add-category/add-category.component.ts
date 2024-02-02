import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent {
  model: any = {};

  constructor(
    private categoryService: CategoryService,
    private alertify: AlertifyService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  onFormSubmit() {
    this.spinner.show();

    if (this.model) {
      this.categoryService.addCategory(this.model).subscribe({
        next: () => {
          this.spinner.hide();
          this.alertify.success('Category has been added');
          this.router.navigate(['/admin/categories']);
        },
        error: (error) => {
          this.spinner.hide();
          this.alertify.error(error.error);
        },
      });
    }
  }

  updateUrlHandle(): void {
    this.model.urlHandle = this.model.name
      .toLowerCase()
      .replace(new RegExp(' ', 'g'), '-');
  }
}

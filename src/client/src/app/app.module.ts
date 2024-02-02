import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HashLocationStrategy, LocationStrategy } from '@angular/common'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NewsletterComponent } from './components/home/newsletter/newsletter.component';
import { PopularPhonesComponent } from './components/home/popular-phones/popular-phones.component';
import { PopularLaptopsComponent } from './components/home/popular-laptops/popular-laptops.component';
import { ProductsComponent } from './components/product/products/products.component';
import { FeaturesComponent } from './components/home/features/features.component';
import { HeroComponent } from './components/home/hero/hero.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { TopBrandsComponent } from './components/home/top-brands/top-brands.component';
import { TopSellersComponent } from './components/home/top-sellers/top-sellers.component';
import { AuthGuard } from './guards/auth.guard';
import { CartComponent } from './components/cart/cart.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { FavouritesComponent } from './components/user/favourites/favourites.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { OrdersComponent } from './components/user/orders/orders.component';
import { OrderDetailComponent } from './components/user/orders/order-detail/order-detail.component';
import { AdminProductListComponent } from './components/admin/product/admin-product-list/admin-product-list.component';
import { AdminCategoryListComponent } from './components/admin/category/admin-category-list/admin-category-list.component';
import { AdminsComponent } from './components/admin/user/admins/admins.component';
import { MembersComponent } from './components/admin/user/members/members.component';
import { EvaluationsComponent } from './components/user/evaluations/evaluations.component';
import { ReviewsComponent } from './components/admin/review/reviews/reviews.component';
import { OrderListComponent } from './components/admin/order/order-list/order-list.component';
import { AdminOrderDetailComponent } from './components/admin/order/admin-order-detail/admin-order-detail.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { EditCategoryComponent } from './components/admin/category/edit-category/edit-category.component';
import { NotFoundComponent } from './components/error-pages/not-found/not-found.component';
import { ProductsInCategoryComponent } from './components/admin/category/products-in-category/products-in-category.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddCategoryComponent } from './components/admin/category/add-category/add-category.component';
import { EditProductComponent } from './components/admin/product/edit-product/edit-product.component';
import { AddProductComponent } from './components/admin/product/add-product/add-product.component';
import { ReviewDetailComponent } from './components/admin/review/review-detail/review-detail.component';
import { DashboardComponent } from './components/admin/dashboard/dashboard/dashboard.component';
import { AddProductImageComponent } from './components/admin/product/add-product-image/add-product-image.component';
import { ProductImagesComponent } from './components/admin/product/product-images/product-images.component';
import { ProductReviewsComponent } from './components/admin/product/product-reviews/product-reviews.component';
import { SettingsComponent } from './components/user/settings/settings.component';
import { AddAdminComponent } from './components/admin/user/admins/add-admin/add-admin.component';
import { ProductDetailComponent } from './components/product/product-detail/product-detail.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ForSubscribersComponent } from './components/mail/for-subscribers/for-subscribers.component';
import { MailsComponent } from './components/mail/mails/mails.component';
import { ForMembersComponent } from './components/mail/for-members/for-members.component';
import { ForAdminsComponent } from './components/mail/for-admins/for-admins.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/forgot-password/reset-password/reset-password.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductDetailComponent,
    NavbarComponent,
    FooterComponent,
    NewsletterComponent,
    PopularPhonesComponent,
    PopularLaptopsComponent,
    ProductsComponent,
    FeaturesComponent,
    HeroComponent,
    LoginComponent,
    SignupComponent,
    TopBrandsComponent,
    TopSellersComponent,
    CartComponent,
    CheckoutComponent,
    FavouritesComponent,
    ProfileComponent,
    OrdersComponent,
    OrderDetailComponent,
    AdminProductListComponent,
    AdminCategoryListComponent,
    AdminsComponent,
    MembersComponent,
    EvaluationsComponent,
    ReviewsComponent,
    OrderListComponent,
    AdminOrderDetailComponent,
    EditCategoryComponent,
    NotFoundComponent,
    ProductsInCategoryComponent,
    AddCategoryComponent,
    EditProductComponent,
    AddProductComponent,
    ReviewDetailComponent,
    DashboardComponent,
    AddProductImageComponent,
    ProductImagesComponent,
    ProductReviewsComponent,
    SettingsComponent,
    AddAdminComponent,
    ForSubscribersComponent,
    MailsComponent,
    ForMembersComponent,
    ForAdminsComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SweetAlert2Module.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['ng-commerce-api.somee.com', 'ng-commerce-api.aheroglu.dev'],
        disallowedRoutes: ['ng-commerce-api.somee.com/api/auth', 'ng-commerce-api.aheroglu.dev/api/auth'],
      },
    }),
    NgxPaginationModule,
    NgxSpinnerModule.forRoot({ type: 'ball-scale-multiple' }),
    BrowserModule,
    BrowserAnimationsModule,
  ],
  providers: [AuthGuard, { provide: LocationStrategy, useClass: HashLocationStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule { }

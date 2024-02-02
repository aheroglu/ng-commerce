import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProductDetailComponent } from './components/product/product-detail/product-detail.component';
import { ProductsComponent } from './components/product/products/products.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { CartComponent } from './components/cart/cart.component';
import { AuthGuard } from './guards/auth.guard';
import { GuestGuard } from './guards/guest.guard';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { FavouritesComponent } from './components/user/favourites/favourites.component';
import { OrdersComponent } from './components/user/orders/orders.component';
import { OrderDetailComponent } from './components/user/orders/order-detail/order-detail.component';
import { AdminGuard } from './guards/admin.guard';
import { AdminProductListComponent } from './components/admin/product/admin-product-list/admin-product-list.component';
import { AdminCategoryListComponent } from './components/admin/category/admin-category-list/admin-category-list.component';
import { AdminsComponent } from './components/admin/user/admins/admins.component';
import { MembersComponent } from './components/admin/user/members/members.component';
import { EvaluationsComponent } from './components/user/evaluations/evaluations.component';
import { ReviewsComponent } from './components/admin/review/reviews/reviews.component';
import { OrderListComponent } from './components/admin/order/order-list/order-list.component';
import { AdminOrderDetailComponent } from './components/admin/order/admin-order-detail/admin-order-detail.component';
import { EditCategoryComponent } from './components/admin/category/edit-category/edit-category.component';
import { NotFoundComponent } from './components/error-pages/not-found/not-found.component';
import { ProductsInCategoryComponent } from './components/admin/category/products-in-category/products-in-category.component';
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
import { ForSubscribersComponent } from './components/mail/for-subscribers/for-subscribers.component';
import { MailsComponent } from './components/mail/mails/mails.component';
import { ForMembersComponent } from './components/mail/for-members/for-members.component';
import { ForAdminsComponent } from './components/mail/for-admins/for-admins.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/forgot-password/reset-password/reset-password.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent, canActivate: [GuestGuard] },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'favourites', component: FavouritesComponent, canActivate: [AuthGuard] },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] },
  { path: 'order-detail/:orderNo', component: OrderDetailComponent, canActivate: [AuthGuard] },
  { path: 'evaluations', component: EvaluationsComponent, canActivate: [AuthGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
  { path: '404', component: NotFoundComponent },
  { path: 'signup', component: SignupComponent, canActivate: [GuestGuard] },
  { path: 'forgot-password', component: ForgotPasswordComponent, canActivate: [GuestGuard] },
  { path: 'reset-password/:email', component: ResetPasswordComponent, canActivate: [GuestGuard] },
  { path: 'products', component: ProductsComponent },
  { path: 'product-detail/:urlHandle', component: ProductDetailComponent },
  { path: 'admin/dashboard', component: DashboardComponent, canActivate: [AdminGuard] },
  { path: 'admin/products', component: AdminProductListComponent, canActivate: [AdminGuard] },
  { path: 'admin/categories', component: AdminCategoryListComponent, canActivate: [AdminGuard] },
  { path: 'admin/admins', component: AdminsComponent, canActivate: [AdminGuard] },
  { path: 'admin/admins/add', component: AddAdminComponent, canActivate: [AdminGuard] },
  { path: 'admin/members', component: MembersComponent, canActivate: [AdminGuard] },
  { path: 'admin/reviews', component: ReviewsComponent, canActivate: [AdminGuard] },
  { path: 'admin/reviews/:id', component: ReviewDetailComponent, canActivate: [AdminGuard] },
  { path: 'admin/orders', component: OrderListComponent, canActivate: [AdminGuard] },
  { path: 'admin/categories/add', component: AddCategoryComponent, canActivate: [AdminGuard] },
  { path: 'admin/products/add', component: AddProductComponent, canActivate: [AdminGuard] },
  { path: 'admin/products/images/:urlHandle', component: ProductImagesComponent, canActivate: [AdminGuard] },
  { path: 'admin/products/add-image/:urlHandle', component: AddProductImageComponent, canActivate: [AdminGuard] },
  { path: 'admin/products/reviews/:urlHandle', component: ProductReviewsComponent, canActivate: [AdminGuard] },
  { path: 'admin/orders/:orderNo', component: AdminOrderDetailComponent, canActivate: [AdminGuard] },
  { path: 'admin/categories/:urlHandle', component: EditCategoryComponent, canActivate: [AdminGuard] },
  { path: 'admin/categories/products/:urlHandle', component: ProductsInCategoryComponent, canActivate: [AdminGuard] },
  { path: 'admin/products/:urlHandle', component: EditProductComponent, canActivate: [AdminGuard] },
  { path: 'mail/mails', component: MailsComponent, canActivate: [AdminGuard] },
  { path: 'mail/for-subscribers', component: ForSubscribersComponent, canActivate: [AdminGuard] },
  { path: 'mail/for-members', component: ForMembersComponent, canActivate: [AdminGuard] },
  { path: 'mail/for-admins', component: ForAdminsComponent, canActivate: [AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})

export class AppRoutingModule { }

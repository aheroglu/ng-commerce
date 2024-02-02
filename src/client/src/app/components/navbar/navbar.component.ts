import { ChangeDetectorRef, Component, DoCheck, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, DoCheck {
  constructor(
    private themeService: ThemeService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private cartService: CartService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngDoCheck(): void {
    const newNumberOfProducts = this.getNumberOfProducts();
    if (this.numberOfProduct !== newNumberOfProducts) {
      this.numberOfProduct = newNumberOfProducts;
      this.cdr.detectChanges();
    }
  }

  currentTheme: any = this.themeService.getTheme();
  role?: string;
  numberOfProduct = this.getNumberOfProducts();

  ngOnInit(): void {
    window.addEventListener('scroll', function () {
      const navbar = document.querySelector('.navbar');
      if (window.scrollY > 100) {
        navbar.classList.add('scrolled');
      } else {
        navbar.classList.remove('scrolled');
      }
    });
  }

  changeThemeColor(): void {
    this.themeService.toggleTheme();
    this.currentTheme = this.themeService.getTheme();
  }

  isLoggedIn() {
    return this.authService.loggedIn();
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  signOut() {
    this.alertify.warning('The session has been terminated');
    this.authService.signOut();
    this.router.navigate(['/home']);
    this.role = undefined;
  }

  getNumberOfProducts(): number {
    return this.cartService.getNumberOfProducts();
  }
}

import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private defaultTheme = 'light';
  themeChanged = new Subject<string>();

  getTheme(): string {
    const storedTheme = localStorage.getItem('theme');
    return storedTheme || this.defaultTheme;
  }

  toggleTheme() {
    const currentTheme = this.getTheme();
    const newTheme = currentTheme === 'light' ? 'dark' : 'light';
    localStorage.setItem('theme', newTheme);
    this.themeChanged.next(newTheme);
  }
}

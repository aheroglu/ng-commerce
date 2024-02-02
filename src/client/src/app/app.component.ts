import { Component, OnInit } from '@angular/core';
import { ThemeService } from './services/theme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(private themeService: ThemeService) {}

  ngOnInit(): void {
    const element = document.body;
    element.dataset['bsTheme'] = this.themeService.getTheme();

    this.themeService.themeChanged.subscribe((newTheme) => {
      element.dataset['bsTheme'] = newTheme;
    });
  }
}

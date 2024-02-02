import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Favourite } from 'src/app/models/favourite';
import { AlertifyService } from 'src/app/services/alertify.service';
import { FavouriteService } from 'src/app/services/favourite.service';

@Component({
  selector: 'app-favourites',
  templateUrl: './favourites.component.html',
  styleUrls: ['./favourites.component.css'],
})
export class FavouritesComponent implements OnInit {
  favourites: Favourite[];
  userId: string;
  p: number = 1;

  constructor(
    private favouriteService: FavouriteService,
    private alertify: AlertifyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getFavourites();
  }

  getFavourites() {
    this.spinner.show();
    const userId = localStorage.getItem('userid');

    this.favouriteService.getFavouritesByUser(userId).subscribe({
      next: favourites => {
        this.spinner.hide();
        this.favourites = favourites;
      },
      error: error => {
        this.spinner.hide();
        this.alertify.error(error.error);
      }
    });
  }

  removeFromFavourites(id: number) {
    this.spinner.show();

    this.favouriteService.removeFavourite(id).subscribe({
      next: () => {
        this.spinner.hide();
        this.getFavourites();
        this.alertify.warning('Product removed from favourites!');
      },
      error: (error) => {
        this.spinner.hide();
        this.alertify.error(error.error);
      },
    });
  }
}

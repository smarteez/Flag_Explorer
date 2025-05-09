import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CountryDisplayComponent } from './country-display/country-display.component';
import { CountryService } from './services/country.service';

@Component({
  selector: 'app-root',
  imports: [ RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',

})
export class AppComponent {
  title = 'Flag-Explorer-App';
    constructor() {}
}

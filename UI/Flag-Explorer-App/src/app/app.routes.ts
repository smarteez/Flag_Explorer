import { Routes } from '@angular/router';
import { CountryDetailsComponent } from './country-details/country-details.component';
import { CountryDisplayComponent } from './country-display/country-display.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: CountryDisplayComponent },
    { path: 'country-details/:name', component: CountryDetailsComponent },
    { path: '**', redirectTo: 'home' }
];

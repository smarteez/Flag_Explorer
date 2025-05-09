import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountryDisplayComponent } from './country-display/country-display.component';
import { BrowserModule } from '@angular/platform-browser';
import { CountryService } from './services/country.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { CountryDetailsComponent } from './country-details/country-details.component';





@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes),
    CommonModule,
    BrowserModule,
    NgxPaginationModule,
    CountryDisplayComponent,
    CountryDetailsComponent,
  ],
 // providers: [CountryService]
  providers: [provideHttpClient(withInterceptorsFromDi())
    ,CountryService
  ],
  exports: [RouterModule]


})
export class AppModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountryDisplayComponent } from './country-display/country-display.component';
import { BrowserModule } from '@angular/platform-browser';
import { CountryService } from './services/country.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';





@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BrowserModule,
    NgxPaginationModule,
    CountryDisplayComponent,

  ],
 // providers: [CountryService]
  providers: [provideHttpClient(withInterceptorsFromDi())
    ,CountryService
  ]


})
export class AppModule { }

import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CountryService } from '../services/country.service';
import { Country } from '../interface/country';
import { Observable, Subscription } from 'rxjs';
import { NgxPaginationModule } from 'ngx-pagination';
import { Router } from '@angular/router';


@Component({
  selector: 'app-country-display',
  imports: [CommonModule, NgxPaginationModule],


  templateUrl: './country-display.component.html',
  styleUrl: './country-display.component.css',
})
export class CountryDisplayComponent implements OnInit, OnDestroy {
  countries$!: Observable<Country[]>;
  countriesSub$ = new Subscription();
  countries: Country[] = [];
  pagedCountries: any[] = []; 
  page = 1;
  itemsPerPage = 25;

 constructor(private countryService: CountryService,
            private router: Router
 ) {}

   ngOnInit() {
     this.countriesSub$ = this.countryService.getAllCountries().subscribe(
      countries => {
       this.countries = countries;
       this.updatePage();
    });

  }

    updatePage() {
    const startIndex = (this.page - 1) * this.itemsPerPage;
    this.pagedCountries = this.countries.slice(startIndex, startIndex + this.itemsPerPage);
  }

    changePage(newPage: number) {
    this.page = newPage;
    this.updatePage();
  }
    
 openDetails(countryName: string | undefined) {
    if (!countryName) {
      console.error('Country name is undefined');
      return;
    }
    console.log('Country name:', countryName);
    this.router.navigateByUrl(`/country-details/${countryName}`);





  }


  ngOnDestroy() {
    this.countriesSub$.unsubscribe();
  }



}



import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CountryService } from '../services/country.service';
import { CountryDetails } from '../interface/countryDetails';

@Component({
  selector: 'app-country-details',
  imports: [],
  templateUrl: './country-details.component.html',
  styleUrl: './country-details.component.css'
})
export class CountryDetailsComponent  implements OnInit, OnDestroy{
  activatedRouteSub$ = new Subscription();
  countryDetailsSub$ = new Subscription();
  countryName = "";
  countryDetails: CountryDetails | undefined ;


constructor(private route: ActivatedRoute,
            private router: Router,
            private CountryService: CountryService,
) {} 
ngOnInit() {
  this.activatedRouteSub$ = this.route.paramMap.subscribe(params => {
    this.countryName = params.get('countryName') || '';
  });
  this.countryDetailsSub$ = this.CountryService.getCountryDetails(this.countryName).subscribe( data=> {
      this.countryDetails = data;
    });
}

 goBack() {
    this.router.navigateByUrl('/home');

  }

ngOnDestroy() {
  this.activatedRouteSub$.unsubscribe();
  this.countryDetailsSub$.unsubscribe();
}
}

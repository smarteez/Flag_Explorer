import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription, takeUntil } from 'rxjs';
import { CountryService } from '../services/country.service';
import { CountryDetails } from '../interface/countryDetails';

@Component({
  selector: 'app-country-details',
  imports: [],
  templateUrl: './country-details.component.html',
  styleUrl: './country-details.component.css'
})
export class CountryDetailsComponent  implements OnInit, OnDestroy{
 private destroy$ = new Subject<void>();

  countryName = "";
  countryDetails: CountryDetails | undefined ;


constructor(private route: ActivatedRoute,
            private router: Router,
            private CountryService: CountryService,
) {} 
ngOnInit() {
  this.route.paramMap.pipe(takeUntil(this.destroy$)).subscribe(params => {
    const name = params.get('countryName');
    if (name) {
      this.countryName = name;
      this.CountryService.getCountryDetails(name)
        .pipe(takeUntil(this.destroy$))
        .subscribe(data => {
          this.countryDetails = data;
        });
    }
  });
}


 goBack() {
    this.router.navigateByUrl('/home');

  }

ngOnDestroy() {
    this.destroy$.next();
  this.destroy$.complete();

}
}

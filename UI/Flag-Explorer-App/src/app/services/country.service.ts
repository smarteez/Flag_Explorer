import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Country } from "../interface/country";
import { Observable } from "rxjs";
import { CountryDetails } from "../interface/countryDetails";

@Injectable({
  providedIn: 'root'
})


export class CountryService {
    private  baseUrl = 'https://localhost:44319/api/';
  constructor(private http: HttpClient) { }


  getAllCountries() : Observable<Country[]> {
    return this.http.get<Country[]>(this.baseUrl + 'countries');
  }

  getCountryDetails(name: string) : Observable<CountryDetails> { 
    return this.http.get<CountryDetails>(this.baseUrl + 'countries/' + name);
  }
}
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CountryDisplayComponent } from './country-display.component';
import { CountryService } from '../services/country.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('CountryDisplayComponent', () => {
  let component: CountryDisplayComponent;
  let fixture: ComponentFixture<CountryDisplayComponent>;
  let mockCountryService: jasmine.SpyObj<CountryService>;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    mockCountryService = jasmine.createSpyObj('CountryService', ['getAllCountries']);
    mockRouter = jasmine.createSpyObj('Router', ['navigateByUrl']);

    await TestBed.configureTestingModule({
      declarations: [CountryDisplayComponent],
      providers: [
        { provide: CountryService, useValue: mockCountryService },
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CountryDisplayComponent);
    component = fixture.componentInstance;
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize countries list', () => {
    const mockCountries = [
      { name: 'South Africa', flag: 'flag-url-1', hasErrors: false, errorMessage: '' },
      { name: 'Japan', flag: 'flag-url-2', hasErrors: false, errorMessage: '' }
    ];
    mockCountryService.getAllCountries.and.returnValue(of(mockCountries));

    component.ngOnInit();
    
    expect(component.countries.length).toBe(2);
    expect(component.countries[0].name).toBe('South Africa');
  });

  it('should update page data correctly', () => {
    component.countries = Array(100).fill({ name: 'Country' });
    component.page = 2;
    component.itemsPerPage = 10;

    component.updatePage();

    expect(component.pagedCountries.length).toBe(10);
  });

  it('should navigate to country details', () => {
    const countryName = 'South Africa';
    component.openDetails(countryName);

    expect(mockRouter.navigateByUrl).toHaveBeenCalledWith(`/country-details/${encodeURIComponent(countryName)}`);
  });

  it('should unsubscribe on destroy', () => {
    spyOn(component.countriesSub$, 'unsubscribe');
    component.ngOnDestroy();
    expect(component.countriesSub$.unsubscribe).toHaveBeenCalled();
  });
});
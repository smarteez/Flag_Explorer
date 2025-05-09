import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { CountryDetailsComponent } from './country-details.component';
import { CountryService } from '../services/country.service';

describe('CountryDetailsComponent', () => {
  let component: CountryDetailsComponent;
  let fixture: ComponentFixture<CountryDetailsComponent>;
  let mockCountryService: jasmine.SpyObj<CountryService>;

  beforeEach(async () => {
    mockCountryService = jasmine.createSpyObj('CountryService', ['getCountryDetails']);

    await TestBed.configureTestingModule({
      declarations: [CountryDetailsComponent],
      providers: [
        { provide: CountryService, useValue: mockCountryService },
        {
          provide: ActivatedRoute,
          useValue: { paramMap: of({ get: (key: string) => key === 'countryName' ? 'South Africa' : null }) }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CountryDetailsComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve country details on init', () => {
    const mockData = { 
      name: 'South Africa', 
      population: 59000000, 
      flag: 'https://example.com/south-africa-flag.png', 
      hasErrors: false, 
      errorMessage: '', 
      capital: 'Pretoria' 
    };
    mockCountryService.getCountryDetails.and.returnValue(of(mockData));

    component.ngOnInit();

    expect(component.countryDetails).toEqual(mockData);
  });

  it('should navigate back to home when goBack() is called', () => {
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);
    component['router'] = routerSpy;

    component.goBack();
    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/home');
  });
});

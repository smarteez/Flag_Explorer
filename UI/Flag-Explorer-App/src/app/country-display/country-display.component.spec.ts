import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryDisplayComponent } from './country-display.component';

describe('CountryDisplayComponent', () => {
  let component: CountryDisplayComponent;
  let fixture: ComponentFixture<CountryDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CountryDisplayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CountryDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

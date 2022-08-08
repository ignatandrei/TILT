import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnePublicTiltComponent } from './one-public-tilt.component';

describe('OnePublicTiltComponent', () => {
  let component: OnePublicTiltComponent;
  let fixture: ComponentFixture<OnePublicTiltComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OnePublicTiltComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OnePublicTiltComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayTILTComponent } from './display-tilt.component';

describe('DisplayTILTComponent', () => {
  let component: DisplayTILTComponent;
  let fixture: ComponentFixture<DisplayTILTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayTILTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayTILTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

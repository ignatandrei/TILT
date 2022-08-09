import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTiltComponent } from './my-tilt.component';

describe('MyTiltComponent', () => {
  let component: MyTiltComponent;
  let fixture: ComponentFixture<MyTiltComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyTiltComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyTiltComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

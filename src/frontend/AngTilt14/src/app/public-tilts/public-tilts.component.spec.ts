import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicTiltsComponent } from './public-tilts.component';

describe('PublicTiltsComponent', () => {
  let component: PublicTiltsComponent;
  let fixture: ComponentFixture<PublicTiltsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicTiltsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicTiltsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

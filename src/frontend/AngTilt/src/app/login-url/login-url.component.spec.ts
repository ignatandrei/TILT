import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginUrlComponent } from './login-url.component';

describe('LoginUrlComponent', () => {
  let component: LoginUrlComponent;
  let fixture: ComponentFixture<LoginUrlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginUrlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginUrlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { LoginUrlGuard } from './login-url.guard';

describe('LoginUrlGuard', () => {
  let guard: LoginUrlGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LoginUrlGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});

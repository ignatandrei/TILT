import { TestBed } from '@angular/core/testing';

import { LoginUrlService } from './login-url.service';

describe('LoginUrlService', () => {
  let service: LoginUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

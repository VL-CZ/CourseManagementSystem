import { TestBed } from '@angular/core/testing';

import { EnrollmentRequestService } from './enrollment-request.service';

describe('EnrollmentRequestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrollmentRequestService = TestBed.get(EnrollmentRequestService);
    expect(service).toBeTruthy();
  });
});

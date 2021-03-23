import { TestBed } from '@angular/core/testing';

import { TestSubmissionService } from './test-submission.service';

describe('TestSubmissionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TestSubmissionService = TestBed.get(TestSubmissionService);
    expect(service).toBeTruthy();
  });
});

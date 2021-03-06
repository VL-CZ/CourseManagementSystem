import { TestBed } from '@angular/core/testing';

import { TestSubmitService } from './test-submit.service';

describe('TestSubmitService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TestSubmitService = TestBed.get(TestSubmitService);
    expect(service).toBeTruthy();
  });
});

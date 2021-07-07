import { TestBed } from '@angular/core/testing';

import { CourseTestService } from './course-test.service';

describe('CourseTestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CourseTestService = TestBed.get(CourseTestService);
    expect(service).toBeTruthy();
  });
});

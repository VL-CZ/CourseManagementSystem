import { TestBed } from '@angular/core/testing';

import { CourseAdminService } from './course-admin.service';

describe('CourseAdminService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CourseAdminService = TestBed.get(CourseAdminService);
    expect(service).toBeTruthy();
  });
});

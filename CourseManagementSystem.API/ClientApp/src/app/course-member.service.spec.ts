import { TestBed } from '@angular/core/testing';

import { CourseMemberService } from './course-member.service';

describe('PersonService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CourseMemberService = TestBed.get(CourseMemberService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { ForumPostService } from './forum-post.service';

describe('ForumPostService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ForumPostService = TestBed.get(ForumPostService);
    expect(service).toBeTruthy();
  });
});

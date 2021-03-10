import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestSubmissionReviewComponent } from './test-submission-review.component';

describe('TestSubmissionReviewComponent', () => {
  let component: TestSubmissionReviewComponent;
  let fixture: ComponentFixture<TestSubmissionReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestSubmissionReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestSubmissionReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

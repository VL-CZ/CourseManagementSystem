import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestSubmissionListComponent } from './test-submission-list.component';

describe('TestSubmissionListComponent', () => {
  let component: TestSubmissionListComponent;
  let fixture: ComponentFixture<TestSubmissionListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestSubmissionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestSubmissionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

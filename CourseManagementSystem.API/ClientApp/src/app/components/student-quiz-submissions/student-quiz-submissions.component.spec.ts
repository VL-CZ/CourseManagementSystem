import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentQuizSubmissionsComponent } from './student-quiz-submissions.component';

describe('StudentQuizSubmissionsComponent', () => {
  let component: StudentQuizSubmissionsComponent;
  let fixture: ComponentFixture<StudentQuizSubmissionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentQuizSubmissionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentQuizSubmissionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

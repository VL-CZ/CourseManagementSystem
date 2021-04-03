import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentTestSubmissionsComponent } from './student-test-submissions.component';

describe('StudentTestSubmissionsComponent', () => {
  let component: StudentTestSubmissionsComponent;
  let fixture: ComponentFixture<StudentTestSubmissionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentTestSubmissionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentTestSubmissionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

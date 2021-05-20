import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentAverageScoreComponent } from './student-average-score.component';

describe('StudentAverageScoreComponent', () => {
  let component: StudentAverageScoreComponent;
  let fixture: ComponentFixture<StudentAverageScoreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentAverageScoreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentAverageScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

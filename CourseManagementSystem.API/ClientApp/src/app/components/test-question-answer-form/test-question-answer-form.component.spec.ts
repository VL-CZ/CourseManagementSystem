import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestQuestionAnswerFormComponent } from './test-question-answer-form.component';

describe('TestQuestionAnswerFormComponent', () => {
  let component: TestQuestionAnswerFormComponent;
  let fixture: ComponentFixture<TestQuestionAnswerFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestQuestionAnswerFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestQuestionAnswerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestQuestionEditComponent } from './test-question-edit.component';

describe('TestQuestionComponent', () => {
  let component: TestQuestionEditComponent;
  let fixture: ComponentFixture<TestQuestionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestQuestionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestQuestionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

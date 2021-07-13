import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestQuestionTextComponent } from './test-question-text.component';

describe('TestQuestionTextComponent', () => {
  let component: TestQuestionTextComponent;
  let fixture: ComponentFixture<TestQuestionTextComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestQuestionTextComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestQuestionTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

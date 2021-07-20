import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollmentRequestListComponent } from './enrollment-request-list.component';

describe('EnrollmentRequestListComponent', () => {
  let component: EnrollmentRequestListComponent;
  let fixture: ComponentFixture<EnrollmentRequestListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrollmentRequestListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollmentRequestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {ModalModule} from 'ngx-bootstrap/modal';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {StudentGradeListComponent} from './components/student-grade-list/student-grade-list.component';
import {StudentListComponent} from './components/student-list/student-list.component';
import {StudentDetailComponent} from './components/student-detail/student-detail.component';
import {FileListComponent} from './components/file-list/file-list.component';
import {CourseListComponent} from './components/course-list/course-list.component';
import {CourseDetailComponent} from './components/course-detail/course-detail.component';
import {TestDetailComponent} from './components/test-detail/test-detail.component';
import {TestListComponent} from './components/test-list/test-list.component';
import {TestCreateComponent} from './components/test-create/test-create.component';
import {TestSubmitComponent} from './components/test-submit/test-submit.component';
import {TestSubmissionReviewComponent} from './components/test-submission-review/test-submission-review.component';
import {TestSubmissionListComponent} from './components/test-submission-list/test-submission-list.component';
import {AddGradeComponent} from './components/add-grade/add-grade.component';
import {StudentTestSubmissionsComponent} from './components/student-test-submissions/student-test-submissions.component';
import {ConfirmDialogComponent} from './components/confirm-dialog/confirm-dialog.component';
import {CourseForumComponent} from './components/course-forum/course-forum.component';
import {TestEditComponent} from './components/test-edit/test-edit.component';
import {CourseEnrollmentComponent} from './components/course-enrollment/course-enrollment.component';
import {StudentAverageScoreComponent} from './components/student-average-score/student-average-score.component';
import {ErrorDialogComponent} from './components/error-dialog/error-dialog.component';
import {StudentQuizSubmissionsComponent} from './components/student-quiz-submissions/student-quiz-submissions.component';
import {AdminListComponent} from './components/admin-list/admin-list.component';
import {EnrollmentRequestListComponent} from './components/enrollment-request-list/enrollment-request-list.component';
import {TestQuestionEditComponent} from './components/test-question-edit/test-question-edit.component';
import {TestQuestionTextComponent} from './components/test-question-text/test-question-text.component';
import {TestQuestionAnswerFormComponent} from './components/test-question-answer-form/test-question-answer-form.component';
import {CourseMenuComponent} from './components/course-menu/course-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    StudentGradeListComponent,
    StudentListComponent,
    StudentDetailComponent,
    FileListComponent,
    CourseListComponent,
    CourseDetailComponent,
    TestDetailComponent,
    TestListComponent,
    TestCreateComponent,
    TestSubmitComponent,
    TestSubmissionReviewComponent,
    TestSubmissionListComponent,
    AddGradeComponent,
    StudentTestSubmissionsComponent,
    ConfirmDialogComponent,
    CourseForumComponent,
    TestEditComponent,
    CourseEnrollmentComponent,
    StudentAverageScoreComponent,
    ErrorDialogComponent,
    StudentQuizSubmissionsComponent,
    AdminListComponent,
    EnrollmentRequestListComponent,
    TestQuestionEditComponent,
    TestQuestionTextComponent,
    TestQuestionAnswerFormComponent,
    CourseMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'students/:id', component: StudentDetailComponent},
      {path: 'courses', component: CourseListComponent},
      {path: 'courses/:id', component: CourseDetailComponent},
      {path: 'courses/:id/enrollmentRequests', component: EnrollmentRequestListComponent},
      {path: 'tests/:id', component: TestDetailComponent},
      {path: 'tests/edit/:id', component: TestEditComponent},
      {path: 'tests/create/:id', component: TestCreateComponent},
      {path: 'tests/submit/:id', component: TestSubmitComponent},
      {path: 'submissions/:id', component: TestSubmissionReviewComponent}
    ]),
    ModalModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmDialogComponent, ErrorDialogComponent]
})
export class AppModule {
}

import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {ModalModule} from 'ngx-bootstrap/modal';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {StudentGradeListComponent} from './student-grade-list/student-grade-list.component';
import {StudentListComponent} from './student-list/student-list.component';
import {StudentDetailComponent} from './student-detail/student-detail.component';
import {FileListComponent} from './file-list/file-list.component';
import {CourseListComponent} from './course-list/course-list.component';
import {CourseDetailComponent} from './course-detail/course-detail.component';
import {TestDetailComponent} from './test-detail/test-detail.component';
import {TestListComponent} from './test-list/test-list.component';
import {TestCreateComponent} from './test-create/test-create.component';
import {TestSubmitComponent} from './test-submit/test-submit.component';
import {TestSubmissionReviewComponent} from './test-submission-review/test-submission-review.component';
import {TestSubmissionListComponent} from './test-submission-list/test-submission-list.component';
import {AddGradeComponent} from './add-grade/add-grade.component';
import {StudentTestSubmissionsComponent} from './student-test-submissions/student-test-submissions.component';
import {ConfirmDialogComponent} from './confirm-dialog/confirm-dialog.component';
import {CourseForumComponent} from './course-forum/course-forum.component';
import {TestEditComponent} from './test-edit/test-edit.component';
import { CourseEnrollmentComponent } from './course-enrollment/course-enrollment.component';

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
    CourseEnrollmentComponent
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
  entryComponents: [ConfirmDialogComponent]
})
export class AppModule {
}

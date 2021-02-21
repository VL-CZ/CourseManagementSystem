import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {AuthorizeGuard} from 'src/api-authorization/authorize.guard';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {GradeListComponent} from './grade-list/grade-list.component';
import {StudentListComponent} from './student-list/student-list.component';
import {StudentDetailComponent} from './student-detail/student-detail.component';
import {FileListComponent} from './file-list/file-list.component';
import {CourseListComponent} from './course-list/course-list.component';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { TestDetailComponent } from './test-detail/test-detail.component';
import { TestListComponent } from './test-list/test-list.component';
import { TestCreateComponent } from './test-create/test-create.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GradeListComponent,
    StudentListComponent,
    StudentDetailComponent,
    FileListComponent,
    CourseListComponent,
    CourseDetailComponent,
    TestDetailComponent,
    TestListComponent,
    TestCreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'grades', component: GradeListComponent},
      {path: 'students/:id', component: StudentDetailComponent},
      {path: 'courses', component: CourseListComponent},
      {path: 'courses/:id', component: CourseDetailComponent},
      {path: 'tests/:id', component: TestDetailComponent},
      {path: 'tests/create/:id', component: TestCreateComponent},
    ])
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

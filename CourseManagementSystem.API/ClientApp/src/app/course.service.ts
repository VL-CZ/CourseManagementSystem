import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddCourseVM} from './viewmodels/courseVM';
import {CourseMemberOrAdminVM} from './viewmodels/courseMemberOrAdminVM';
import {CourseFileVM} from './viewmodels/courseFileVM';
import {CourseTestDetailsVM} from './viewmodels/courseTestVM';
import {ForumPostVM} from './viewmodels/forumPostVM';
import {WrapperVM} from './viewmodels/wrapperVM';

@Injectable({
  providedIn: 'root'
})
export class CourseService extends ApiService {
  private static controllerName = 'courses';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, CourseService.controllerName);
  }

  /**
   * create new course
   * @returns created course info
   */
  public create(courseVM: AddCourseVM): Observable<{}> {
    return this.httpPost('create', courseVM);
  }

  /**
   * delete course by id
   * @param id identifier of the course
   */
  public delete(id: string): Observable<{}> {
    return this.httpDelete(id);
  }

  /**
   * enroll currently logged-in user to the course with given id
   * @param courseId id of the course to enroll
   */
  public enrollTo(courseId: string): Observable<{}> {
    return this.httpPost(`${courseId}/enroll`, {});
  }

  /**
   * add new admin to the course with given id
   * @param courseId id of the course to enroll
   * @param adminId id of the administrator to add
   */
  public addAdmin(courseId: string, adminId: WrapperVM<string>): Observable<{}> {
    return this.httpPost(`${courseId}/addAdmin`, adminId);
  }

  /**
   * get all members of this course
   * @param courseId identifier of the course
   */
  public getAllMembers(courseId: string): Observable<CourseMemberOrAdminVM[]> {
    return this.httpGet<CourseMemberOrAdminVM[]>(`${courseId}/members`);
  }

  /**
   * get all admins of this course
   * @param courseId identifier of the course
   */
  public getAllAdmins(courseId: string): Observable<CourseMemberOrAdminVM[]> {
    return this.httpGet<CourseMemberOrAdminVM[]>(`${courseId}/admins`);
  }

  /**
   * get all shared files in this this course
   * @param courseId identifier of the course
   */
  public getAllFiles(courseId: string): Observable<CourseFileVM[]> {
    return this.httpGet<CourseFileVM[]>(`${courseId}/files`);
  }

  /**
   * get all active tests in this course
   * @param courseId identifier of the course
   */
  public getActiveTests(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.httpGet<CourseTestDetailsVM[]>(`${courseId}/activeTests`);
  }

  /**
   * get all non-published tests in this course
   * @param courseId identifier of the course
   */
  public getNonPublishedTests(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.httpGet<CourseTestDetailsVM[]>(`${courseId}/nonPublishedTests`);
  }

  /**
   * get all tests after deadline in this course
   * @param courseId identifier of the course
   */
  public getTestsAfterDeadline(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.httpGet<CourseTestDetailsVM[]>(`${courseId}/testsAfterDeadline`);
  }

  /**
   * get all posts in this course
   * @param courseId identifier of the course
   */
  public getAllPosts(courseId: string): Observable<ForumPostVM[]> {
    return this.httpGet<ForumPostVM[]>(`${courseId}/posts`);
  }
}

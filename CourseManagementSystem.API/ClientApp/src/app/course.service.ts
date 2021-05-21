import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddCourseVM} from './viewmodels/courseVM';
import {CourseMemberVM} from './viewmodels/courseMemberVM';
import {CourseFileVM} from './viewmodels/courseFileVM';
import {CourseTestDetailsVM} from './viewmodels/courseTestVM';
import {ForumPostVM} from './viewmodels/forumPostVM';

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
   * get all members of this course
   * @param courseId identifier of the course
   */
  public getAllMembers(courseId: string): Observable<CourseMemberVM[]> {
    return this.httpGet<CourseMemberVM[]>(`${courseId}/members`);
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
  public getAllActiveTests(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.httpGet<CourseTestDetailsVM[]>(`${courseId}/activeTests`);
  }

  /**
   * get all tests in this course
   * @param courseId identifier of the course
   */
  public getAllTests(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.httpGet<CourseTestDetailsVM[]>(`${courseId}/tests`);
  }

  /**
   * get all posts in this course
   * @param courseId identifier of the course
   */
  public getAllPosts(courseId: string): Observable<ForumPostVM[]> {
    return this.httpGet<ForumPostVM[]>(`${courseId}/posts`);
  }
}

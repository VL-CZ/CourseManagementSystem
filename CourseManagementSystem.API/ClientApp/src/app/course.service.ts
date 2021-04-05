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
    return this.http.post<{}>(this.controllerUrl + 'create', courseVM);
  }

  /**
   * delete course by id
   * @param id identifier of the course
   */
  public delete(id: number): Observable<{}> {
    return this.http.delete(this.controllerUrl + id);
  }

  /**
   * get all members of this course
   * @param courseId identifier of the course
   */
  public getAllMembers(courseId: string): Observable<CourseMemberVM[]> {
    return this.http.get<CourseMemberVM[]>(this.controllerUrl + `${courseId}/members`);
  }

  /**
   * get all shared files in this this course
   * @param courseId identifier of the course
   */
  public getAllFiles(courseId: string): Observable<CourseFileVM[]> {
    return this.http.get<CourseFileVM[]>(this.controllerUrl + `${courseId}/files`);
  }

  /**
   * get all tests in this course
   * @param courseId identifier of the course
   */
  public getAllTests(courseId: string): Observable<CourseTestDetailsVM[]> {
    return this.http.get<CourseTestDetailsVM[]>(this.controllerUrl + `${courseId}/tests`);
  }

  /**
   * get all posts in this course
   * @param courseId identifier of the course
   */
  public getAllPosts(courseId: string): Observable<ForumPostVM[]> {
    return this.http.get<ForumPostVM[]>(this.controllerUrl + `${courseId}/posts`);
  }
}

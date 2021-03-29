import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CourseInfoVM} from './viewmodels/courseInfoVM';
import {AddCourseVM} from './viewmodels/addCourseVM';
import {CourseMemberVM} from './viewmodels/courseMemberVM';
import {FileVM} from './viewmodels/fileVM';
import {CourseTestVM} from './viewmodels/courseTestVM';

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
  public create(courseVM: AddCourseVM): Observable<CourseInfoVM> {
    return this.http.post<CourseInfoVM>(this.controllerUrl + 'create', courseVM);
  }

  /**
   * delete course by id
   * @param id
   */
  public delete(id: number): Observable<{}> {
    return this.http.delete(this.controllerUrl + id);
  }

  /**
   * get all members of this course
   * @param courseId
   */
  public getAllMembers(courseId: string): Observable<CourseMemberVM[]> {
    return this.http.get<CourseMemberVM[]>(this.controllerUrl + `${courseId}/members`);
  }

  /**
   * get all shared files in this this course
   * @param courseId
   */
  public getAllFiles(courseId: string): Observable<FileVM[]> {
    return this.http.get<FileVM[]>(this.controllerUrl + `${courseId}/files`);
  }

  /**
   * get all tests in this course
   * @param courseId
   */
  public getAllTests(courseId: string): Observable<CourseTestVM[]> {
    return this.http.get<CourseTestVM[]>(this.controllerUrl + `${courseId}/tests`);
  }
}

import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CourseInfoVM} from './viewmodels/courseInfoVM';
import {AddCourseVM} from './viewmodels/addCourseVM';
import {Person} from './viewmodels/student';
import {FileVM} from './viewmodels/fileVM';

@Injectable({
  providedIn: 'root'
})
export class CourseService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * create new course
   * @returns created course info
   */
  public create(courseVM: AddCourseVM): Observable<CourseInfoVM> {
    return this.http.post<CourseInfoVM>(this.baseUrl + 'api/courses/create/', courseVM);
  }

  /**
   * delete course by id
   * @param id
   */
  public delete(id: number): Observable<{}> {
    return this.http.delete(this.baseUrl + 'api/courses/' + id);
  }

  /**
   * get all members of this course
   * @param courseId
   */
  public getAllMembers(courseId: number): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl + `api/courses/${courseId.toString()}/members`);
  }

  /**
   * get all shared files in this this course
   * @param courseId
   */
  public getAllFiles(courseId: number): Observable<FileVM[]> {
    return this.http.get<FileVM[]>(this.baseUrl + `api/courses/${courseId.toString()}/files`);
  }
}

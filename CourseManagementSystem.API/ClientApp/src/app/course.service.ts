import {Inject, Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CourseInfoVM} from './viewmodels/courseInfoVM';
import {AddCourseVM} from './viewmodels/addCourseVM';

@Injectable({
  providedIn: 'root'
})
export class CourseService extends ApiService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  /**
   * get all courses
   */
  public getAll(): Observable<CourseInfoVM[]> {
    return this.http.get<CourseInfoVM[]>(this.baseUrl + 'api/courses/');
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
}

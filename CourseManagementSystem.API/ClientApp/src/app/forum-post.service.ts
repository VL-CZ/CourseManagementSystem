import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {ForumPostVM} from './viewmodels/forumPostVM';

@Injectable({
  providedIn: 'root'
})
export class ForumPostService extends ApiService {
  private static controllerName = 'forumPosts';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl, ForumPostService.controllerName);
  }

  /**
   * delete forum post by its id
   * @param postId identifier of the forum post
   */
  public delete(postId: string): Observable<{}> {
    return this.http.delete(this.controllerUrl + postId);
  }

  /**
   * add new forum post into a course
   * @param post forum post to add
   * @param courseId identifier of the course where to add the post
   */
  public add(post: ForumPostVM, courseId: string): Observable<{}> {
    return this.http.post<{}>(this.controllerUrl + courseId, post);
  }
}

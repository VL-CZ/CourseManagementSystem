import {Component, Input, OnInit} from '@angular/core';
import {ForumPostVM} from '../viewmodels/forumPostVM';
import {CourseService} from '../course.service';
import {ForumPostService} from '../forum-post.service';
import {RoleAuthService} from '../role-auth.service';
import {WrapperVM} from '../viewmodels/wrapperVM';

@Component({
  selector: 'app-course-forum',
  templateUrl: './course-forum.component.html',
  styleUrls: ['./course-forum.component.css']
})
export class CourseForumComponent implements OnInit {

  /**
   * identifier of the course that this forum belongs to
   * @private
   */
  @Input()
  private courseId: string;

  /**
   * is the current user admin of the course?
   */
  @Input()
  public isCourseAdmin: boolean;

  /**
   * posts in this forum
   */
  public posts: ForumPostVM[] = [];

  /**
   * post to add
   */
  public postToAdd: WrapperVM<string> = new WrapperVM<string>();

  private courseService: CourseService;
  private forumPostService: ForumPostService;

  constructor(courseService: CourseService, forumPostService: ForumPostService, roleAuthService: RoleAuthService) {
    this.courseService = courseService;
    this.forumPostService = forumPostService;
  }

  ngOnInit() {
    this.reloadPosts();
  }

  /**
   * delete the given post
   * @param post post to delete
   */
  public delete(post: ForumPostVM): void {
    this.forumPostService.delete(post.id).subscribe(() => {
      this.reloadPosts();
    });
  }

  /**
   * add a new post
   */
  public addPost(): void {
    this.forumPostService.add(this.postToAdd, this.courseId).subscribe(() => {
      this.reloadPosts();
      this.postToAdd = new WrapperVM<string>();
    });
  }

  /**
   * reload the forum posts
   */
  private reloadPosts(): void {
    this.courseService.getAllPosts(this.courseId).subscribe(posts => {
      this.posts = posts;
    });
  }
}

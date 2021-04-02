import {Component, Input, OnInit} from '@angular/core';
import {ForumPostVM} from '../viewmodels/forumPostVM';
import {CourseService} from '../course.service';
import {ForumPostService} from '../forum-post.service';
import {RoleAuthService} from '../role-auth.service';

@Component({
  selector: 'app-course-forum',
  templateUrl: './course-forum.component.html',
  styleUrls: ['./course-forum.component.css']
})
export class CourseForumComponent implements OnInit {

  @Input()
  private courseId: string;

  /**
   * posts in this forum
   */
  public posts: ForumPostVM[] = [];

  /**
   * post to add
   */
  public postToAdd: ForumPostVM = new ForumPostVM();

  /**
   * is the current user admin?
   */
  public isAdmin: boolean;

  private courseService: CourseService;
  private forumPostService: ForumPostService;

  constructor(courseService: CourseService, forumPostService: ForumPostService, roleAuthService: RoleAuthService) {
    this.courseService = courseService;
    this.forumPostService = forumPostService;

    roleAuthService.isAdmin().subscribe(res => {
      this.isAdmin = res.isAdmin;
    });
  }

  ngOnInit() {
    this.reloadPosts();
  }

  /**
   * reload the forum posts
   */
  public reloadPosts(): void {
    this.courseService.getAllPosts(this.courseId).subscribe(posts => {
      this.posts = posts;
    });
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
    this.forumPostService.add(this.postToAdd, this.courseId).subscribe(post => {
      this.reloadPosts();
      this.postToAdd = new ForumPostVM();
    });
  }
}

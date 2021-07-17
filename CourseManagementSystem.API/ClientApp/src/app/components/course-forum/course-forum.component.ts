import {Component, Input, OnInit} from '@angular/core';
import {ForumPostVM} from '../../viewmodels/forumPostVM';
import {CourseService} from '../../services/course.service';
import {ForumPostService} from '../../services/forum-post.service';
import {RoleAuthService} from '../../services/role-auth.service';
import {WrapperVM} from '../../viewmodels/wrapperVM';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../utils/observableWrapper';
import {ConfirmDialogManager} from '../../utils/confirmDialogManager';

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
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(courseService: CourseService, forumPostService: ForumPostService, roleAuthService: RoleAuthService,
              bsModalService: BsModalService) {
    this.courseService = courseService;
    this.forumPostService = forumPostService;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);
  }

  ngOnInit() {
    this.reloadPosts();
  }

  /**
   * delete the given post
   * @param post post to delete
   */
  public delete(post: ForumPostVM): void {
    this.confirmDialogManager.displayDialog(
      'Remove a forum post',
      'Are you sure you want to Remove the selected post?',
      () => {
        this.forumPostService.delete(post.id).subscribe(() => {
          this.reloadPosts();
        });
      });
  }

  /**
   * add a new post
   */
  public addPost(): void {
    this.observableWrapper.subscribeOrShowError(
      this.forumPostService.add(this.postToAdd, this.courseId),
      () => {
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

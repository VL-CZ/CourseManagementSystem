import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {CourseService} from '../../services/course.service';
import {CourseInfoVM} from '../../viewmodels/courseVM';
import {PageNavigator} from '../../tools/pageNavigator';
import {Router} from '@angular/router';

/**
 * class representing menu with link to course detail
 */
@Component({
  selector: 'app-course-menu',
  templateUrl: './course-menu.component.html',
  styleUrls: ['./course-menu.component.css']
})
export class CourseMenuComponent implements OnInit, OnChanges {

  @Input()
  private courseId: string;

  /**
   *
   */
  public courseInfo: CourseInfoVM = new CourseInfoVM();

  /**
   * class for navigating between the pages
   */
  public readonly pageNavigator: PageNavigator;

  private courseService: CourseService;

  constructor(courseService: CourseService, router: Router) {
    this.courseService = courseService;
    this.pageNavigator = new PageNavigator(router);
  }

  ngOnInit() {
    this.loadCourse();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.loadCourse();
  }

  /**
   * load course data from the API
   * @private
   */
  private loadCourse(): void {
    if (this.courseId == null) {
      return;
    } else {
      this.courseService.getById(this.courseId).subscribe(res => {
        this.courseInfo = res;
      });
    }
  }
}

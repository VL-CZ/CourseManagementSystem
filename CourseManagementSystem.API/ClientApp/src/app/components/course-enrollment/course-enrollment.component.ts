import {Component, OnInit} from '@angular/core';
import {PeopleService} from '../../services/people.service';
import {CourseService} from '../../services/course.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterUtils} from '../../utils/routerUtils';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../utils/observableWrapper';
import {InformationDialogManager} from '../../utils/informationDialogManager';

/**
 * component with enrollment to course
 */
@Component({
  selector: 'app-course-enrollment',
  templateUrl: './course-enrollment.component.html',
  styleUrls: ['./course-enrollment.component.css']
})
export class CourseEnrollmentComponent implements OnInit {

  /**
   * id of the course that we enroll to
   */
  public courseToEnrollId: string;

  private readonly courseService: CourseService;
  private router: Router;
  private activatedRoute: ActivatedRoute;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;
  private informationDialogManager: InformationDialogManager;

  constructor(courseService: CourseService, activatedRoute: ActivatedRoute, router: Router, bsModalService: BsModalService) {
    this.courseService = courseService;
    this.router = router;
    this.activatedRoute = activatedRoute;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.informationDialogManager = new InformationDialogManager(this.bsModalRef, this.bsModalService);
  }

  ngOnInit() {
  }

  /**
   * enroll to a course
   */
  public enroll(): void {
    this.observableWrapper.subscribeOrShowError(
      this.courseService.enrollTo(this.courseToEnrollId),
      () => {
        this.informationDialogManager.displayDialog('Enrollment request was sent.');
      });
  }
}

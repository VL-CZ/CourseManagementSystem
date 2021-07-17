import {Component, OnInit} from '@angular/core';
import {EnrollmentRequestVM} from '../../viewmodels/enrollmentRequestVM';
import {EnrollmentRequestService} from '../../services/enrollment-request.service';
import {ActivatedRoute} from '@angular/router';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseService} from '../../services/course.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogManager} from '../../utils/confirmDialogManager';

@Component({
  selector: 'app-enrollment-request-list',
  templateUrl: './enrollment-request-list.component.html',
  styleUrls: ['./enrollment-request-list.component.css']
})
export class EnrollmentRequestListComponent implements OnInit {

  /**
   * list of enrollment requests to the given course
   */
  public enrollmentRequests: EnrollmentRequestVM[] = [];

  /**
   * id of the course that these reqeusts belong to
   */
  public courseId: string;

  private enrollmentRequestService: EnrollmentRequestService;
  private courseService: CourseService;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(route: ActivatedRoute, enrollmentRequestService: EnrollmentRequestService, courseService: CourseService,
              bsModalService: BsModalService) {
    this.enrollmentRequestService = enrollmentRequestService;
    this.courseService = courseService;
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.bsModalService = bsModalService;
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.bsModalService);

    this.reload();
  }

  ngOnInit() {
  }

  /**
   * approve the enrollment request
   * @param request enrollment request to approve
   */
  public approve(request: EnrollmentRequestVM): void {
    this.enrollmentRequestService.approve(request.id).subscribe(() => {
      this.reload();
    });
  }

  /**
   * decline the enrollment request
   * @param request enrollment request to decline
   */
  public decline(request: EnrollmentRequestVM): void {
    this.confirmDialogManager.displayDialog(
      'Decline a request for enrollment',
      'Are you sure you want to decline the selected enrollment request?',
      () => {
        this.enrollmentRequestService.decline(request.id).subscribe(() => {
          this.reload();
        });
      });
  }

  /**
   * reload the enrollment requests for the given course
   * @private
   */
  private reload(): void {
    this.courseService.getEnrollmentRequests(this.courseId).subscribe(requests => {
      this.enrollmentRequests = requests;
    });
  }
}

import {Component, OnInit} from '@angular/core';
import {EnrollmentRequestVM} from '../../viewmodels/enrollmentRequestVM';
import {EnrollmentRequestService} from '../../services/enrollment-request.service';
import {ActivatedRoute} from '@angular/router';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {CourseService} from '../../services/course.service';

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

  private courseId: string;
  private enrollmentRequestService: EnrollmentRequestService;
  private courseService: CourseService;

  constructor(route: ActivatedRoute, enrollmentRequestService: EnrollmentRequestService, courseService: CourseService) {
    this.enrollmentRequestService = enrollmentRequestService;
    this.courseService = courseService;
    this.courseId = ActivatedRouteUtils.getIdParam(route);
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
    this.enrollmentRequestService.decline(request.id).subscribe(() => {
      this.reload();
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

import {Component, Input, OnInit} from '@angular/core';
import {AddGradeVM} from '../viewmodels/addGradeVM';
import {PercentCalculator} from '../utils/percentCalculator';
import {CourseMemberService} from '../course-member.service';
import {RouterUtils} from '../utils/routerUtils';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-add-grade',
  templateUrl: './add-grade.component.html',
  styleUrls: ['./add-grade.component.css']
})
export class AddGradeComponent implements OnInit {

  @Input()
  public userId: string;
  public gradeToAdd: AddGradeVM;

  private readonly courseMemberService: CourseMemberService;
  private readonly route: ActivatedRoute;
  private readonly router: Router;

  constructor(courseMemberService: CourseMemberService, route: ActivatedRoute, router: Router) {
    this.courseMemberService = courseMemberService;
    this.route = route;
    this.router = router;
    this.gradeToAdd = new AddGradeVM();
  }

  ngOnInit() {
  }

  public addGrade(): void {
    // divide percents by 100 -> get double
    this.gradeToAdd.percentualValue = PercentCalculator.percentToDouble(this.gradeToAdd.percentualValue);

    this.courseMemberService.assignGrade(this.userId, this.gradeToAdd).subscribe(
      result => {
        RouterUtils.reloadPage(this.router, this.route);
      });

    this.gradeToAdd = new AddGradeVM();
  }
}

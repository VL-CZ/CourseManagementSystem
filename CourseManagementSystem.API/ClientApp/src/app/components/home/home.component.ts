import {Component} from '@angular/core';
import {Router} from '@angular/router';

/**
 * home component
 */
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(router: Router) {
    router.navigate(['/courses']);
  }
}

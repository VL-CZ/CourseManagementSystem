import { Component, OnInit } from '@angular/core';
import {BsModalRef} from 'ngx-bootstrap/modal';

/**
 * This component represent error dialog
 */
@Component({
  selector: 'app-error-dialog',
  templateUrl: './error-dialog.component.html',
  styleUrls: ['./error-dialog.component.css']
})
export class ErrorDialogComponent implements OnInit {

  /**
   * list of errors
   */
  public errors: string[];

  /**
   * modal reference
   */
  public modalRef: BsModalRef;

  constructor(modalRef: BsModalRef) {
    this.modalRef = modalRef;
  }

  ngOnInit() {
  }
}

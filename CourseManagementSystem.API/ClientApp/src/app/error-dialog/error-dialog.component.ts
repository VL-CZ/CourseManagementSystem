import { Component, OnInit } from '@angular/core';
import {BsModalRef} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-error-dialog',
  templateUrl: './error-dialog.component.html',
  styleUrls: ['./error-dialog.component.css']
})
export class ErrorDialogComponent implements OnInit {

  public errors: string[];

  public modalRef: BsModalRef;

  constructor(modalRef: BsModalRef) {
    this.modalRef = modalRef;
  }

  ngOnInit() {
  }
}

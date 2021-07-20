import { Component, OnInit } from '@angular/core';
import {BsModalRef} from 'ngx-bootstrap/modal';

/**
 * component representing information dialog
 */
@Component({
  selector: 'app-information-dialog',
  templateUrl: './information-dialog.component.html',
  styleUrls: ['./information-dialog.component.css']
})
export class InformationDialogComponent implements OnInit {

  /**
   * Text of the dialog
   */
  public text: string;

  /**
   * modal reference
   */
  public modalRef: BsModalRef;

  constructor(modalRef: BsModalRef) {
    this.modalRef = modalRef;
  }

  ngOnInit(): void {
  }
}
